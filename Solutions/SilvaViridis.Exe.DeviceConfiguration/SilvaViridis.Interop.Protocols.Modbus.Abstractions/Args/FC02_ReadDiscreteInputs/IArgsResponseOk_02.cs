using System.Collections.Generic;

namespace SilvaViridis.Interop.Protocols.Modbus.Abstractions.Args.FC02_ReadDiscreteInputs
{
    public interface IArgsResponseOk_02 : IModbusArgsResponseOk
    {
        byte ByteCount { get; }

        IReadOnlyList<byte> InputStatus { get; }

        IReadOnlyList<bool> Inputs { get; }

        ushort StartingAddress { get; }

        ushort QuantityOfInputs { get; }
    }
}
