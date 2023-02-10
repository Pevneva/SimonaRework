using System.Collections;
using CodeBase.Hero;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class Agro : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private Chase _chase;
        [SerializeField] private float _cooldown;

        private Coroutine _stopChaseCoroutine;
        
        private void Start()
        {
            _triggerObserver.TriggerEnter += Chase;
            _triggerObserver.TriggerExit += StopChase;

            SwitchOffChase();
        }

        private void Chase(Collider2D obj)
        {
            StopAggroCoroutine();

            SwitchOnChase();
        }
        
        private void StopAggroCoroutine()
        {
            if (_stopChaseCoroutine != null)
            {
                StopCoroutine(_stopChaseCoroutine);
                _stopChaseCoroutine = null;
            }
        }
        
        private void SwitchOnChase() => _chase.enabled = true;

        private void StopChase(Collider2D obj) => 
            _stopChaseCoroutine = StartCoroutine(StopChase(_cooldown));

        private IEnumerator StopChase(float cooldown)
        {
            yield return new WaitForSeconds(cooldown);
            SwitchOffChase();
        }

        private void SwitchOffChase() => _chase.enabled = false;
    }
}