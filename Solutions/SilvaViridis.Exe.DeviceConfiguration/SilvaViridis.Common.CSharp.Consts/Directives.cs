namespace SilvaViridis.Common.CSharp.Consts
{
    public static class Directives
    {
        public const string DIR_NullableDisable = $"{_nullable} {_disable}";

        public const string DIR_NullableEnable = $"{_nullable} {_enable}";

        public const string DIR_NullableRestore = $"{_nullable} {_restore}";

        public const string DIR_NullableDisableAnnotations = $"{_nullable} {_disable} {_annotations}";

        public const string DIR_NullableEnableAnnotations = $"{_nullable} {_enable} {_annotations}";

        public const string DIR_NullableRestoreAnnotations = $"{_nullable} {_restore} {_annotations}";

        public const string DIR_NullableDisableWarnings = $"{_nullable} {_disable} {_warnings}";

        public const string DIR_NullableEnableWarnings = $"{_nullable} {_enable} {_warnings}";

        public const string DIR_NullableRestoreWarnings = $"{_nullable} {_restore} {_warnings}";

        private const string _nullable = "#nullable";

        private const string _disable = "disable";

        private const string _enable = "enable";

        private const string _restore = "restore";

        private const string _annotations = "annotations";

        private const string _warnings = "warnings";
    }
}
