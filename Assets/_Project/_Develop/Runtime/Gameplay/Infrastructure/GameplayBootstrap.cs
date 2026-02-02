using UnityEngine;
using System.Collections;
using Assets.Project._Develop.Runtime.Infrastructure;
using Assets.Project._Develop.Runtime.Infrastructure.DI;
using Assets.Project._Develop.Runtime.Utilities.SceneManagment;
using System;
using Assets.Project._Develop.Runtime.Utilities.CoroutinesManagment;

namespace Assets.Project._Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayBootstrap : SceneBootstrap
    {
        private DIConteiner _conteiner;
        private GameplayInputArgs _inputArgs;
        private GameplayCycle _gameplayCycle;

        public override void ProcessRegistrations(DIConteiner projectConteiner, IInputSceneArgs inputSceneArgs = null)
        {
            _conteiner = new DIConteiner(projectConteiner);

            if (inputSceneArgs is not GameplayInputArgs gameplayInputArgs)
                throw new ArgumentException($"{nameof(inputSceneArgs)} is not MatchTargetWeightMask with {typeof(GameplayInputArgs)} type");

            _inputArgs = gameplayInputArgs;

            GameplayContextRegistrations.Process(_conteiner, _inputArgs);
        }

        public override IEnumerator Initiaize()
        {
            Debug.Log("Инициализация геймплейной сцены");

            _gameplayCycle = _conteiner.Resolve<GameplayCycle>();

            _gameplayCycle.Prepare();

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Старт геймплейной сцены");

            _conteiner.Resolve<ICoroutinesPerformer>().StartPerform(_gameplayCycle.Launch());
        }

        private void OnDisable()
        {
            _gameplayCycle.Dispose();
        }
    }
}