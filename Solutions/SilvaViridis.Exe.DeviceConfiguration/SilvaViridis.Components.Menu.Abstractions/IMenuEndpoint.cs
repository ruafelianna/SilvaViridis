namespace SilvaViridis.Components.Menu.Abstractions
{
    public interface IMenuEndpoint : IMenuItem
    {
        object? Content { get; }
    }
}
