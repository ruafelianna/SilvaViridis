namespace SilvaViridis.Interop.Protocols.Modbus.Abstractions.Args.FC0F_WriteMultipleCoils
{
    public interface IArgsResponseOk_0F : IModbusArgsResponseOk
    {
        ushort StartingAddress { get; }

        ushort QuantityOfOutputs { get; }
    }
}
