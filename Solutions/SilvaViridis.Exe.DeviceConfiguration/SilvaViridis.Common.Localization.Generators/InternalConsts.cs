namespace SilvaViridis.Common.Localization.Generators
{
    internal static class InternalConsts
    {
        #region Conf file

        public const string LocalizationConf_Ext = ".toml";

        public const string LocalizationConf = $"localization{LocalizationConf_Ext}";

        public const string LocalizationConf_Class = "generated_class_name";

        public const string LocalizationConf_Default = "default_culture";

        public const string LocalizationConf_Namespace = "generated_namespace";

        #endregion

        #region Ns

        public const string NS_Root = nameof(SilvaViridis);

        public const string NS_Common = $"{NS_Root}.Common";

        public const string NS_Localization = $"{NS_Common}.Localization";

        public const string NS_Localization_Abstractions = $"{NS_Localization}.Abstractions";

        #endregion

        #region Objects

        public const string I_TranslationUnit = "ITranslationUnit";

        public const string I_TranslationProvider = "ITranslationProvider";

        public const string CL_TranslationUnit = "TranslationUnit";

        public const string CL_TranslationProvider = "TranslationProvider";

        #endregion

        #region Other

        public const string Tab = "    ";

        public const string AnalyzerOptions_Project_Dir = "build_property.projectdir";

        #endregion
    }
}
