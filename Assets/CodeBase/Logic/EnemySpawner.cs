using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Logic
{
    public class EnemySpawner : MonoBehaviour, ISaveProgress
    {
        [SerializeField] private MonsterTypeId _typeId;

        public bool IsSlain;
        
        private string _id;

        private void Awake()
        {
            _id = GetComponent<UniqueId>().Id;
        }

        public void Load(PlayerProgress playerProgress)
        {
            IsSlain = playerProgress.KillData.ClearedSpawners.Contains(_id);
            if (IsSlain == false)
                Spawn();
        }

        private void Spawn()
        {
        }

        public void Save(PlayerProgress playerProgress)
        {
            if (IsSlain)
                playerProgress.KillData.ClearedSpawners.Add(_id);
        }
    }
}