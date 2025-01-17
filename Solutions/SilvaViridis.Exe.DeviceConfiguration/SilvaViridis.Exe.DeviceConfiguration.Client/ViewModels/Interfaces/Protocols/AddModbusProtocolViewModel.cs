using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SilvaViridis.Common.Numerics;
using SilvaViridis.Components;
using SilvaViridis.Components.Extensions;
using SilvaViridis.Exe.DeviceConfiguration.Client.Assets.Translations;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Protocols.Abstractions;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Protocols.DTOs;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Protocols
{
    public partial class AddModbusRTUProtocolViewModel :
        ValidatableViewModelBase,
        IAddProtocolViewModel
    {
        public AddModbusRTUProtocolViewModel(NumberRegex numberRegex)
        {
            var addAddresses = this
                .WhenAnyValue(vm => vm.AddAddresses);

            #region Start

            AddressesStartingWith_Str = "1";

            _addressesStartingWithHelper = this
                .WhenAnyValue(vm => vm.AddressesStartingWith_Str)
                .Select(str => str.AsByte(numberRegex))
                .ToProperty(this, vm => vm.AddressesStartingWith);

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

            #endregion

            #region Quantity

            _addressesQuantityHelper = this
                .WhenAnyValue(vm => vm.AddressesQuantity_Str)
                .Select(str => str.AsInt(numberRegex))
                .ToProperty(this, vm => vm.AddressesQuantity);

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

            #endregion

            #region Step

            AddressesStep_Str = "1";

            _addressesStepHelper = this
                .WhenAnyValue(vm => vm.AddressesStep_Str)
                .Select(str => str.AsInt(numberRegex))
                .ToProperty(this, vm => vm.AddressesStep);

            this.RuleNotNullOrWhiteSpace(
                vm => vm.AddressesStep_Str,
                addAddresses
            );

            this.RuleShouldBeAnIntNumber(
                vm => vm.AddressesStep_Str,
                numberRegex,
                addAddresses
            );

            this.RuleShouldBeMoreOrEqual(
                vm => vm.AddressesStep_Str,
                Observable.Return(_minStep),
                numberRegex,
                addAddresses
            );

            this.RuleShouldBeLessOrEqual(
                vm => vm.AddressesStep_Str,
                Observable.Return(_maxStep),
                numberRegex,
                addAddresses
            );

            #endregion

            AddCommonRule(vm => vm.AddressesStartingWith_Str, addAddresses);

            AddCommonRule(vm => vm.AddressesQuantity_Str, addAddresses);

            AddCommonRule(vm => vm.AddressesStep_Str, addAddresses);
        }

        [Reactive]
        private bool _addAddresses;

        [Reactive]
        private string? _addressesStartingWith_Str;

        [Reactive]
        private string? _addressesQuantity_Str;

        [Reactive]
        private string? _addressesStep_Str;

        [ObservableAsProperty]
        private byte? _addressesStartingWith;

        [ObservableAsProperty]
        private int? _addressesStep;

        [ObservableAsProperty]
        private int? _addressesQuantity;

        private const byte _minAddress = byte.MinValue;

        private const byte _maxAddress = byte.MaxValue;

        private const int _minQuantity = 1;

        private const int _maxQuantity = byte.MaxValue + 1;

        private const byte _minStep = 1;

        private const byte _maxStep = byte.MaxValue - 1;

        private void AddCommonRule(
            Expression<Func<AddModbusRTUProtocolViewModel, string?>> property,
            IObservable<bool> shouldApply
        ) => this.RuleShouldBeLessOrEqual(
            property,
            vm => vm.AddressesStartingWith,
            vm => vm.AddressesQuantity,
            vm => vm.AddressesStep,
            (starting, quantity, step) => starting + quantity * step,
            Observable.Return(_maxQuantity),
            Strings.AddConnection_StartingWith_Quantity_Step_Constraint.ValueObservable,
            shouldApply
        );

        public IAddProtocolDTO AsDTO()
            => ValidationContext.IsValid
                ? new AddModbusRTUProtocolDTO(
                    AddressesStartingWith ?? 1,
                    AddressesQuantity ?? 0,
                    AddressesStep ?? 1
                )
                : throw new InvalidOperationException();
    }
}
