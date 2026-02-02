using UnityEngine;
using Assets.Project._Develop.Runtime.Infrastructure.DI;
using Assets.Project._Develop.Runtime.Utilities.ConfigsManagment;
using Assets.Project._Develop.Runtime.Utilities.CoroutinesManagment;
using Assets.Project._Develop.Runtime.Utilities.SceneManagment;

namespace Assets.Project._Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuContextRegistrations
    {
        public static void Process(DIConteiner conteiner)
        {
            Debug.Log("Services regiatration process in Main Menu scene");

            conteiner.RegisterAsSingle(CreateGameModeSelector);
        }

        private static GameModeSelector CreateGameModeSelector(DIConteiner c)
        {
            ConfigsProviderService configsProviderService = c.Resolve<ConfigsProviderService>();
            ICoroutinesPerformer coroutinesPerformer = c.Resolve<ICoroutinesPerformer>();
            SceneSwitcherService sceneSwitcherService = c.Resolve<SceneSwitcherService>();

            return new GameModeSelector(
                configsProviderService,
                coroutinesPerformer,
                sceneSwitcherService);
        }
    }
}
