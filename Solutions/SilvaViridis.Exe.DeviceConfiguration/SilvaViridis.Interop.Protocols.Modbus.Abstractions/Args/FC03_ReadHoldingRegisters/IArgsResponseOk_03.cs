using System.Collections.Generic;

namespace SilvaViridis.Interop.Protocols.Modbus.Abstractions.Args.FC03_ReadHoldingRegisters
{
    public interface IArgsResponseOk_03 : IModbusArgsResponseOk
    {
        byte ByteCount { get; }

        IReadOnlyList<byte> RegisterValue { get; }

        IReadOnlyList<ushort> RegistersDirect { get; }

        IReadOnlyList<ushort> RegistersReverse { get; }

        ushort StartingAddress { get; }

        ushort QuantityOfRegisters { get; }
    }
}
