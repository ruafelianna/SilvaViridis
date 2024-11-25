using SilvaViridis.Common.Generators.Enums;

namespace SilvaViridis.Common.CSharp.Enums
{
    /// <summary>
    /// <seealso href="https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/access-modifiers"/>
    /// </summary>
    [HasStrings(
        StringsClass = nameof(AccessModifierStrings),
        ExtNamespace = $"{nameof(SilvaViridis)}.{nameof(CSharp)}.{nameof(Extensions)}"
    )]
    public enum AccessModifier : byte
    {
        /// <summary>
        /// Code in any assembly can access this type or member.
        /// The accessibility level of the containing type
        /// controls the accessibility level of public members
        /// of the type
        /// </summary>
        Public = 0,

        /// <summary>
        /// Only code declared in the same class or struct
        /// can access this member
        /// </summary>
        Private = 1,

        /// <summary>
        /// Only code in the same class or in a derived class
        /// can access this type or member
        /// </summary>
        Protected = 2,

        /// <summary>
        /// Only code in the same assembly can access
        /// this type or member
        /// </summary>
        Internal = 3,

        /// <summary>
        /// Only code in the same assembly or in a derived class
        /// in another assembly can access this type or member
        /// </summary>
        ProtectedInternal = 4,

        /// <summary>
        /// Only code in the same assembly and in the same class
        /// or a derived class can access the type or member
        /// </summary>
        PrivateProtected = 5,

        /// <summary>
        /// Only code in the same file can access
        /// the type or member
        /// </summary>
        File = 6,
    }

    public class AccessModifierStrings
    {
        public const string Public = "public";

        public const string Private = "private";

        public const string Protected = "protected";

        public const string Internal = "internal";

        public const string ProtectedInternal = "protected internal";

        public const string PrivateProtected = "private protected";

        public const string File = "file";
    }
}
