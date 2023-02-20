using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyPatrol : MonoBehaviour
    {
        [SerializeField] private GameObject _leftBorder;
        [SerializeField] private GameObject _rightBorder;
        [SerializeField] private GroundDetecter _groundDetection;
        [SerializeField] private EnemyMover _mover;
        [SerializeField] private ChaseHero _chase;
        [SerializeField] private EnemyRotation _rotation;

        private bool _isRightNavigation = true;

        private Vector2 Direction => _isRightNavigation ? Vector2.right : Vector2.left;
        
        private void Update()
        {
            if (_chase.enabled)
                return;
            
            if (_groundDetection.IsGrounded() == false)
                return;

            _isRightNavigation = IsRightDirection();

            _rotation.Rotate(Direction);
            _mover.Move(Direction);
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
    }
}