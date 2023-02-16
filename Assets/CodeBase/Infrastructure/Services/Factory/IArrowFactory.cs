using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factory
{
    public interface IArrowFactory : IService
    {
        void InitializePool(Transform parent);
        GameObject CreateArrow(Transform parent);
    }
}