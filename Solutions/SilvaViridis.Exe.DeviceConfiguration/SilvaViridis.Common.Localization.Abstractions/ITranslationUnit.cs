using System;

namespace SilvaViridis.Common.Localization.Abstractions
{
    public interface ITranslationUnit
    {
        string Key { get; }

        string? Value { get; }

        IObservable<string> ValueObservable { get; }
    }
}
