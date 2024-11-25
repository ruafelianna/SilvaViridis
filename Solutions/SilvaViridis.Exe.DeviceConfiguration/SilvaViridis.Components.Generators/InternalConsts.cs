using SilvaViridis.Common.CSharp.Enums;

namespace SilvaViridis.Components.Generators
{
    internal static class InternalConsts
    {
        #region Ns

        public const string NS_Root = nameof(SilvaViridis);

        public const string NS_Local = $"{NS_Root}.{nameof(Components)}.{nameof(Generators)}";

        public const string NS_DynamicData = "DynamicData";

        public const string NS_AccessModifier = $"{NS_Root}.{nameof(Common)}.{nameof(Common.CSharp)}.{nameof(Common.CSharp.Enums)}";

        #endregion

        #region SourceCache

        public const string CL_SourceCache = "SourceCache";

        #endregion

        #region AccessModifier

        public const string E_AccessModifier = nameof(AccessModifier);

        #endregion

        #region Attribute

        public const string A_SourceCache = "SourceCache";

        public const string A_SourceCacheFull = $"{A_SourceCache}{POST_Attribute}";

        public const string P_SourceCache_KeyTypeName = "KeyTypeName";

        public const string P_SourceCache_KeyTypeNamespace = "KeyTypeNamespace";

        public const string P_SourceCache_CacheModifier = "CacheModifier";

        public const string P_SourceCache_PropertyModifier = "PropertyModifier";

        #endregion

        #region Other

        public const string POST_Cache = "Cache";

        public const string Tab = "    ";

        public const string NewLine = "\n";

        #endregion
    }
}
