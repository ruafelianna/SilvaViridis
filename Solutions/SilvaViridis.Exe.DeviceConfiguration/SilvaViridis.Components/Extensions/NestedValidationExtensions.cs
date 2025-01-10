using ReactiveUI;
using ReactiveUI.Validation.Abstractions;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;
using System;
using System.Linq.Expressions;
using System.Reactive.Linq;

namespace SilvaViridis.Components.Extensions
{
    public static class NestedValidationExtensions
    {
        public static ValidationHelper RuleNestedCtxValidIfExists<TViewModel, TProperty>(
            this TViewModel vm,
            Expression<Func<TViewModel, TProperty?>> property
        )
            where TViewModel : ValidatableViewModelBase
            where TProperty : IValidatableViewModel
            => vm.ValidationRule(
                property,
                vm
                    .WhenAnyValue(property)
                    .Select(prop => prop is null
                        ? Observable.Return(true)
                        : prop.ValidationContext.Valid
                    )
                    .Switch(),
                string.Empty
            );
    }
}
