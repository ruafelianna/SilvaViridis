using HanumanInstitute.MvvmDialogs;
using ReactiveUI;
using SilvaViridis.Components;
using System;
using System.Reactive;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Dialogs
{
    public class DialogErrorViewModel :
        ViewModelBase,
        IModalDialogViewModel,
        ICloseable
    {
        public DialogErrorViewModel(
            string header,
            string message,
            Exception? ex = null
        )
        {
            Header = header;
            Message = message;
            Exception = ex;

            CmdClose = ReactiveCommand.Create(
                () => RequestClose?.Invoke(this, EventArgs.Empty)
            );
        }

        public string Header { get; }

        public string Message { get; }

        public Exception? Exception { get; }

        public bool? DialogResult => true;

        public event EventHandler? RequestClose;

        public ReactiveCommand<Unit, Unit> CmdClose { get; }
    }
}
