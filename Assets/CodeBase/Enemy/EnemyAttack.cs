using System.Linq;
using CodeBase.Hero;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Factory;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private float _attackCooldown;
        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _layer;
        [SerializeField] private float _effectiveDistance = 0.85f;
        [SerializeField] private float _damage = 10f;

        private IGameFactory _factory;
        private Transform _heroTransform;
        private float _cooldownCounter;
        private bool _isAttacking;
        private Collider2D[] _hits = new Collider2D[1];
        private bool _attackIsActive;

        private void Start()
        {
            _factory = AllServices.Container.Single<IGameFactory>();

            if (_heroTransform != null)
                InitHeroTransform();
            else
                _factory.HeroCreated += InitHeroTransform;
        }

        private void Update()
        {
            UpdateCooldown();
            
            if (CanAttack())
                StartAttack();
        }

        public void OnAttack()
        {
            if (Hit(out Collider2D hit))
            {
                PhysicsDebug.DrawDebug(StartPoint(), 1, 3);
                hit.gameObject.GetComponent<HeroHealth>().TakeDamage(_damage);
            }
        }

        public void OnAttackEnded()
        {
            _cooldownCounter = _attackCooldown;
            _isAttacking = false;
        }

        public void ActiveAttack() => _attackIsActive = true;

        public void DisableAttack() => _attackIsActive = false;


        private bool Hit(out Collider2D hit)
        {
            int hitsCount = Physics2D.OverlapCircleNonAlloc(StartPoint(), _radius, _hits, _layer);
            hit = _hits.FirstOrDefault();
            return hitsCount > 0;
        }

        private Vector3 StartPoint() => 
            transform.position + Vector3.forward * _effectiveDistance;

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

        private void InitHeroTransform() => 
            _heroTransform = _factory.HeroTransform;
    }
}