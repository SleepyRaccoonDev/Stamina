using Assets.Project._Develop.Runtime.Utilities.SceneManagment;

namespace Assets.Project._Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayInputArgs : IInputSceneArgs
    {
        public GameplayInputArgs(int levelNumber)
        {
            LevelNumber = levelNumber;
        }

        public int LevelNumber { get; }
    }
}