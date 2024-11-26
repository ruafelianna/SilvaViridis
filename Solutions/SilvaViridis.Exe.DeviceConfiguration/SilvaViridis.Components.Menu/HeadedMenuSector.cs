using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SilvaViridis.Components.Menu.Abstractions;
using System;
using System.Collections.Generic;

namespace SilvaViridis.Components.Menu
{
    public partial class HeadedMenuSector : MenuSector, IHeadedMenuSector
    {
        public HeadedMenuSector(
            int sortKey,
            IObservable<string> header,
            IEnumerable<IMenuItem>? items = null
        ) : base(sortKey, items)
            => Init(header, out _headerHelper);

        public HeadedMenuSector(
            int sortKey,
            string guid,
            IObservable<string> header,
            IEnumerable<IMenuItem>? items = null
        ) : base(sortKey, guid, items)
            => Init(header, out _headerHelper);

        [ObservableAsProperty]
        public string _header = null!;

        private void Init(
            IObservable<string> header,
            out ObservableAsPropertyHelper<string> headerHelper
        ) => headerHelper = header.ToProperty(this, vm => vm.Header);
    }
}
