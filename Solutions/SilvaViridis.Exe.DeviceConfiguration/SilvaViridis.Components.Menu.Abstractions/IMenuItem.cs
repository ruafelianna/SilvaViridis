namespace SilvaViridis.Components.Menu.Abstractions
{
    public interface IMenuItem
    {
        string Guid { get; }

        int SortKey { get; set; }
    }
}
