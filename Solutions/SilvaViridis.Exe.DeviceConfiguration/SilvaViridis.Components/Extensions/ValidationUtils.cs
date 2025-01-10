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
using System.Threading.Tasks;

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

        public static string GetComparisonString(
            ComparisonOperation operation
        ) => operation switch
        {
            ComparisonOperation.Less
                => ValidationStrings.Less.Value!,

            ComparisonOperation.LessOrEqual
                => ValidationStrings.LessOrEqual.Value!,

            ComparisonOperation.More
                => ValidationStrings.More.Value!,

            ComparisonOperation.MoreOrEqual
                => ValidationStrings.MoreOrEqual.Value!,

            ComparisonOperation.Equal
                => ValidationStrings.Equal.Value!,

            ComparisonOperation.NotEqual
                => ValidationStrings.NotEqual.Value!,

            _ => throw new ArgumentOutOfRangeException(nameof(operation)),
        };

        public static ValidationHelper CreateComparisonRule<TViewModel, TValue>(
            this TViewModel vm,
            Expression<Func<TViewModel, string?>> property,
            Func<Task<TValue>> getValue,
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
                        ValidationStrings.MustBeComparison.ValueObservable,
                        shouldApply ?? Observable.Return(true)
                    )
                    .Select(async data => {
                        var value = await getValue();
                        return new
                        {
                            IsValid =
                                data.First.IsNullOrWhiteSpace()
                                || (
                                    TValue.TryParse(
                                        data.First,
                                        numberRegex?.NumberStyles ?? NumberStyles.None,
                                        numberRegex?.NumberFormat,
                                        out var result
                                    )
                                    && Compare(operation, result, value)
                                ),
                            Msg = string.Format(
                                data.Second,
                                GetComparisonString(operation),
                                value
                            ),
                            ShouldApply = data.Third,
                        };
                    })
                    .Concat(),
                data => !data.ShouldApply || data.IsValid,
                data => message ?? data.Msg
            );
    }
}
