using UnityEngine;
using Assets.Project._Develop.Runtime.Infrastructure.DI;

namespace Assets.Project._Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuContextRegistrations
    {
        public static void Process(DIConteiner conteiner)
        {
            Debug.Log("Services regiatration process in Main Menu scene");
        }
    }
}
