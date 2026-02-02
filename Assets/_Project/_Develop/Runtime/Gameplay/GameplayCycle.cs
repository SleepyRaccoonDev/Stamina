using System;
using System.Collections;
using Assets.Project._Develop.Runtime.Utilities.CoroutinesManagment;
using Assets.Project._Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace Assets.Project._Develop.Runtime.Gameplay
{
    public class GameplayCycle : IDisposable
    {
        private readonly KeyCode SpaceKeyCod = KeyCode.Space;

        private GameModeFactory _gameModeFactory;
        private ICoroutinesPerformer _performer;
        private GameplayConditionsFactory _gameplayConditionsFactory;
        private Typer _typer;
        private SceneSwitcherService _sceneSwitcherService;

        private GameMode _gameMode;

        public GameplayCycle(
            GameModeFactory gameModeFactory,
            ICoroutinesPerformer performer,
            GameplayConditionsFactory gameplayConditionsFactory,
            Typer typer,
            SceneSwitcherService sceneSwitcherService)
        {
            _gameModeFactory = gameModeFactory;
            _performer = performer;
            _gameplayConditionsFactory = gameplayConditionsFactory;
            _typer = typer;
            _sceneSwitcherService = sceneSwitcherService;
        }

        public void Prepare()
        {
            _gameplayConditionsFactory.GenerateNewSequence();
            _gameMode = _gameModeFactory.CreateGameMode();
        }

        public IEnumerator Launch()
        {
            _gameMode.IsWined += OnGameModeIsWined;
            _gameMode.IsDefeated += OnGameModeIsDefeated;

            _performer.StartPerform(_typer.Start());
            _gameMode.Start();

            Debug.Log($"Сгенерированная последовательность - {_gameplayConditionsFactory.GeneratedSequence}");

            yield break;
        }

        public void Dispose()
        {
            _gameMode.IsWined -= OnGameModeIsWined;
            _gameMode.IsDefeated -= OnGameModeIsDefeated;
            
        }

        private void OnGameModeIsDefeated()
        {
            Debug.Log("LOOSE!");
            _performer.StartPerform(EndGameProcessForDefeat());
        }

        private void OnGameModeIsWined()
        {
            Debug.Log("WIN!");
            _performer.StartPerform(EndGameProcessForWin());
        }

        private IEnumerator EndGameProcessForWin()
        {
            Dispose();
            _gameMode.Dispose();
            Debug.Log("Press Space to continue...");
            yield return new WaitUntil(() => Input.GetKeyDown(SpaceKeyCod));
            _performer.StartPerform(_sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu));
        }

        private IEnumerator EndGameProcessForDefeat()
        {
            Dispose();
            Debug.Log("Press Space to continue...");
            yield return new WaitUntil(() => Input.GetKeyDown(SpaceKeyCod));
            Prepare();
            _performer.StartPerform(Launch());
        }
    }
}