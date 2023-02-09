using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyPatrol : MonoBehaviour
    {
        [SerializeField] private GameObject _leftBorder;
        [SerializeField] private GameObject _rightBorder;
        [SerializeField] private GroundDetecter _groundDetection;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private EnemyMover _mover;

        private bool _isRightNavigation = true;

        private void Update()
        {
            if (_groundDetection.IsGrounded() == false)
                return;

            _isRightNavigation = IsRightDirection();

            Move(_isRightNavigation ? Vector2.right : Vector2.left);
        }

        private bool IsRightDirection()
        {
            if (IsRightBorderReached())
                return false;
            else if (IsLeftBorderReached())
                return true;

            return _isRightNavigation;
        }

        private bool IsRightBorderReached() => 
            transform.position.x >= _rightBorder.transform.position.x;

        private bool IsLeftBorderReached() => 
            transform.position.x <= _leftBorder.transform.position.x;

        private void Move(Vector3 direction)
        {
            Rotate(direction);
            _mover.Move(direction);
            _animator.Move(direction.magnitude);
        }

        private void Rotate(Vector3 direction) => 
            _spriteRenderer.flipX = direction.x >= 0;
    }
}