using ReactiveUI;
using System.Reactive;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.Interactions
{
    public class AppInteractions
    {
        public AppInteractions()
        {
            Exit = new();
            ChangeLanguage = new();
            ChangeTheme = new();
        }

        public Interaction<Unit, Unit> Exit { get; }

        public Interaction<AvailableLanguages, Unit> ChangeLanguage { get; }

        public Interaction<AvailableThemes, Unit> ChangeTheme { get; }
    }
}
