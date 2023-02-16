using CodeBase.Infrastructure.Services.AssetManagement;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factory
{
    public class ArrowFactory : IArrowFactory
    {
        private const int Capacity = 20;
        
        private readonly ObjectPool _pool = new ObjectPool();
        private readonly IAssetProvider _assetProvider;

        public ArrowFactory(IAssetProvider assetProvider) => 
            _assetProvider = assetProvider;

        public void InitializePool(Transform parent) => 
            _pool.Initialize(_assetProvider, AssetsPath.ArrowPath, parent, Capacity);
        
        public GameObject CreateArrow(Transform parent)
        {
            if (_pool.TryGetObject(out GameObject arrow))
            {
                arrow.transform.parent = parent;
                return arrow;
            }

            return _assetProvider.Instantiate(AssetsPath.ArrowPath, parent.position);
        }
    }
}