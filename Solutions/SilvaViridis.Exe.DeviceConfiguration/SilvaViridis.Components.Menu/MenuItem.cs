using ReactiveUI.SourceGenerators;
using SilvaViridis.Components.Menu.Abstractions;

namespace SilvaViridis.Components.Menu
{
    public partial class MenuItem : ViewModelBase, IMenuItem
    {
        public MenuItem(int sortKey) :
            this(sortKey, System.Guid.NewGuid().ToString())
        {
        }

        public MenuItem(int sortKey, string guid)
        {
            Guid = guid;
            SortKey = sortKey;
        }

        public string Guid { get; }

        [Reactive]
        private int _sortKey;
    }
}
