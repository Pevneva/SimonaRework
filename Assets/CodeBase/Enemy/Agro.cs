using CodeBase.Hero;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class Agro : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private Chase _chase;

        private void Start()
        {
            _triggerObserver.TriggerEnter += Chase;
            _triggerObserver.TriggerExit += StopChase;

            _chase.enabled = false;
        }

        private void Chase(Collider2D obj) => _chase.enabled = true;

        private void StopChase(Collider2D obj) => _chase.enabled = false;
    }
}