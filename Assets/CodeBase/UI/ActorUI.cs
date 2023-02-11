using CodeBase.Hero;
using UnityEngine;

namespace CodeBase.UI
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HpBar _hpBar;
        
        private HeroHealth _heroHealth;

        public void Construct(HeroHealth heroHealth)
        {
            _heroHealth = heroHealth;
            _heroHealth.HealthChanged += HpBarUpdate;
        }

        private void OnDestroy() => _heroHealth.HealthChanged -= HpBarUpdate;

        private void HpBarUpdate() => _hpBar.SetValue(_heroHealth.Current, _heroHealth.Max);
    }
}