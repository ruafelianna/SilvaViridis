using ReactiveUI.SourceGenerators;
using SilvaViridis.Components;
using SilvaViridis.Components.Extensions;
using System;
using System.Threading.Tasks;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces
{
    public partial class CreateBatchViewModel : SavableViewModel
    {
        public CreateBatchViewModel(
            Func<Task> saveCallback,
            Func<Task> cancelCallback
        ) : base(saveCallback, cancelCallback)
        {
            this.RuleNotNullOrWhiteSpace(vm => vm.Name);
        }

        [Reactive]
        public string? _name;
    }
}
