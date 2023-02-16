using System.Linq;
using CodeBase.Hero;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private LayerMask _layer;
        
        public float AttackCooldown = 0.1f;
        public float Radius = 1f;
        public float Damage = 10f;

        private Transform _heroTransform;
        private float _cooldownCounter;
        private bool _isAttacking;
        private Collider2D[] _hits = new Collider2D[1];
        private bool _attackIsActive;

        private void Update()
        {
            UpdateCooldown();
            
            if (CanAttack())
                StartAttack();
        }

        public void OnAttack()
        {
            if (Hit(out Collider2D hit)) 
                hit.gameObject.GetComponent<HeroHealth>().TakeDamage(Damage);
        }

        public void OnAttackEnded()
        {
            _cooldownCounter = AttackCooldown;
            _isAttacking = false;
        }

        public void ActiveAttack() => _attackIsActive = true;

        public void DisableAttack() => _attackIsActive = false;


        private bool Hit(out Collider2D hit)
        {
            int hitsCount = Physics2D.OverlapCircleNonAlloc(transform.position, Radius, _hits, _layer);
            hit = _hits.FirstOrDefault();
            return hitsCount > 0;
        }

        private void UpdateCooldown()
        {
            if (CooldownIsUp() == false)
                _cooldownCounter -= Time.deltaTime;
        }

        private bool CooldownIsUp() =>
            _cooldownCounter <= 0;

        private bool CanAttack() => 
            _attackIsActive && _isAttacking == false && CooldownIsUp();

        private void StartAttack()
        {
            _isAttacking = true;
            _animator.PlayAttack();
        }
    }
}