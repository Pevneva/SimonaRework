using System;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private EnemyAttack _attack;
        [SerializeField] private EnemyMover _mover;
        [SerializeField] private EnemyPatrol _patrol;
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private CheckAttackRange _checkAttackRange;
        [SerializeField] private ChaseHero _chase;

        private const float DeathTime = 0.7f;

        public event Action Happened;

        private void Start() =>
            _health.HealthChanged += CheckDeath;

        private void OnDestroy()
        {
            if (_health != null)
                _health.HealthChanged -= CheckDeath;
        }

        private void CheckDeath()
        {
            if (_health.Current <= 0)
                Die();
        }

        private void Die()
        {
            _health.HealthChanged -= CheckDeath;

            _animator.PlayDeath();
            _attack.enabled = false;
            _mover.enabled = false;
            _patrol.enabled = false;
            _checkAttackRange.enabled = false;
            _chase.enabled = false;

            Happened?.Invoke();
            Invoke(nameof(DestroyEnemy), DeathTime);
        }

        private void DestroyEnemy() =>
            Destroy(gameObject);
    }
}