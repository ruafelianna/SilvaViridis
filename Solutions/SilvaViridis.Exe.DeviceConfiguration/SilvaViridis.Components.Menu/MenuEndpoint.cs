using ReactiveUI.SourceGenerators;
using SilvaViridis.Components.Menu.Abstractions;

namespace SilvaViridis.Components.Menu
{
    public partial class MenuEndpoint : MenuItem, IMenuEndpoint
    {
        public MenuEndpoint(
            int sortKey,
            object? content
        ) : base(sortKey)
            => Init(content);

        public MenuEndpoint(
            int sortKey,
            string guid,
            object? content
        ) : base(sortKey, guid)
            => Init(content);

        [Reactive]
        private object? _content;

        private void Init(object? content)
            => Content = content;
    }
}
