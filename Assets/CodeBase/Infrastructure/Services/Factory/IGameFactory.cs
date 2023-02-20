using System.Collections.Generic;
using CodeBase.Enemy;
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
        void CreateArrow(GameObject hero);
        void RegisterSaveLoadItems(ILoadProgress loader);
        GameObject CreateMonster(MonsterTypeId typeId, Transform parent);
        LootPiece CreateLoot();
    }
}