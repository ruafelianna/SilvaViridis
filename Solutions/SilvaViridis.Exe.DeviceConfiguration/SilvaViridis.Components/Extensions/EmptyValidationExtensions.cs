using ReactiveUI.Validation.Helpers;
using SilvaViridis.Common.Text.Extensions;
using SilvaViridis.Components.Assets.Translations;
using System;
using System.Linq.Expressions;

namespace SilvaViridis.Components.Extensions
{
    public static class EmptyValidationExtensions
    {
        public static ValidationHelper RuleNotNull<TViewModel, TProperty>(
            this TViewModel vm,
            Expression<Func<TViewModel, TProperty?>> property,
            IObservable<bool>? shouldApply = null,
            string? message = null
        ) where TViewModel : ValidatableViewModelBase
            => vm.CreateSimpleRule(
                property,
                ValidationStrings.CannotBeEmpty.ValueObservable,
                value => value is not null,
                msg => msg,
                shouldApply,
                message
            );

        public static ValidationHelper RuleNotNullOrWhiteSpace<TViewModel>(
            this TViewModel vm,
            Expression<Func<TViewModel, string?>> property,
            IObservable<bool>? shouldApply = null,
            string? message = null
        ) where TViewModel : ValidatableViewModelBase
            => vm.CreateSimpleRule(
                property,
                ValidationStrings.CannotBeEmpty.ValueObservable,
                value => !value.IsNullOrWhiteSpace(),
                msg => msg,
                shouldApply,
                message
            );

        public static ValidationHelper RuleNotNullOrEmpty<TViewModel>(
            this TViewModel vm,
            Expression<Func<TViewModel, string?>> property,
            IObservable<bool>? shouldApply = null,
            string? message = null
        ) where TViewModel : ValidatableViewModelBase
            => vm.CreateSimpleRule(
                property,
                ValidationStrings.CannotBeEmpty.ValueObservable,
                value => !value.IsNullOrEmpty(),
                msg => msg,
                shouldApply,
                message
            );
    }
}
