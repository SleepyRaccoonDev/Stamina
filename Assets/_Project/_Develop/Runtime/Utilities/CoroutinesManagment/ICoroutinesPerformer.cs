using System.Collections;
using UnityEngine;

namespace Assets.Project._Develop.Runtime.Utilities.CoroutinesManagment
{
    public interface ICoroutinesPerformer
    {
        Coroutine StartPerform(IEnumerator coroutine);

        void StopPerform(IEnumerator coroutine);
    }
}

