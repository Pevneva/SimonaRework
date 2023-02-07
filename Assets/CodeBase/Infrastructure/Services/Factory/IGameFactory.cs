using System.Collections.Generic;
using CodeBase.Hero;
using CodeBase.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(GameObject at);
        void CreateHud();
        void Cleanup();
        List<ILoadProgress> ProgressLoaders { get; }
        List<ISaveProgress> ProgressSavers { get; }
    }
}