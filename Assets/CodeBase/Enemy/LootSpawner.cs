using CodeBase.Data;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.Randomizer;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyDeath _enemyDeath;

        private const float SpawningHeight = 0.8f;
        
        private IGameFactory _factory;
        private int _lootMin;
        private int _lootMax;
        private IRandomService _random;

        public void Construct(IGameFactory gameFactory, IRandomService random)
        {
            _factory = gameFactory;
            _random = random;
        }

        private void Start() => 
            _enemyDeath.Happened += SpawnLoot;

        public void SetLoot(int min, int max)
        {
            _lootMin = min;
            _lootMax = max;
        }

        private void SpawnLoot()
        {
            if (_enemyDeath != null)
                _enemyDeath.Happened -= SpawnLoot;

            LootPiece lootPiece = _factory.CreateLoot();
            lootPiece.transform.position = transform.position.AddY(SpawningHeight);

            var lootItem = GenerateLoot();
            lootPiece.Initialize(lootItem);
        }

        private Loot GenerateLoot()
        {
            return new Loot
            {
                Value = _random.Next(_lootMin, _lootMax)
            };
        }
    }
}