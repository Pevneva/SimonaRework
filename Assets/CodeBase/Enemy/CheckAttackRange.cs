using CodeBase.Hero;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class CheckAttackRange : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private EnemyAttack _enemyAttack;

        private void Start()
        {
            _triggerObserver.TriggerEnter += SwitchOnAttack;
            _triggerObserver.TriggerExit += SwitchOffAttack;
            
            SwitchOffAttack(null);
        }

        private void SwitchOnAttack(Collider2D obj) => _enemyAttack.ActiveAttack();
        private void SwitchOffAttack(Collider2D obj) => _enemyAttack.DisableAttack();
    }
}