using CodeBase.Enemy;
using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "MonsterStaticData", menuName = "StaticData/Monster", order = 0)]
    public class MonsterStaticData : ScriptableObject
    {
        public MonsterTypeId MonsterTypeId;
        
        [Range(1, 100)] 
        public int Hp = 30;
        
        [Range(1, 30)] 
        public float Damage = 1f;
        
        [Range(0, 5)] 
        public float AttackCooldown = 0.11f;
        
        [Range(1, 10)] 
        public float MoveSpeed = 1;

        [Range(1, 5)] 
        public int MinLoot = 2;
        
        [Range(1, 20)] 
        public int MaxLoot = 7;

        [Range(0.1f, 2)] 
        public float AttackZone = 0.41f;
        
        [Range(0.1f, 2)] 
        public float MinimalChaseDistance = 0.95f;

        public GameObject Prefab;
    }
}