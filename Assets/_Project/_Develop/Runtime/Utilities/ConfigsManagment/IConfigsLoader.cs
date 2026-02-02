using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Project._Develop.Runtime.Utilities.ConfigsManagment
{
    public interface IConfigsLoader
    {
        IEnumerator LoadAsync(Action<Dictionary<Type, object>> onConfigsLoaded);
        IEnumerator LoadMultiAsync(Action<Dictionary<Type, Dictionary<string, object>>> onConfigsLoaded);
    }
}