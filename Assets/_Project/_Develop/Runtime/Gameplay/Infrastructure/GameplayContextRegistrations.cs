using Assets.Project._Develop.Runtime.Gameplay.Configs;
using Assets.Project._Develop.Runtime.Infrastructure.DI;
using Assets.Project._Develop.Runtime.Utilities.CoroutinesManagment;
using Assets.Project._Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace Assets.Project._Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayContextRegistrations
    {
        private static GameplayConditionsConfig _gameplayConditionsConfig;

        public static void Process(DIConteiner conteiner, GameplayInputArgs args)
        {
            Debug.Log("Services regiatration process in gameplay scene");

            _gameplayConditionsConfig = args.Configs;

            conteiner.RegisterAsSingle(CreateSymbolsGenerator);
            conteiner.RegisterAsSingle(CreateTyper);
            conteiner.RegisterAsSingle(CreateGameplayConditionsFactory);
            conteiner.RegisterAsSingle(CreateGameModeFactory);
            conteiner.RegisterAsSingle(CreateGameplayCycle);
        }

        private static SymbolsGenerator CreateSymbolsGenerator(DIConteiner c)
        {
            return new SymbolsGenerator(_gameplayConditionsConfig);
        }

        private static Typer CreateTyper(DIConteiner c)
        {
            return new Typer(_gameplayConditionsConfig);
        }

        private static GameplayConditionsFactory CreateGameplayConditionsFactory(DIConteiner c)
        {
            Typer typer = c.Resolve<Typer>();
            SymbolsGenerator symbolsGenerator = c.Resolve<SymbolsGenerator>();

            return new GameplayConditionsFactory(typer, symbolsGenerator);
        }

        private static GameModeFactory CreateGameModeFactory(DIConteiner c)
        {
            GameplayConditionsFactory gameplayConditionsFactory = c.Resolve<GameplayConditionsFactory>();
            return new GameModeFactory(gameplayConditionsFactory);
        }

        private static GameplayCycle CreateGameplayCycle(DIConteiner c)
        {
            Typer typer = c.Resolve<Typer>();
            ICoroutinesPerformer performer = c.Resolve<ICoroutinesPerformer>();
            GameplayConditionsFactory gameplayConditionsFactory = c.Resolve<GameplayConditionsFactory>();
            SceneSwitcherService sceneSwitcherService = c.Resolve<SceneSwitcherService>();
            GameModeFactory gameModeFactory = c.Resolve<GameModeFactory>();

            return new GameplayCycle(
                gameModeFactory,
                performer,
                gameplayConditionsFactory,
                typer,
                sceneSwitcherService);
        }
    }
}