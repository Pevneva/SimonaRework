using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Factory;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class ChaseHero : Chase
    {
        private const float MinimalDistance = 0.015f;
        
        [SerializeField] public Mover _mover;
        [SerializeField] private EnemyRotation _rotation;
        
        private Transform _heroTransform;
        private IGameFactory _gameFactory;
        private Vector3 _direction;

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
                Move();
        }

        private void Move()
        {
            _direction = _heroTransform.position - transform.position;
            transform.Translate(_direction * (Time.deltaTime * _mover.Speed));
            _rotation.Rotate(_direction);
        }

        private void InitializeHero() => _heroTransform = _gameFactory.HeroTransform;
        
        private bool HeroInitialized() => _heroTransform != null;
        
        private bool HeroNotReached() => 
            Vector3.Distance(_heroTransform.position, transform.position) >= MinimalDistance;        
    }
}