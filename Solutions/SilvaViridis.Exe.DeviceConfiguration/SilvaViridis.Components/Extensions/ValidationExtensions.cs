using ReactiveUI;
using System.Linq.Expressions;
using System;
using ReactiveUI.Validation.Extensions;
using System.Reactive.Linq;
using SilvaViridis.Common.Text.Extensions;
using SilvaViridis.Components.Assets.Translations;

namespace SilvaViridis.Components.Extensions
{
    public static class ValidationExtensions
    {
        public static void RuleNotNullOrWhiteSpace<TViewModel>(
            this TViewModel vm,
            Expression<Func<TViewModel, string?>> property,
            string? message = null
        ) where TViewModel : ValidatableViewModelBase
            => vm.ValidationRule(
                property,
                vm.WhenAnyValue(property)
                    .Select(x => !x.IsNullOrWhiteSpace()),
                message ?? ValidationStrings.CannotBeEmpty.Value!
            );

        public static void RuleNotNullOrEmpty<TViewModel>(
            this TViewModel vm,
            Expression<Func<TViewModel, string?>> property,
            string? message = null
        ) where TViewModel : ValidatableViewModelBase
            => vm.ValidationRule(
                property,
                vm.WhenAnyValue(property)
                    .Select(x => !x.IsNullOrEmpty()),
                message ?? ValidationStrings.CannotBeEmpty.Value!
            );
    }
}
