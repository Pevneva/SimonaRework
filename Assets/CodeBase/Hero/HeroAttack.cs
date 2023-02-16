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

        public void Construct(IInputService inputService, IGameFactory gameFactory)
        {
            _input = inputService;
            _factory = gameFactory;
        }

        private void Update()
        {
            if (_input.IsAttackButtonUp)
                Attack();
        }

        public void OnAttack() => _factory.CreateArrow(gameObject);

        public void OnAttackEnded() { }

        private void Attack() => _animator.PlayAttack();
    }
}