using ReactiveUI;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;
using SilvaViridis.Common.Numerics;
using SilvaViridis.Common.Text.Extensions;
using SilvaViridis.Components.Assets.Translations;
using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Numerics;
using System.Reactive.Linq;

namespace SilvaViridis.Components.Extensions
{
    internal static class ValidationUtils
    {
        public static ValidationHelper CreateSimpleRule<TViewModel, TValue>(
            this TViewModel vm,
            Expression<Func<TViewModel, TValue>> property,
            IObservable<string> defaultErrMsg,
            Func<TValue?, bool> isValid,
            Func<string, string> format,
            IObservable<bool>? shouldApply,
            string? message
        ) where TViewModel : ValidatableViewModelBase
            => vm.ValidationRule(
                property,
                vm
                    .WhenAnyValue(property)
                    .CombineLatest(
                        defaultErrMsg,
                        shouldApply ?? Observable.Return(true)
                    )
                    .Select(data => new {
                        IsValid = isValid(data.First),
                        Msg = format(data.Second),
                        ShouldApply = data.Third,
                    }),
                data => !data.ShouldApply || data.IsValid,
                data => message ?? data.Msg
            );

        public enum ComparisonOperation
        {
            Less,
            LessOrEqual,
            More,
            MoreOrEqual,
            Equal,
            NotEqual,
        }

        public static bool Compare<TValue>(
            ComparisonOperation operation,
            TValue left,
            TValue right
        ) where TValue :
            IComparisonOperators<TValue, TValue, bool>,
            INumberBase<TValue>
            => operation switch
            {
                ComparisonOperation.Less
                    => left < right,

                ComparisonOperation.LessOrEqual
                    => left <= right,

                ComparisonOperation.More
                    => left > right,

                ComparisonOperation.MoreOrEqual
                    => left >= right,

                ComparisonOperation.Equal
                    => left == right,

                ComparisonOperation.NotEqual
                    => left != right,

                _ => throw new ArgumentOutOfRangeException(nameof(operation)),
            };

        public static IObservable<string> GetComparisonString(
            ComparisonOperation operation
        ) => (
            operation switch {
                ComparisonOperation.Less
                    => ValidationStrings.Less,

                ComparisonOperation.LessOrEqual
                    => ValidationStrings.LessOrEqual,

                ComparisonOperation.More
                    => ValidationStrings.More,

                ComparisonOperation.MoreOrEqual
                    => ValidationStrings.MoreOrEqual,

                ComparisonOperation.Equal
                    => ValidationStrings.Equal,

                ComparisonOperation.NotEqual
                    => ValidationStrings.NotEqual,

                _ => throw new ArgumentOutOfRangeException(nameof(operation)),
            }
        ).ValueObservable;

        public static ValidationHelper CreateComparisonRule<TViewModel, TValue>(
            this TViewModel vm,
            Expression<Func<TViewModel, string?>> property,
            IObservable<TValue> value,
            ComparisonOperation operation,
            NumberRegex? numberRegex,
            IObservable<bool>? shouldApply,
            string? message
        )
            where TViewModel : ValidatableViewModelBase
            where TValue :
                IComparisonOperators<TValue, TValue, bool>,
                INumberBase<TValue>
            => vm.ValidationRule(
                property,
                vm
                    .WhenAnyValue(property)
                    .CombineLatest(
                        value,
                        ValidationStrings.MustBeComparison.ValueObservable,
                        GetComparisonString(operation),
                        shouldApply ?? Observable.Return(true)
                    )
                    .Select(data => new {
                        Property = data.First,
                        Value = data.Second,
                        ErrorMsg = data.Third,
                        ComparisonStr = data.Fourth,
                        ShouldApply = data.Fifth,
                    })
                    .Select(data => new {
                        IsValid =
                            data.Property.IsNullOrWhiteSpace()
                            || (
                                TValue.TryParse(
                                    data.Property,
                                    numberRegex?.NumberStyles ?? NumberStyles.None,
                                    numberRegex?.NumberFormat,
                                    out var result
                                )
                                && Compare(operation, result, data.Value)
                            ),
                        Msg = string.Format(
                            data.ErrorMsg,
                            data.ComparisonStr,
                            data.Value
                        ),
                        data.ShouldApply,
                    }),
                data => !data.ShouldApply || data.IsValid,
                data => message ?? data.Msg
            );

        public static ValidationHelper CreateComparisonRule<TViewModel, TProperty, T1, T2, TValue>(
            this TViewModel vm,
            Expression<Func<TViewModel, TProperty?>> property,
            Expression<Func<TViewModel, T1?>> prop1,
            Expression<Func<TViewModel, T2?>> prop2,
            Func<T1, T2, TValue> process,
            IObservable<TValue> value,
            ComparisonOperation comparisonOperation,
            IObservable<bool>? shouldApply,
            IObservable<string> processDesc,
            string? message
        )
            where TViewModel : ValidatableViewModelBase
            where TValue :
                INumberBase<TValue>,
                IComparisonOperators<TValue, TValue, bool>
            where T1 : struct
            where T2 : struct
            => vm.ValidationRule(
                property,
                vm
                    .WhenAnyValue(prop1)
                    .CombineLatest(
                        vm.WhenAnyValue(prop2),
                        value,
                        ValidationStrings.MustBeComparisonFull.ValueObservable,
                        GetComparisonString(comparisonOperation),
                        processDesc,
                        shouldApply ?? Observable.Return(true)
                    )
                    .Select(data => new
                    {
                        Prop1 = data.First,
                        Prop2 = data.Second,
                        Value = data.Third,
                        ErrorMsg = data.Fourth,
                        ComparisonStr = data.Fifth,
                        Description = data.Sixth,
                        ShouldApply = data.Seventh,
                    })
                    .Select(data =>
                    {
                        return new
                        {
                            IsValid =
                                data.Prop1 is null
                                || data.Prop2 is null
                                || Compare(
                                    comparisonOperation,
                                    process(data.Prop1.Value, data.Prop2.Value),
                                    data.Value
                                ),
                            Msg = string.Format(
                                data.ErrorMsg,
                                data.Description,
                                data.ComparisonStr,
                                data.Value
                            ),
                            data.ShouldApply,
                        };
                    }),
                data => !data.ShouldApply || data.IsValid,
                data => message ?? data.Msg
            );
    }
}
