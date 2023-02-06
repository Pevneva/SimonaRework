using CodeBase.Infrastructure.Services.AssetManagement;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;

        public GameFactory(IAssetProvider assetProvider) => 
            _assetProvider = assetProvider;

        public GameObject CreateHero(GameObject at) =>
            _assetProvider.Instantiate(AssetsPath.HeroPath, at: at.transform.position);

        public void CreateHud() => 
            _assetProvider.Instantiate(AssetsPath.HudPath);
    }
}