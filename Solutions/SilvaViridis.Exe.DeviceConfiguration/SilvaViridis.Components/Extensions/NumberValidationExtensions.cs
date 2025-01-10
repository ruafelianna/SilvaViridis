using ReactiveUI.Validation.Helpers;
using SilvaViridis.Common.Numerics;
using SilvaViridis.Common.Text.Extensions;
using SilvaViridis.Components.Assets.Translations;
using System;
using System.Linq.Expressions;

namespace SilvaViridis.Components.Extensions
{
    public static class NumberValidationExtensions
    {
        public static ValidationHelper RuleShouldBeANumber<TViewModel>(
            this TViewModel vm,
            Expression<Func<TViewModel, string?>> property,
            NumberRegex numberRegex,
            IObservable<bool>? shouldApply = null,
            string? message = null
        ) where TViewModel : ValidatableViewModelBase
            => vm.CreateSimpleRule(
                property,
                ValidationStrings.ShouldBeANumber.ValueObservable,
                value =>
                    value.IsNullOrWhiteSpace()
                    || numberRegex.IsNumber(value.Trim()),
                msg => msg,
                shouldApply,
                message
            );

        public static ValidationHelper RuleShouldBeAnIntNumber<TViewModel>(
            this TViewModel vm,
            Expression<Func<TViewModel, string?>> property,
            NumberRegex numberRegex,
            IObservable<bool>? shouldApply = null,
            string? message = null
        ) where TViewModel : ValidatableViewModelBase
            => vm.CreateSimpleRule(
                property,
                ValidationStrings.ShouldBeAnIntNumber.ValueObservable,
                value =>
                    value.IsNullOrWhiteSpace()
                    || numberRegex.IsIntNumber(value.Trim()),
                msg => msg,
                shouldApply,
                message
            );
    }
}
