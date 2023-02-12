using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroDeath : MonoBehaviour
    {
        [SerializeField] private HeroAnimator _animator;
        [SerializeField] private HeroMover _mover;
        [SerializeField] private HeroHealth _health;

        private bool _isDead;

        private void Start() => _health.HealthChanged += CheckDeath;

        private void OnDestroy() => _health.HealthChanged -= CheckDeath;

        private void CheckDeath()
        {
            if (_isDead == false && _health.Current <= 0)
                Die();
        }

        private void Die()
        {
            _isDead = true;
            _mover.enabled = false;
            _animator.PlayDeath();
        }
    }
}