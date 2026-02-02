using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace Assets.Project._Develop.Runtime.Utilities.ConfigsManagment
{
    public class ConfigsProviderService 
    {
        private readonly Dictionary<Type, Dictionary<string, object>> _configs = new();

        private readonly IConfigsLoader[] _loaders;

        public ConfigsProviderService(params IConfigsLoader[] loaders)
        {
            _loaders = loaders;
        }

        public T GetConfig<T>() where T : class
        {
            if (_configs.ContainsKey(typeof(T)) == false)
                throw new InvalidOperationException($"Not found config by {typeof(T)}");

            if (_configs[typeof(T)].Count > 1)
                throw new InvalidOperationException($"Cant choose nessesary config {typeof(T)}, Its more then 1");

            foreach (var pair in _configs[typeof(T)])
                return (T)pair.Value;

            throw new InvalidOperationException("Dictionary is empty");
        }

        public T GetConfigBy<T>(string configName) where T : class
        {
            if (_configs.TryGetValue(typeof(T), out var typedConfigs) == false)
                throw new InvalidOperationException($"Configs of type {typeof(T)} not loaded");

            if (typedConfigs.TryGetValue(configName, out var config) == false)
                throw new InvalidOperationException($"Config {typeof(T)} with id '{configName}' not found");

            return (T)config;
        }

        public IEnumerator LoadAsync()
        {
            _configs.Clear();

            foreach (IConfigsLoader loader in _loaders)
                yield return loader.LoadMultiAsync(loadedConfigs => _configs.AddRange(loadedConfigs));
        }
    }
}