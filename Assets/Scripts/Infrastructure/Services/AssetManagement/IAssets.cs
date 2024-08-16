using UnityEngine;

namespace Infrastructure.Services.AssetManagement
{
    public interface IAssets : IService
    {
        T Instantiate<T>(string path) where T : Object;
        T Instantiate<T>(string path, Transform parent) where T : Object;
    }
}