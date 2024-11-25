using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using HanumanInstitute.MvvmDialogs.Avalonia;
using HanumanInstitute.MvvmDialogs.Avalonia.MessageBox;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using SilvaViridis.Exe.DeviceConfiguration.Client.Assets.Translations;
using SilvaViridis.Exe.DeviceConfiguration.Client.Interactions;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels;
using System;
using System.Reactive;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.Avalonia
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            var loggerFactory = LoggerFactory
                .Create(builder => builder
                    .AddFilter(logLevel => true)
                    .AddSimpleConsole(options => {
                        options.TimestampFormat = "dd.MM.yyyy HH:mm:ss ";
                        options.IncludeScopes = true;
                        options.SingleLine = false;
                        options.ColorBehavior = LoggerColorBehavior.Enabled;
                        options.UseUtcTimestamp = false;
                    })
                );

            var dialogService = new DialogService(
                new DialogManager(
                    viewLocator: new ViewLocator(),
                    logger: loggerFactory?.CreateLogger<DialogManager>(),
                    dialogFactory: new DialogFactory()
                        .AddDialogHost()
                        .AddMessageBox(MessageBoxMode.Popup)
                )
            );

            GC.KeepAlive(typeof(DialogService));

            var appInteractions = new AppInteractions();

            var vm = new MainViewModel(appInteractions);

            appInteractions.Exit
                .RegisterHandler(ctx => {
                    dialogService.Close(vm);
                    ctx.SetOutput(Unit.Default);
                });

            appInteractions.ChangeLanguage
                .RegisterHandler(ctx => {
                    var lang = ctx.Input
                        .ToString()
                        .Replace('_', '-');

                    Strings.TranslationProvider.Culture
                        = new(lang);

                    ctx.SetOutput(Unit.Default);
                });

            appInteractions.ChangeTheme
                .RegisterHandler(ctx => {
                    RequestedThemeVariant = ctx.Input switch
                    {
                        AvailableThemes.Light
                            => ThemeVariant.Light,
                        AvailableThemes.Dark
                            => ThemeVariant.Dark,
                        _ => ThemeVariant.Default,
                    };

                    ctx.SetOutput(Unit.Default);
                });

            dialogService.Show(null, vm);
        }
    }
}
