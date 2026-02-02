using UnityEngine;

namespace Assets.Project._Develop.Runtime.Utilities.AssetsManagment
{
    public class ResourcesAssetsLoader
    {
        public T Load<T>(string path) where T : Object
            => Resources.Load<T>(path);

        public T[] LoadAll<T>(string path) where T : Object
            => Resources.LoadAll<T>(path);
    }
}