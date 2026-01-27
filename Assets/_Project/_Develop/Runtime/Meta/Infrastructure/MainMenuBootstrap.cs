using System.Collections;
using Assets.Project._Develop.Runtime.Infrastructure;
using Assets.Project._Develop.Runtime.Infrastructure.DI;
using Assets.Project._Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace Assets.Project._Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIConteiner _conteiner;

        public override void ProcessRegistrations(DIConteiner conteiner, IInputSceneArgs inputSceneArgs = null)
        {
            _conteiner = conteiner;

            MainMenuContextRegistrations.Process(_conteiner);
        }

        public override IEnumerator Initiaize()
        {
            Debug.Log("Инициализация меню сцены");

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Старт муню сцены");
        }
    }
}