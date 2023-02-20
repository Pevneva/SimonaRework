using System;
using CodeBase.Data;
using TMPro;
using UnityEngine;

namespace CodeBase.UI
{
    public class CoinCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counter;
        
        private WorldData _wordData;

        public void Construct(WorldData worldData)
        {
            _wordData = worldData;
            _wordData.LootData.Changed += UpdateCounter;
        }

        private void Start()
        {
            UpdateCounter();
        }

        private void UpdateCounter()
        {
            _counter.text = _wordData.LootData.Collected.ToString();
        }
    }
}