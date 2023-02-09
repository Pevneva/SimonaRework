using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyRotation : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        public void Rotate(Vector3 direction) => 
            _spriteRenderer.flipX = direction.x >= 0;
    }
}