using ReactiveUI.Validation.Helpers;
using System;
using System.Linq.Expressions;
using System.Numerics;
using static SilvaViridis.Components.Extensions.ValidationUtils;

namespace SilvaViridis.Components.Extensions
{
    public static class ComplexComparisonValidationExtensions
    {
        public static ValidationHelper RuleShouldBeLess<TViewModel, TProperty, T1, T2, TValue>(
            this TViewModel vm,
            Expression<Func<TViewModel, TProperty?>> property,
            Expression<Func<TViewModel, T1?>> prop1,
            Expression<Func<TViewModel, T2?>> prop2,
            Func<T1, T2, TValue> process,
            IObservable<TValue> value,
            IObservable<string> processDesc,
            IObservable<bool>? shouldApply = null,
            string? message = null
        )
            where TViewModel : ValidatableViewModelBase
            where TValue :
                INumberBase<TValue>,
                IComparisonOperators<TValue, TValue, bool>
            where T1 : struct
            where T2 : struct
            => vm.CreateComparisonRule(
                property,
                prop1,
                prop2,
                process,
                value,
                ComparisonOperation.Less,
                shouldApply,
                processDesc,
                message
            );

        public static ValidationHelper RuleShouldBeLessOrEqual<TViewModel, TProperty, T1, T2, TValue>(
            this TViewModel vm,
            Expression<Func<TViewModel, TProperty?>> property,
            Expression<Func<TViewModel, T1?>> prop1,
            Expression<Func<TViewModel, T2?>> prop2,
            Func<T1, T2, TValue> process,
            IObservable<TValue> value,
            IObservable<string> processDesc,
            IObservable<bool>? shouldApply = null,
            string? message = null
        )
            where TViewModel : ValidatableViewModelBase
            where TValue :
                INumberBase<TValue>,
                IComparisonOperators<TValue, TValue, bool>
            where T1 : struct
            where T2 : struct
            => vm.CreateComparisonRule(
                property,
                prop1,
                prop2,
                process,
                value,
                ComparisonOperation.LessOrEqual,
                shouldApply,
                processDesc,
                message
            );

        public static ValidationHelper RuleShouldBeMore<TViewModel, TProperty, T1, T2, TValue>(
            this TViewModel vm,
            Expression<Func<TViewModel, TProperty?>> property,
            Expression<Func<TViewModel, T1?>> prop1,
            Expression<Func<TViewModel, T2?>> prop2,
            Func<T1, T2, TValue> process,
            IObservable<TValue> value,
            IObservable<string> processDesc,
            IObservable<bool>? shouldApply = null,
            string? message = null
        )
            where TViewModel : ValidatableViewModelBase
            where TValue :
                INumberBase<TValue>,
                IComparisonOperators<TValue, TValue, bool>
            where T1 : struct
            where T2 : struct
            => vm.CreateComparisonRule(
                property,
                prop1,
                prop2,
                process,
                value,
                ComparisonOperation.More,
                shouldApply,
                processDesc,
                message
            );

        public static ValidationHelper RuleShouldBeMoreOrEqual<TViewModel, TProperty, T1, T2, TValue>(
            this TViewModel vm,
            Expression<Func<TViewModel, TProperty?>> property,
            Expression<Func<TViewModel, T1?>> prop1,
            Expression<Func<TViewModel, T2?>> prop2,
            Func<T1, T2, TValue> process,
            IObservable<TValue> value,
            IObservable<string> processDesc,
            IObservable<bool>? shouldApply = null,
            string? message = null
        )
            where TViewModel : ValidatableViewModelBase
            where TValue :
                INumberBase<TValue>,
                IComparisonOperators<TValue, TValue, bool>
            where T1 : struct
            where T2 : struct
            => vm.CreateComparisonRule(
                property,
                prop1,
                prop2,
                process,
                value,
                ComparisonOperation.MoreOrEqual,
                shouldApply,
                processDesc,
                message
            );

        public static ValidationHelper RuleShouldBeEqual<TViewModel, TProperty, T1, T2, TValue>(
            this TViewModel vm,
            Expression<Func<TViewModel, TProperty?>> property,
            Expression<Func<TViewModel, T1?>> prop1,
            Expression<Func<TViewModel, T2?>> prop2,
            Func<T1, T2, TValue> process,
            IObservable<TValue> value,
            IObservable<string> processDesc,
            IObservable<bool>? shouldApply = null,
            string? message = null
        )
            where TViewModel : ValidatableViewModelBase
            where TValue :
                INumberBase<TValue>,
                IComparisonOperators<TValue, TValue, bool>
            where T1 : struct
            where T2 : struct
            => vm.CreateComparisonRule(
                property,
                prop1,
                prop2,
                process,
                value,
                ComparisonOperation.Equal,
                shouldApply,
                processDesc,
                message
            );

        public static ValidationHelper RuleShouldBeNotEqual<TViewModel, TProperty, T1, T2, TValue>(
            this TViewModel vm,
            Expression<Func<TViewModel, TProperty?>> property,
            Expression<Func<TViewModel, T1?>> prop1,
            Expression<Func<TViewModel, T2?>> prop2,
            Func<T1, T2, TValue> process,
            IObservable<TValue> value,
            IObservable<string> processDesc,
            IObservable<bool>? shouldApply = null,
            string? message = null
        )
            where TViewModel : ValidatableViewModelBase
            where TValue :
                INumberBase<TValue>,
                IComparisonOperators<TValue, TValue, bool>
            where T1 : struct
            where T2 : struct
            => vm.CreateComparisonRule(
                property,
                prop1,
                prop2,
                process,
                value,
                ComparisonOperation.NotEqual,
                shouldApply,
                processDesc,
                message
            );
    }
}
