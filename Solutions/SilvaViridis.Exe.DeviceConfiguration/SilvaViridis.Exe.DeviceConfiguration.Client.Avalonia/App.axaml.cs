using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Styling;
using HanumanInstitute.MvvmDialogs;
using HanumanInstitute.MvvmDialogs.Avalonia;
using HanumanInstitute.MvvmDialogs.Avalonia.MessageBox;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using SilvaViridis.Common.Numerics;
using SilvaViridis.Components;
using SilvaViridis.Components.Assets.Translations;
using SilvaViridis.Exe.DeviceConfiguration.Client.Assets.Translations;
using SilvaViridis.Exe.DeviceConfiguration.Client.Interactions;
using SilvaViridis.Exe.DeviceConfiguration.Client.Interactions.Enums;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Dialogs;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Devices;
using SilvaViridis.Interop.Ports.SerialPort;
using System;
using System.Globalization;
using System.Reactive;
using System.Reactive.Linq;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.Avalonia
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);

            OverrideDynamicResources();
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

            var appLogger = loggerFactory.CreateLogger<App>();

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

            var numberRegex = new NumberRegex(
                new NumberFormatInfo
                {
                    NumberDecimalSeparator = ".",
                    NumberGroupSeparator = ",",
                    NumberGroupSizes = [3,],
                },
                NumberStyles.AllowThousands
                | NumberStyles.AllowLeadingSign
                | NumberStyles.AllowLeadingWhite
                | NumberStyles.AllowTrailingWhite
            );

            var connInfoFactory = new AddConnectionInfoFactory<SerialPortContext>(
                numberRegex
            );

            var vm = new MainViewModel(appInteractions, connInfoFactory);

            InitAppInteractions(
                appInteractions,
                dialogService,
                vm,
                appLogger
            );

            _isDebug = true;

            appInteractions.ChangeLanguage
                .Handle(
                    Enum.TryParse<AvailableLanguages>(
                        CultureInfo.CurrentCulture.Name.Replace('-', '_'),
                        out var enumLang
                    )
                        ? enumLang
                        : default
                )
                .Wait();

            dialogService.Show(null, vm);
        }

        private bool _isDebug;

        private void OverrideDynamicResources()
        {
            Resources["SystemErrorTextColor"]
                = new SolidColorBrush(Colors.Salmon);
        }

        private void InitAppInteractions(
            AppInteractions appInteractions,
            DialogService dialogService,
            ViewModelBase vm,
            ILogger appLogger
        )
        {
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

                    var culture = new CultureInfo(lang);

                    Strings.TranslationProvider.Culture = culture;
                    ValidationStrings.TranslationProvider.Culture = culture;

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

            appInteractions.FatalException
                .RegisterHandler(async ctx =>
                {
                    var msg = Strings.App_Error_Fatal.Value;

                    appLogger.LogCritical(ctx.Input, "{msg}", msg);

                    var ex = _isDebug ? ctx.Input : null;

                    await dialogService
                        .ShowDialogHostAsync(
                            vm,
                            new()
                            {
                                Content = new DialogErrorViewModel(
                                    Strings.Dialog_Error.Value!,
                                    msg!,
                                    ex
                                ),
                                OverlayBackground = new SolidColorBrush(
                                    Colors.Black,
                                    0.99
                                ),
                            }
                        );

                    await appInteractions.Exit
                        .Handle(Unit.Default);

                    ctx.SetOutput(Unit.Default);
                });

            appInteractions.ToggleDebug
                .RegisterHandler(ctx => {
                    _isDebug = !_isDebug;

                    ctx.SetOutput(Unit.Default);
                });
        }
    }
}
