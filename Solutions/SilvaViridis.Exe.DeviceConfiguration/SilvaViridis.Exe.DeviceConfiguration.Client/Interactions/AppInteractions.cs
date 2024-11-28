using ReactiveUI;
using SilvaViridis.Exe.DeviceConfiguration.Client.Interactions.Enums;
using System;
using System.Reactive;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.Interactions
{
    public class AppInteractions
    {
        public AppInteractions()
        {
            Exit = new(RxApp.MainThreadScheduler);
            ChangeLanguage = new();
            ChangeTheme = new();
            FatalException = new(RxApp.MainThreadScheduler);
            ToggleDebug = new();
        }

        public Interaction<Unit, Unit> Exit { get; }

        public Interaction<AvailableLanguages, Unit> ChangeLanguage { get; }

        public Interaction<AvailableThemes, Unit> ChangeTheme { get; }

        public Interaction<Exception, Unit> FatalException { get; }

        public Interaction<Unit, Unit> ToggleDebug { get; }
    }
}
