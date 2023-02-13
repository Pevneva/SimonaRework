using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Factory;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class ChaseHero : Chase
    {
        private const float MinimalDistance = 0.95f; 

        [SerializeField] public Mover _mover;
        [SerializeField] private EnemyRotation _rotater;

        private Transform _heroTransform;
        private IGameFactory _gameFactory;

        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            if (_gameFactory.HeroTransform != null)
                InitializeHero();
            else
                _gameFactory.HeroCreated += InitializeHero;
        }

        private void Update()
        {
            if (HeroInitialized() && HeroNotReached())
            {
                Vector3 direction = _heroTransform.position - transform.position;
                _mover.Move(direction);
                _rotater.Rotate(direction);
            } 
        }

        public bool HeroInitialized() => _heroTransform != null;

        public bool HeroNotReached() => 
            Vector3.Distance(_heroTransform.position, transform.position) >= MinimalDistance;

        private void InitializeHero() => _heroTransform = _gameFactory.HeroTransform;
    }
}