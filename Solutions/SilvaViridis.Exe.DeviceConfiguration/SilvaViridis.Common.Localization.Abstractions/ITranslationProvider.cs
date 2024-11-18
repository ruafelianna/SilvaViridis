using System.Collections.Generic;
using System.Globalization;

namespace SilvaViridis.Common.Localization.Abstractions
{
    public interface ITranslationProvider
    {
        CultureInfo Culture { get; set; }

        CultureInfo DefaultCulture { get; }

        IDictionary<string, string?>? Translation { get; }

        IDictionary<string, string?>? DefaultTranslation { get; }
    }
}
