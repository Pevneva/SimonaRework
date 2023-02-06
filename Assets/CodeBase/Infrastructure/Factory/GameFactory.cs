using CodeBase.Infrastructure.AssetManagment;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;

        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public GameObject CreateHero(GameObject at) =>
            _assetProvider.Instantiate(AssetsPath.HeroPath, at: at.transform.position);

        public void CreateHud() => _assetProvider.Instantiate(AssetsPath.HudPath);
    }
}