using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.Input;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroAttack : MonoBehaviour
    {
        [SerializeField] private HeroAnimator _animator;

        private IInputService _input;
        private IGameFactory _factory;

        private void Awake()
        {
            _input = AllServices.Container.Single<IInputService>();
            _factory = AllServices.Container.Single<IGameFactory>();
        }

        private void Update()
        {
            if (_input.IsAttackButtonUp())
                Attack();
        }

        public void OnAttack() => _factory.CreateArrow(gameObject);

        public void OnAttackEnded() { }

        private void Attack() => _animator.PlayAttack();
    }
}