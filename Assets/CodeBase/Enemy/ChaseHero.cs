using UnityEngine;

namespace CodeBase.Enemy
{
    public class ChaseHero : Chase
    {
        [SerializeField] private Mover _mover;
        [SerializeField] private EnemyRotation _rotater;

        public float MinimalDistance = 0.95f; 
        
        private Transform _heroTransform;

        public void Construct(Transform heroTransform) => _heroTransform = heroTransform;

        private void Update()
        {
            if (HeroNotReached())
            {
                Vector3 direction = _heroTransform.position - transform.position;
                direction = new Vector3(direction.x, 0, 0);
                _mover.Move(direction);
                _rotater.Rotate(direction);
            } 
        }

        public bool HeroNotReached() => 
            Vector3.Distance(_heroTransform.position, transform.position) >= MinimalDistance;
    }
}