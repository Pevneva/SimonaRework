using System.Collections.Generic;
using CodeBase.Arrow;
using CodeBase.Enemy;
using CodeBase.Hero;
using CodeBase.Infrastructure.Services.AssetManagement;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.PersistentData;
using CodeBase.Infrastructure.Services.Randomizer;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.StaticData;
using CodeBase.UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticData;
        private readonly IInputService _inputService;
        private readonly IArrowFactory _arrowFactory;
        private readonly IRandomService _randomService;
        private readonly IPersistentProgressService _progressService;

        public List<ILoadProgress> ProgressLoaders { get; } = new List<ILoadProgress>();
        public List<ISaveProgress> ProgressSavers { get; } = new List<ISaveProgress>();

        private Transform HeroTransform { get; set; }
        
        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticData, IInputService inputService, IArrowFactory arrowFactory, IRandomService randomService, IPersistentProgressService progressService)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
            _inputService = inputService;
            _arrowFactory = arrowFactory;
            _randomService = randomService;
            _progressService = progressService;
        }

        public GameObject CreateHero(GameObject at)
        {
            GameObject hero = InstantiateRegistered(AssetsPath.HeroPath, at: at.transform.position);
            HeroTransform = hero.transform;
            hero.GetComponent<HeroAttack>().Construct(_inputService,this);
            hero.GetComponent<HeroMover>().Construct(_inputService);
            _arrowFactory.InitializePool(HeroTransform);
            return hero;
        }

        public GameObject CreateHud()
        {
            GameObject hud = InstantiateRegistered(AssetsPath.HudPath);
            hud.GetComponentInChildren<CoinCounter>().Construct(_progressService.Progress.WorldData);
            return hud;
        }

        public void CreateArrow(GameObject hero)
        {
            GameObject arrow = _arrowFactory.CreateArrow(hero.transform);
            arrow.GetComponent<ArrowMover>().Construct(hero.GetComponent<SpriteRenderer>().flipX);
            arrow.SetActive(true);
        }

        public GameObject CreateMonster(MonsterTypeId typeId, Transform parent)
        {
            MonsterStaticData monsterData = _staticData.ForMonster(typeId);

            GameObject monster = Object.Instantiate(_staticData.ForMonster(typeId).Prefab, parent.position,
                Quaternion.identity, parent);

            EnemyHealth health = monster.GetComponentInChildren<EnemyHealth>();
            health.Max = monsterData.Hp;
            health.Current = monsterData.Hp;

            EnemyAttack attack = monster.GetComponentInChildren<EnemyAttack>();
            attack.Radius = monsterData.AttackZone;
            attack.Damage = monsterData.Damage;
            attack.AttackCooldown = monsterData.AttackCooldown;

            monster.GetComponentInChildren<EnemyMover>().Speed = monsterData.MoveSpeed;
            
            LootSpawner lootSpawner = monster.GetComponentInChildren<LootSpawner>();
            lootSpawner.Construct(this, _randomService);
            lootSpawner.SetLoot(monsterData.MinLoot, monsterData.MaxLoot);
            
            ChaseHero chaseHero = monster.GetComponentInChildren<ChaseHero>();
            chaseHero.Construct(HeroTransform);
            chaseHero.MinimalDistance = monsterData.MinimalChaseDistance;
            
            return monster;
        }

        public LootPiece CreateLoot()
        {
            LootPiece lootPiece = InstantiateRegistered(AssetsPath.LootPath).GetComponent<LootPiece>();
            lootPiece.Construct(_progressService.Progress.WorldData);
            return lootPiece;
        }

        public void Cleanup()
        {
            ProgressLoaders.Clear();
            ProgressSavers.Clear();
        }

        private GameObject InstantiateRegistered(string prefab, Vector3 at)
        {
            GameObject gameObject = _assetProvider.Instantiate(prefab, at: at);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private GameObject InstantiateRegistered(string prefab)
        {
            GameObject gameObject = _assetProvider.Instantiate(prefab);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject hero)
        {
            foreach (var loader in hero.GetComponentsInChildren<ILoadProgress>())
                RegisterSaveLoadItems(loader);
        }

        public void RegisterSaveLoadItems(ILoadProgress loader)
        {
            if (loader is ISaveProgress saver)
                ProgressSavers.Add(saver);

            ProgressLoaders.Add(loader);
        }
    }
}