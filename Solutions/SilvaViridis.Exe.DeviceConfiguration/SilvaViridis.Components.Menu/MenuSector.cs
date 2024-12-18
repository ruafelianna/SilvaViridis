using DynamicData;
using ReactiveUI;
using SilvaViridis.Components.Generators;
using SilvaViridis.Components.Menu.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

namespace SilvaViridis.Components.Menu
{
    public partial class MenuSector : MenuItem, IMenuSector
    {
        public MenuSector(
            int sortKey,
            IEnumerable<IMenuItem>? items = null
        ) : base(sortKey)
            => Init(out _menuItemsCache, out _menuItems, items);

        public MenuSector(
            int sortKey,
            string guid,
            IEnumerable<IMenuItem>? items = null
        ) : base(sortKey, guid)
            => Init(out _menuItemsCache, out _menuItems, items);

        [SourceCache(KeyTypeName = nameof(String))]
        private readonly ReadOnlyObservableCollection<IMenuItem> _menuItems;

        public void AddMenuItem(IMenuItem item)
            => _menuItemsCache.AddOrUpdate(item);

        public void AddMenuItems(IEnumerable<IMenuItem> items)
            => _menuItemsCache.AddOrUpdate(items);

        public void RemoveMenuItem(IMenuItem item)
            => _menuItemsCache.Remove(item);

        public void RemoveMenuItems(IEnumerable<IMenuItem> items)
            => _menuItemsCache.Remove(items);

        private static void Init(
            out SourceCache<IMenuItem, string> menuItemsCache,
            out ReadOnlyObservableCollection<IMenuItem> menuItems,
            IEnumerable<IMenuItem>? items
        )
        {
            menuItemsCache = new(menuItem => menuItem.Guid);

            menuItemsCache
                .Connect()
                .AutoRefresh(menuItem => menuItem.IsVisible)
                .Filter(menuItem => menuItem.IsVisible)
                .SortBy(menuItem => menuItem.SortKey)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out menuItems)
                .Subscribe();

            if (items is not null)
            {
                foreach (var item in items)
                {
                    menuItemsCache.AddOrUpdate(item);
                }
            }
        }
    }
}
