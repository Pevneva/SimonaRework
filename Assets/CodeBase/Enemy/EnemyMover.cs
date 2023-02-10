using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyMover : Mover
    {
        [SerializeField] private float _speed;

        public override float Speed => _speed;
        public override Vector3 Direction { get; set; }

        public override void Move(Vector3 direction)
        {
            Direction = direction;
            transform.Translate(direction * (Time.deltaTime * _speed));
        }
    }
}