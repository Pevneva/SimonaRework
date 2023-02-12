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

        public event Action Happened;

        private void Start() => 
            _health.HealthChanged += CheckDeath;

        private void OnDestroy() => 
            _health.HealthChanged -= CheckDeath;

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
            
            Happened?.Invoke();
            Invoke(nameof(DestroyEnemy), 1);
        }

        private void DestroyEnemy() => 
            Destroy(gameObject);
    }
}