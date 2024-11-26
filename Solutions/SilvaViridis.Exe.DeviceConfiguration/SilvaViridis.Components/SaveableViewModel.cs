using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace SilvaViridis.Components
{
    public abstract class SavableViewModel : ValidatableViewModelBase
    {
        public SavableViewModel(
            Func<Task> saveCallback,
            Func<Task> cancelCallback
        )
        {
            CmdSave = ReactiveCommand
                .CreateFromTask(
                    saveCallback,
                    this
                        .WhenAnyValue(vm => vm.HasErrors)
                        .Select(hasErrors => !hasErrors)
                );

            CmdCancel = ReactiveCommand
                .CreateFromTask(cancelCallback);
        }

        public ReactiveCommand<Unit, Unit> CmdSave { get; }

        public ReactiveCommand<Unit, Unit> CmdCancel { get; }
    }
}
