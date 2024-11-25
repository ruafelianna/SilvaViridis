using ReactiveUI;
using SilvaViridis.Components;
using SilvaViridis.Exe.DeviceConfiguration.Client.Interactions;
using System.Reactive;
using System.Reactive.Linq;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels
{
    public class ExitViewModel : ViewModelBase
    {
        public ExitViewModel(AppInteractions appInteractions)
        {
            CmdExit = ReactiveCommand
                .CreateFromTask(
                    async () => await appInteractions
                        .Exit
                        .Handle(Unit.Default)
                );
        }

        public ReactiveCommand<Unit, Unit> CmdExit { get; }
    }
}
