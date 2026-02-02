using System;

namespace Assets.Project._Develop.Runtime.Gameplay
{
    public class NotMatchingCondition : IGameCondition
    {
        public event Action Triggered;

        private Typer _typer;
        private string _generatedString;

        public NotMatchingCondition(Typer typer, string generatedString)
        {
            _typer = typer;
            _generatedString = generatedString;
        }

        public void Activate() => _typer.IsTyped += OnMatching;

        public void Dispose() => _typer.IsTyped -= OnMatching;

        private void OnMatching(string typedString)
        {
            if (string.Equals(_generatedString, typedString, StringComparison.Ordinal) == false)
                Triggered?.Invoke();
        }
    }
}