using SilvaViridis.Common.Localization.Abstractions;

namespace SilvaViridis.Interop.Protocols.Modbus.Abstractions.Exceptions
{
    public class ModbusException : TranslatableException
    {
        public ModbusException(
            ITranslationUnit message,
            params object?[]? args
        ) : base(message, args)
        {
        }
    }
}
