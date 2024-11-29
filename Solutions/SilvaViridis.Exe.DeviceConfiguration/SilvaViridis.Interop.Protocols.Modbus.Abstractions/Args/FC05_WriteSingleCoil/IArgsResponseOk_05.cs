namespace SilvaViridis.Interop.Protocols.Modbus.Abstractions.Args.FC05_WriteSingleCoil
{
    public interface IArgsResponseOk_05 : IModbusArgsResponseOk
    {
        ushort OutputAddress { get; }

        ushort OutputValue { get; }
    }
}
