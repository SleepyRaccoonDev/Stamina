using Assets.Project._Develop.Runtime.Infrastructure.DI;
using UnityEngine;

namespace Assets.Project._Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayContextRegistrations
    {
        public static void Process(DIConteiner conteiner, GameplayInputArgs args)
        {
            Debug.Log("Services regiatration process in gameplay scene");
        }
    }
}