using SilvaViridis.Interop.Ports.SerialPort.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace SilvaViridis.Interop.Protocols.Modbus.Abstractions.Clients
{
    public interface IModbusRTUClient : IModbusClient
    {
        Task<IArgsResponseOk_01> ReadCoils(
            ISerialPort port,
            byte address,
            IArgsRequest_01 request,
            CancellationToken token = default
        );

        Task<IArgsResponseOk_02> ReadDiscreteInputs(
            ISerialPort port,
            byte address,
            IArgsRequest_02 request,
            CancellationToken token = default
        );

        Task<IArgsResponseOk_03> ReadHoldingRegisters(
            ISerialPort port,
            byte address,
            IArgsRequest_03 request,
            CancellationToken token = default
        );

        Task<IArgsResponseOk_04> ReadInputRegisters(
            ISerialPort port,
            byte address,
            IArgsRequest_04 request,
            CancellationToken token = default
        );

        Task<IArgsResponseOk_05> WriteSingleCoil(
            ISerialPort port,
            byte address,
            IArgsRequest_05 request,
            CancellationToken token = default
        );

        Task<IArgsResponseOk_06> WriteSingleRegister(
            ISerialPort port,
            byte address,
            IArgsRequest_06 request,
            CancellationToken token = default
        );

        Task<IArgsResponseOk_0F> WriteMultipleCoils(
            ISerialPort port,
            byte address,
            IArgsRequest_0F request,
            CancellationToken token = default
        );

        Task<IArgsResponseOk_10> WriteMultipleRegisters(
            ISerialPort port,
            byte address,
            IArgsRequest_10 request,
            CancellationToken token = default
        );
    }
}
