using CodeBase.Data;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class ChaseHero : Chase
    {
        [SerializeField] private Mover _mover;
        [SerializeField] private EnemyRotation _rotater;

        public float MinimalDistance = 0.95f;
        
        private Transform _heroTransform;
        private Vector3 _direction;

        public void Construct(Transform heroTransform) => _heroTransform = heroTransform;

        private void Update()
        {
            if (HeroNotReached())
            {
                _direction = (_heroTransform.position - transform.position).AsHorizontalMoving();
                
                _mover.Move(_direction);
                _rotater.Rotate(_direction);
            }
        }

        public bool HeroNotReached() =>
            Vector3.Distance(_heroTransform.position, transform.position) >= MinimalDistance;
    }
}