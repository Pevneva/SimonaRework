using UnityEngine;

namespace CodeBase.Arrow
{
    public class ArrowMover : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;

        private Vector3 _direction;
        
        public void Construct(bool isSpriteFlipX) => 
            _direction = isSpriteFlipX ? Vector3.left : Vector3.right;

        private void Update() => 
            transform.Translate(_direction * (Time.deltaTime * _speed));
    }
}