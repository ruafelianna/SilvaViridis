using ReactiveUI.Validation.Helpers;
using SilvaViridis.Common.Numerics;
using System;
using System.Linq.Expressions;
using System.Numerics;
using System.Threading.Tasks;
using static SilvaViridis.Components.Extensions.ValidationUtils;

namespace SilvaViridis.Components.Extensions
{
    public static class SimpleComparisonValidationExtensions
    {
        public static ValidationHelper RuleShouldBeLess<TViewModel, TValue>(
            this TViewModel vm,
            Expression<Func<TViewModel, string?>> property,
            Func<Task<TValue>> getValue,
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
                getValue,
                ComparisonOperation.Less,
                numberRegex,
                shouldApply,
                message
            );

        public static ValidationHelper RuleShouldBeLessOrEqual<TViewModel, TValue>(
            this TViewModel vm,
            Expression<Func<TViewModel, string?>> property,
            Func<Task<TValue>> getValue,
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
                getValue,
                ComparisonOperation.LessOrEqual,
                numberRegex,
                shouldApply,
                message
            );

        public static ValidationHelper RuleShouldBeMore<TViewModel, TValue>(
            this TViewModel vm,
            Expression<Func<TViewModel, string?>> property,
            Func<Task<TValue>> getValue,
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
                getValue,
                ComparisonOperation.More,
                numberRegex,
                shouldApply,
                message
            );

        public static ValidationHelper RuleShouldBeMoreOrEqual<TViewModel, TValue>(
            this TViewModel vm,
            Expression<Func<TViewModel, string?>> property,
            Func<Task<TValue>> getValue,
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
                getValue,
                ComparisonOperation.MoreOrEqual,
                numberRegex,
                shouldApply,
                message
            );

        public static ValidationHelper RuleShouldBeEqual<TViewModel, TValue>(
            this TViewModel vm,
            Expression<Func<TViewModel, string?>> property,
            Func<Task<TValue>> getValue,
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
                getValue,
                ComparisonOperation.Equal,
                numberRegex,
                shouldApply,
                message
            );

        public static ValidationHelper RuleShouldBeNotEqual<TViewModel, TValue>(
            this TViewModel vm,
            Expression<Func<TViewModel, string?>> property,
            Func<Task<TValue>> getValue,
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
                getValue,
                ComparisonOperation.NotEqual,
                numberRegex,
                shouldApply,
                message
            );
    }
}
