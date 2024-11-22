namespace SilvaViridis.Common.Generators.Enums;

internal static class InternalConsts
{
    #region Ns

    public const string NS_Local = $"{nameof(SilvaViridis)}.{nameof(Common)}.{nameof(Generators)}.{nameof(Enums)}";

    #endregion

    #region Attribute

    public const string A_HasStrings = "HasStrings";

    public const string A_HasStringsFull = $"{A_HasStrings}{POST_Attribute}";

    public const string P_HasStrings_StringsClass = "StringsClass";

    public const string P_HasStrings_StringsNamespace = "StringsNamespace";

    public const string P_HasStrings_ExtClass = "ExtClass";

    public const string P_HasStrings_ExtNamespace = "ExtNamespace";

    public const string P_HasStrings_GenerateFunctions = "GenerateFunctions";

    #endregion

    #region Formatter

    public const string I_IFormatter = "IFormatter";

    public const string T_TObj = "TObj";

    public const string M_AsString = "AsString";

    public const string L_obj = "obj";

    #endregion

    #region Generic attribute

    public const string T_TFormatter = "TFormatter";

    public const string L_fmt = "fmt";

    #endregion

    #region Other

    public const string POST_Extensions = "Extensions";

    public const string POST_Dict = "Dict";

    public const string Tab = "    ";

    public const string NewLine = "\n";

    public const string F_hasAttr = "_hasStringsAttr";

    #endregion
}
