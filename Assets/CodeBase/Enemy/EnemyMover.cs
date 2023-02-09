using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private float _speed;
        
        public void Move(Vector3 direction) => 
            transform.Translate(direction * (Time.deltaTime * _speed));
    }
}