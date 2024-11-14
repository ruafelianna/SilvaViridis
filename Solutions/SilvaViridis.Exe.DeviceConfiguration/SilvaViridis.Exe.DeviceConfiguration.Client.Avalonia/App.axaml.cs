using Avalonia;
using Avalonia.Markup.Xaml;
using HanumanInstitute.MvvmDialogs.Avalonia;
using HanumanInstitute.MvvmDialogs.Avalonia.MessageBox;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels;
using System;

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

            var vm = new MainViewModel();

            dialogService.Show(null, vm);
        }
    }
}
