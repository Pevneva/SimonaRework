using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services.AssetManagement;
using CodeBase.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;

        public List<ILoadProgress> ProgressLoaders { get; } = new List<ILoadProgress>();
        public List<ISaveProgress> ProgressSavers { get; } = new List<ISaveProgress>();

        public Transform HeroTransform { get; private set; }

        public event Action HeroCreated;

        public GameFactory(IAssetProvider assetProvider) =>
            _assetProvider = assetProvider;

        public GameObject CreateHero(GameObject at)
        {
            GameObject hero = InstantiateRegistered(AssetsPath.HeroPath, at: at.transform.position);
            HeroTransform = hero.transform;
            HeroCreated?.Invoke();
            return hero;
        }

        public void CreateHud() =>
            InstantiateRegistered(AssetsPath.HudPath);

        public void Cleanup()
        {
            ProgressLoaders.Clear();
            ProgressSavers.Clear();
        }

        private GameObject InstantiateRegistered(string prefab, Vector3 at)
        {
            GameObject gameObject = _assetProvider.Instantiate(prefab, at: at);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }
        
        private GameObject InstantiateRegistered(string prefab)
        {
            GameObject gameObject = _assetProvider.Instantiate(prefab);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject hero)
        {
            foreach (var loader in hero.GetComponentsInChildren<ILoadProgress>())
                RegisterSaveLoadItems(loader);
        }

        private void RegisterSaveLoadItems(ILoadProgress loader)
        {
            if (loader is ISaveProgress saver)
                ProgressSavers.Add(saver);
            
            ProgressLoaders.Add(loader);
        }
    }
}