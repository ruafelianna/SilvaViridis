using SilvaViridis.Common.CSharp.Enums;

namespace SilvaViridis.Common.CSharp.Extensions
{
    public static partial class AccessModifierExtensions
    {
        public static AccessModifierRestriction Restriction(
             this AccessModifier left,
             AccessModifier right
         )
        {
            if (left == right)
            {
                return AccessModifierRestriction.Equal;
            }

            if (
                left == AccessModifier.File
                || right == AccessModifier.File
            )
            {
                return AccessModifierRestriction.Error;
            }

            if (
                left == AccessModifier.Public
                || right == AccessModifier.Private
                || (
                    left == AccessModifier.Internal
                    && right == AccessModifier.PrivateProtected
                )
                || (
                    left == AccessModifier.ProtectedInternal
                    && (
                        right == AccessModifier.Protected
                        || right == AccessModifier.Internal
                        || right == AccessModifier.PrivateProtected
                    )
                )
            )
            {
                return AccessModifierRestriction.LessRestrict;
            }

            return AccessModifierRestriction.MoreRestrict;
        }
    }
}
