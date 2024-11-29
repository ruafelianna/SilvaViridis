using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Assets.Translations;

namespace SilvaViridis.Interop.Protocols.Modbus.Abstractions.Exceptions
{
    public class ModbusIncorrectResponseException : ModbusException
    {
        public ModbusIncorrectResponseException(string param)
            : base(
                ModbusExceptionsStrings.ModbusIncorrectResponseException,
                param
            )
        {
            IncorrectParam = param;
        }

        public string IncorrectParam { get; }
    }
}
