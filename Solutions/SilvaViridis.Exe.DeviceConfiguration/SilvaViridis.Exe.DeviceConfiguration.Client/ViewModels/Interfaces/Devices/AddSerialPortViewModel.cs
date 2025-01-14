using DynamicData;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SilvaViridis.Common.Numerics;
using SilvaViridis.Components;
using SilvaViridis.Components.Extensions;
using SilvaViridis.Exe.DeviceConfiguration.Client.Assets.Translations;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Devices.Abstractions;
using SilvaViridis.Interop.Ports.SerialPort.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Devices
{
    public partial class AddSerialPortViewModel<TSerialPortContext> :
        ValidatableViewModelBase,
        IAddConnectionInfo,
        IAddSerialPortViewModel
        where TSerialPortContext : ISerialPortContext
    {
        public AddSerialPortViewModel(NumberRegex numberRegex)
        {
            var portsCache = new SourceCache<ISerialPortName, string>(x => x.Name);

            portsCache
                .Connect()
                .SortBy(x => x.Name)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out var ports)
                .Subscribe();

            async Task refreshPorts()
            {
                var result = await TSerialPortContext.Ports;
                using var suspend = portsCache.SuspendNotifications();
                portsCache.Clear();
                portsCache.AddOrUpdate(
                    result.Select(name => new SerialPortNameViewModel(name))
                );
            }

            AvailablePorts = ports;

            RefreshPorts = ReactiveCommand.CreateFromTask(refreshPorts);

            AddressesStartingWith_Str = "1";

            _addressesQuantityHelper = this
                .WhenAnyValue(vm => vm.AddressesQuantity_Str)
                .Select(str => str.AsInt(numberRegex))
                .ToProperty(this, vm => vm.AddressesQuantity);

            _addressesStartingWithHelper = this
                .WhenAnyValue(vm => vm.AddressesStartingWith_Str)
                .Select(str => str.AsByte(numberRegex))
                .ToProperty(this, vm => vm.AddressesStartingWith);

            this.RuleNotNull(vm => vm.SelectedPort);

            var addAddresses = this
                .WhenAnyValue(vm => vm.AddAddresses);

            this.RuleNotNullOrWhiteSpace(
                vm => vm.AddressesStartingWith_Str,
                addAddresses
            );

            this.RuleShouldBeAnIntNumber(
                vm => vm.AddressesStartingWith_Str,
                numberRegex,
                addAddresses
            );

            this.RuleShouldBeMoreOrEqual(
                vm => vm.AddressesStartingWith_Str,
                Observable.Return(_minAddress),
                numberRegex,
                addAddresses
            );

            this.RuleShouldBeLessOrEqual(
                vm => vm.AddressesStartingWith_Str,
                Observable.Return(_maxAddress),
                numberRegex,
                addAddresses
            );

            this.RuleNotNullOrWhiteSpace(
                vm => vm.AddressesQuantity_Str,
                addAddresses
            );

            this.RuleShouldBeAnIntNumber(
                vm => vm.AddressesQuantity_Str,
                numberRegex,
                addAddresses
            );

            this.RuleShouldBeMoreOrEqual(
                vm => vm.AddressesQuantity_Str,
                Observable.Return(_minQuantity),
                numberRegex,
                addAddresses
            );

            this.RuleShouldBeLessOrEqual(
                vm => vm.AddressesQuantity_Str,
                vm => vm.AddressesQuantity,
                vm => vm.AddressesStartingWith,
                (quantity, starting) => quantity + starting,
                Observable.Return(_maxQuantity),
                Strings.AddConnection_StartingWith_Quantity_Constraint.ValueObservable,
                addAddresses
            );
        }

        public IEnumerable<ISerialPortName> AvailablePorts { get; }

        [Reactive]
        private ISerialPortName? _selectedPort = null!;

        [Reactive]
        private bool _addAddresses;

        [Reactive]
        private string? _addressesQuantity_Str;

        [Reactive]
        private string? _addressesStartingWith_Str;

        [ObservableAsProperty]
        private int? _addressesQuantity;

        [ObservableAsProperty]
        private byte? _addressesStartingWith;

        public ReactiveCommand<Unit, Unit> RefreshPorts { get; }

        private const int _minQuantity = 1;

        private const int _maxQuantity = byte.MaxValue + 1;

        private const byte _minAddress = byte.MinValue;

        private const byte _maxAddress = byte.MaxValue;
    }
}
