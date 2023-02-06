using System;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroMover : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidBody;
        [SerializeField] private float _speed = 3f;
        [SerializeField] private float _jumpForce = 10f;
        [SerializeField] private TriggerObserver _triggerObserver;

        private IInputService _inputService;
        private Vector3 _movementVector;
        private bool _isGrounded;


        private void Awake() => 
            _inputService = AllServices.Container.Single<IInputService>();

        private void Start()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;
        }
        

        private void TriggerExit(Collider2D obj) => DisableJumping();

        private void EnableJumping() => _isGrounded = true;

        private void TriggerEnter(Collider2D obj) => EnableJumping();

        private void DisableJumping() => _isGrounded = false;

        private void Update()
        {
            _movementVector = Vector3.zero;

            if (IsInputting())
                _movementVector = SetMovementVector();

            Move(_movementVector);

            if (_isGrounded == false)
                return;

            if (_inputService.IsJumpButtonUp)
                Jump();
        }

        private bool IsInputting() => 
            _inputService.Axis.sqrMagnitude > Constants.Epsilon;

        private void Move(Vector3 movementVector) =>
            transform.Translate(movementVector * (Time.deltaTime * _speed));

        private Vector3 SetMovementVector()
        {
            Vector3 movementVector;
            movementVector = Camera.main.transform.TransformDirection(_inputService.Axis);
            movementVector.y = 0;
            movementVector.z = 0;
            movementVector.Normalize();
            return movementVector;
        }

        private void Jump() =>
            _rigidBody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }
}