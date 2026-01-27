using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Assets.Project._Develop.Runtime.Utilities.SceneManagment
{
    public class SceneLoaderService
    {
        public IEnumerator LoadAsync(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            AsyncOperation wait = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);

            yield return new WaitUntil(() => wait.isDone == false);
        }

        public IEnumerator UnLoadAsync(string sceneName)
        {
            AsyncOperation wait = SceneManager.UnloadSceneAsync(sceneName);

            yield return new WaitUntil(() => wait.isDone == false);
        }
    }
}