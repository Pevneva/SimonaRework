using CodeBase.Data;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.SaveLoad;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Hero
{
    public class HeroMover : MonoBehaviour, ISaveProgress
    {
        [SerializeField] private Rigidbody2D _rigidBody;
        [SerializeField] private float _speed = 3f;
        [SerializeField] private float _jumpForce = 10f;
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private HeroAnimator _animator;
        [SerializeField] private GroundDetecter _groundDetecter;

        private IInputService _inputService;
        private Vector3 _movementVector;

        public Vector3 MovementVector => _movementVector;
        
        private void Awake() => 
            _inputService = AllServices.Container.Single<IInputService>();

        private void Update()
        {
            _movementVector = Vector3.zero;
            
            if (IsInputting())
                _movementVector = SetMovementVector();

            Move(_movementVector);

            if (_groundDetecter.IsGrounded() == false)
                return;

            if (_inputService.IsJumpButtonUp)
                Jump();
        }

        private bool IsInputting() => 
            _inputService.Axis.sqrMagnitude > Constants.Epsilon;

        private void Move(Vector3 movementVector)
        {
            _animator.PlayEndJump(_groundDetecter.IsGrounded());
            transform.Translate(movementVector * (Time.deltaTime * _speed));
        }

        private Vector3 SetMovementVector()
        {
            Vector3 movementVector;
            movementVector = Camera.main.transform.TransformDirection(_inputService.Axis);
            movementVector.y = 0;
            movementVector.z = 0;
            movementVector.Normalize();

            Rotate(movementVector);
            
            return movementVector;
        }

        private void Rotate(Vector3 movementVector) => 
            _sprite.flipX = movementVector.x < 0;

        private void Jump()
        {
            _animator.PlayStartJump();
            _rigidBody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }

        public void Load(PlayerProgress playerProgress)
        {
            if (CurrentLevel() == playerProgress.WorldData.PositionOnLevel.Level)
            {
                Vector3Data savedData = playerProgress.WorldData.PositionOnLevel.Position;
                if (savedData != null) 
                    SetPosition(savedData);
            }
        }

        private void SetPosition(Vector3Data savedData)
        {
            _rigidBody.isKinematic = true;
            transform.position = savedData.AsVectorUnity().AddY(1);
            _rigidBody.isKinematic = false;
        }

        public void Save(PlayerProgress playerProgress)
        {
            playerProgress.WorldData.PositionOnLevel =
                new PositionOnLevel(CurrentLevel(), transform.position.AsVectorData());
        }

        private static string CurrentLevel() => 
            SceneManager.GetActiveScene().name;
    }
}