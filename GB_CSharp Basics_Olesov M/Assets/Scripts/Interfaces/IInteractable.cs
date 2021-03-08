namespace BallGame
{
    public interface IInteractable : IActionable, IInitializable
    {
        bool IsInteractable { get; }
    }
}
