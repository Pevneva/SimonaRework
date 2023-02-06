﻿using UnityEngine;

namespace CodeBase.Infrastructure
{
    public interface IAssetProvider
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
    }
}