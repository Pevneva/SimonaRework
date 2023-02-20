using System.Collections;
using CodeBase.Data;
using TMPro;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class LootPiece : MonoBehaviour
    {
        private const float DelayBeforeLootDestroying = 1.15f;
        
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private GameObject _pickupPopup;
        [SerializeField] private TextMeshPro _lootText;
        
        private Loot _loot;
        private WorldData _worldData;
        private bool _picked;

        public void Construct(WorldData worldData) => 
            _worldData = worldData;

        public void Initialize(Loot loot) => 
            _loot = loot;

        private void OnTriggerEnter2D(Collider2D other) => 
            PickUp();

        private void PickUp()
        {
            if(_picked)
                return;
            
            _picked = true;
            
            UpdateWorldData();
            HideCoin();
            ShowText();

            StartCoroutine(DestroyLoot());
        }

        private void UpdateWorldData() => 
            _worldData.LootData.Collect(_loot);

        private void HideCoin() => 
            _sprite.gameObject.SetActive(false);

        private void ShowText()
        {
            _lootText.text = _loot.Value.ToString();
            _pickupPopup.SetActive(true);
        }

        private IEnumerator DestroyLoot()
        {
            yield return new WaitForSeconds(DelayBeforeLootDestroying);
            Destroy(gameObject);
        }
    }
}