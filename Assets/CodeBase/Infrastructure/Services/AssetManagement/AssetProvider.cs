using UnityEngine;

namespace CodeBase.Infrastructure.Services.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Instantiate(string path) => 
            Object.Instantiate( GetPrefab(path));

        public GameObject Instantiate(string path, Vector3 at) => 
            Object.Instantiate(GetPrefab(path), at, Quaternion.identity);

        private GameObject GetPrefab(string path) => 
            Resources.Load<GameObject>(path);
    }
}