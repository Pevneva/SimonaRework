using System.Collections.Generic;
using System.Linq;
using CodeBase.Infrastructure.Services.AssetManagement;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factory
{
    public class ObjectPool
    {
        private readonly List<GameObject> _pool = new List<GameObject>();
        private Transform _parent;

        public void Initialize(IAssetProvider assetProvider, string prefab, Transform container, int capacity)
        {
            _parent = container;
            for (int i = 0; i < capacity; i++)
            {
                GameObject objectPool = assetProvider.Instantiate(prefab, container.position);
                objectPool.SetActive(false);

                _pool.Add(objectPool);
            }
        }

        public bool TryGetObject(out GameObject result)
        {
            result = _pool.FirstOrDefault(t => t.activeSelf == false);
            
            if (result != null) 
                result.transform.position = _parent.position;
            
            return result != null;
        }
    }
}