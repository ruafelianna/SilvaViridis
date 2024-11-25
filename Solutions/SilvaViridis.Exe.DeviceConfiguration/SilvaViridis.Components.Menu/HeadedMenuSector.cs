using ReactiveUI.SourceGenerators;
using SilvaViridis.Components.Menu.Abstractions;

namespace SilvaViridis.Components.Menu
{
    public partial class HeadedMenuSector : MenuSector, IHeadedMenuSector
    {
        public HeadedMenuSector(int sortKey, string header) :
            base(sortKey)
            => Init(header);

        public HeadedMenuSector(int sortKey, string guid, string header) :
            base(sortKey, guid)
            => Init(header);

        [Reactive]
        public string _header = null!;

        private void Init(string header)
            => Header = header;
    }
}
