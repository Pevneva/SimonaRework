using UnityEngine;

namespace CodeBase.Enemy
{
    public abstract class Mover : MonoBehaviour
    {
        public abstract float Speed { get; set; }
        public abstract Vector3 Direction { get; set; }
        public abstract void Move(Vector3 direction);
    }
}