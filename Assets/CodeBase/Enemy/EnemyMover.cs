using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyMover : Mover
    {
        public override float Speed { get; set; }

        public override Vector3 Direction { get; set; }

        public override void Move(Vector3 direction)
        {
            Direction = direction;
            transform.Translate(direction * (Time.deltaTime * Speed));
        }
    }
}