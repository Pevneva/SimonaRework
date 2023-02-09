using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyMover : Mover
    {
        [SerializeField] private float _speed;

        public override float Speed => _speed;

        public override void Move(Vector3 direction) => 
            transform.Translate(direction * (Time.deltaTime * _speed));
    }

    public abstract class Mover : MonoBehaviour
    {
        public abstract void Move(Vector3 direction);
        public abstract float Speed { get;  }
    }
}