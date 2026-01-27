using UnityEngine;
using System.Collections;
using Assets.Project._Develop.Runtime.Infrastructure;
using Assets.Project._Develop.Runtime.Infrastructure.DI;
using Assets.Project._Develop.Runtime.Utilities.SceneManagment;
using System;

namespace Assets.Project._Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayBootstrap : SceneBootstrap
    {
        private DIConteiner _conteiner;
        private GameplayInputArgs _inputArgs;

        public override void ProcessRegistrations(DIConteiner conteiner, IInputSceneArgs inputSceneArgs = null)
        {
            _conteiner = conteiner;

            if (inputSceneArgs is not GameplayInputArgs gameplayInputArgs)
                throw new ArgumentException($"{nameof(inputSceneArgs)} is not MatchTargetWeightMask with {typeof(GameplayInputArgs)} type");

            _inputArgs = gameplayInputArgs;

            GameplayContextRegistrations.Process(_conteiner, _inputArgs);
        }

        public override IEnumerator Initiaize()
        {
            Debug.Log($"Вы попали на уровень {_inputArgs.LevelNumber}");

            Debug.Log("Инициализация геймплейной сцены");

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Старт геймплейной сцены");
        }
    }
}