using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SilvaViridis.Common.Localization.Abstractions;
using SilvaViridis.Components;
using System;
using System.Reactive;
using System.Threading.Tasks;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels
{
    public partial class OneActionViewModel : ViewModelBase
    {
        public OneActionViewModel(
            IObservable<string> header,
            Action action
        )
        {
            Init(header, out _headerHelper);
            CmdExecute = ReactiveCommand.Create(action);
        }

        public OneActionViewModel(
            IObservable<string> header,
            Func<Task> task
        )
        {
            Init(header, out _headerHelper);
            CmdExecute = ReactiveCommand.CreateFromTask(task);
        }

        public OneActionViewModel(
            ITranslationUnit header,
            Action action
        ) : this(header.ValueObservable, action)
        {
        }

        public OneActionViewModel(
        ITranslationUnit header,
            Func<Task> task
        ) : this(header.ValueObservable, task)
        {
        }

        [ObservableAsProperty]
        private string _header = null!;

        public ReactiveCommand<Unit, Unit> CmdExecute { get; }

        private void Init(
            IObservable<string> header,
            out ObservableAsPropertyHelper<string> headerHelper
        ) => headerHelper = header.ToProperty(this, vm => vm.Header);
    }
}
