using System;
using System.Collections;
using Assets.Project._Develop.Runtime.Infrastructure;
using Assets.Project._Develop.Runtime.Infrastructure.DI;
using Assets.Project._Develop.Runtime.Infrastructure.LoadingScreen;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Project._Develop.Runtime.Utilities.SceneManagment
{
    public class SceneSwitcherService
    {
        private readonly SceneLoaderService _sceneLoaderService;
        private readonly ILoadingScreen _loadingScreen;
        private readonly DIConteiner _projectConteiner;

        public SceneSwitcherService(
            SceneLoaderService sceneLoaderService,
            ILoadingScreen loadingScreen,
            DIConteiner projectConteiner)
        {
            _sceneLoaderService = sceneLoaderService;
            _loadingScreen = loadingScreen;
            _projectConteiner = projectConteiner;
        }

        public IEnumerator ProcessSwitchTo(string sceneName, IInputSceneArgs inputSceneArgs = null)
        {
            _loadingScreen.Show();

            yield return _sceneLoaderService.LoadAsync(Scenes.Empty);
            yield return _sceneLoaderService.LoadAsync(sceneName);

            yield return new WaitForSeconds(.1f);

            SceneBootstrap sceneBootstrap = Object.FindObjectOfType<SceneBootstrap>();

            if (sceneBootstrap == null)
                throw new NullReferenceException($"{typeof(SceneBootstrap)} is null");

            DIConteiner sceneContainer = new DIConteiner(_projectConteiner);

            sceneBootstrap.ProcessRegistrations(sceneContainer, inputSceneArgs);

            yield return sceneBootstrap.Initiaize();

            _loadingScreen.Hide();

            sceneBootstrap.Run();
        }
    }
}