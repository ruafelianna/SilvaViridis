using ReactiveUI;
using ReactiveUI.Validation.Abstractions;
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
    public static class ValidationExtensions
    {
        #region Nested

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

        #endregion

        #region Empty

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

        #endregion

        #region Number

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

        #endregion

        #region Comparison

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

        #endregion

        #region Utils

        private static ValidationHelper CreateSimpleRule<TViewModel, TValue>(
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

        private enum ComparisonOperation
        {
            Less,
            LessOrEqual,
            More,
            MoreOrEqual,
            Equal,
            NotEqual,
        }

        private static bool Compare<TValue>(
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

        private static string GetComparisonString(
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

        private static ValidationHelper CreateComparisonRule<TViewModel, TValue>(
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

        #endregion
    }
}
