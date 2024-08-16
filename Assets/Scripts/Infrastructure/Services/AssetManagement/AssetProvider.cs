using UnityEngine;

namespace Infrastructure.Services.AssetManagement
{
    public class AssetProvider : IAssets
    {
        public T Instantiate<T>(string path) where T : Object
        {
            var prefab = Resources.Load<T>(path);
            return Object.Instantiate(prefab);
        }
        
        public T Instantiate<T>(string path, Transform parent) where T : Object
        {
            var prefab = Resources.Load<T>(path);
            return Object.Instantiate(prefab, parent);
        }
    }
}