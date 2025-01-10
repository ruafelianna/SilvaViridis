using ReactiveUI.Validation.Helpers;
using SilvaViridis.Common.Numerics;
using System;
using System.Linq.Expressions;
using System.Numerics;
using static SilvaViridis.Components.Extensions.ValidationUtils;

namespace SilvaViridis.Components.Extensions
{
    public static class SimpleComparisonValidationExtensions
    {
        public static ValidationHelper RuleShouldBeLess<TViewModel, TValue>(
            this TViewModel vm,
            Expression<Func<TViewModel, string?>> property,
            IObservable<TValue> value,
            NumberRegex? numberRegex = null,
            IObservable<bool>? shouldApply = null,
            string? message = null
        )
            where TViewModel : ValidatableViewModelBase
            where TValue :
                IComparisonOperators<TValue, TValue, bool>,
                INumberBase<TValue>
            => vm.CreateComparisonRule(
                property,
                value,
                ComparisonOperation.Less,
                numberRegex,
                shouldApply,
                message
            );

        public static ValidationHelper RuleShouldBeLessOrEqual<TViewModel, TValue>(
            this TViewModel vm,
            Expression<Func<TViewModel, string?>> property,
            IObservable<TValue> value,
            NumberRegex? numberRegex = null,
            IObservable<bool>? shouldApply = null,
            string? message = null
        )
            where TViewModel : ValidatableViewModelBase
            where TValue :
                IComparisonOperators<TValue, TValue, bool>,
                INumberBase<TValue>
            => vm.CreateComparisonRule(
                property,
                value,
                ComparisonOperation.LessOrEqual,
                numberRegex,
                shouldApply,
                message
            );

        public static ValidationHelper RuleShouldBeMore<TViewModel, TValue>(
            this TViewModel vm,
            Expression<Func<TViewModel, string?>> property,
            IObservable<TValue> value,
            NumberRegex? numberRegex = null,
            IObservable<bool>? shouldApply = null,
            string? message = null
        )
            where TViewModel : ValidatableViewModelBase
            where TValue :
                IComparisonOperators<TValue, TValue, bool>,
                INumberBase<TValue>
            => vm.CreateComparisonRule(
                property,
                value,
                ComparisonOperation.More,
                numberRegex,
                shouldApply,
                message
            );

        public static ValidationHelper RuleShouldBeMoreOrEqual<TViewModel, TValue>(
            this TViewModel vm,
            Expression<Func<TViewModel, string?>> property,
            IObservable<TValue> value,
            NumberRegex? numberRegex = null,
            IObservable<bool>? shouldApply = null,
            string? message = null
        )
            where TViewModel : ValidatableViewModelBase
            where TValue :
                IComparisonOperators<TValue, TValue, bool>,
                INumberBase<TValue>
            => vm.CreateComparisonRule(
                property,
                value,
                ComparisonOperation.MoreOrEqual,
                numberRegex,
                shouldApply,
                message
            );

        public static ValidationHelper RuleShouldBeEqual<TViewModel, TValue>(
            this TViewModel vm,
            Expression<Func<TViewModel, string?>> property,
            IObservable<TValue> value,
            NumberRegex? numberRegex = null,
            IObservable<bool>? shouldApply = null,
            string? message = null
        )
            where TViewModel : ValidatableViewModelBase
            where TValue :
                IComparisonOperators<TValue, TValue, bool>,
                INumberBase<TValue>
            => vm.CreateComparisonRule(
                property,
                value,
                ComparisonOperation.Equal,
                numberRegex,
                shouldApply,
                message
            );

        public static ValidationHelper RuleShouldBeNotEqual<TViewModel, TValue>(
            this TViewModel vm,
            Expression<Func<TViewModel, string?>> property,
            IObservable<TValue> value,
            NumberRegex? numberRegex = null,
            IObservable<bool>? shouldApply = null,
            string? message = null
        )
            where TViewModel : ValidatableViewModelBase
            where TValue :
                IComparisonOperators<TValue, TValue, bool>,
                INumberBase<TValue>
            => vm.CreateComparisonRule(
                property,
                value,
                ComparisonOperation.NotEqual,
                numberRegex,
                shouldApply,
                message
            );
    }
}
