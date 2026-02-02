using Assets.Project._Develop.Runtime.Gameplay;

public class GameplayConditionsFactory
{
    private Typer _typer;
    private SymbolsGenerator _symbolsGenerator;

    public GameplayConditionsFactory(
        Typer typer,
        SymbolsGenerator symbolsGenerator)
    {
        _typer = typer;
        _symbolsGenerator = symbolsGenerator;
    }

    public string GeneratedSequence { get; private set; }

    public void GenerateNewSequence() => GeneratedSequence = _symbolsGenerator.Generate();

    public IGameCondition CreateWinCondition() => new MatchingCondition(_typer, GeneratedSequence);

    public IGameCondition CreateDefeatCondition() => new NotMatchingCondition(_typer, GeneratedSequence);
}