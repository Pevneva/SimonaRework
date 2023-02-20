using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Logic
{
    public class EnemySpawner : MonoBehaviour, ISaveProgress
    {
        [SerializeField] private MonsterTypeId _typeId;

        private bool _isSlain;
        private string _id;
        private IGameFactory _gameFactory;
        private GameObject _monster;
        private EnemyDeath _enemyDeath;

        private void Awake()
        {
            _id = GetComponent<UniqueId>().Id;
            _gameFactory = AllServices.Container.Single<IGameFactory>();
        }

        public void Load(PlayerProgress playerProgress)
        {
            _isSlain = playerProgress.KillData.ClearedSpawners.Contains(_id);
            if (_isSlain == false)
                Spawn();
        }

        private void Spawn()
        {
            _monster = _gameFactory.CreateMonster(_typeId, transform);
            _enemyDeath = _monster.GetComponentInChildren<EnemyDeath>();
            _enemyDeath.Happened += Slay;
        }

        private void Slay()
        {
            if (_enemyDeath != null) 
                _enemyDeath.Happened -= Slay;
            
            _isSlain = true;
        }

        public void Save(PlayerProgress playerProgress)
        {
            if (_isSlain)
                playerProgress.KillData.ClearedSpawners.Add(_id);
        }
    }
}