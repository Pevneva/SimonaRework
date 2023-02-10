using UnityEngine;

namespace CodeBase.Enemy
{
    public abstract class Mover : MonoBehaviour
    {
        public abstract void Move(Vector3 direction);
        public abstract float Speed { get;  }
        public abstract Vector3 Direction { get; set; }
    }
}