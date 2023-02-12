using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(GameObject at);
        GameObject CreateHud();
        void Cleanup();
        List<ILoadProgress> ProgressLoaders { get; }
        List<ISaveProgress> ProgressSavers { get; }
        Transform HeroTransform { get; }
        event Action HeroCreated;
        void CreateArrow(GameObject hero);
    }
}