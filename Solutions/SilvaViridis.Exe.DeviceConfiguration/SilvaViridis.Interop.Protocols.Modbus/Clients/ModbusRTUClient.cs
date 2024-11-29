using SilvaViridis.Common.Numerics;
using SilvaViridis.Interop.Ports.SerialPort.Abstractions;
using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Args;
using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Args.Extensions;
using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Args.Factory;
using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Clients;
using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Exceptions;
using SilvaViridis.Interop.Protocols.Modbus.Args.Factory;
using System.Threading;
using System.Threading.Tasks;

namespace SilvaViridis.Interop.Protocols.Modbus.Clients
{
    public class ModbusRTUClient : IModbusRTUClient
    {
        public ModbusRTUClient()
        {
            _modbusArgsResponseFactory = new ModbusArgsResponseFactory();
            _modbusArgsErrorFactory = new ModbusArgsErrorFactory();
        }

        public ModbusRTUClient(
            IModbusArgsResponseFactory modbusArgsResponseFactory,
            IModbusArgsErrorFactory modbusArgsErrorFactory
        )
        {
            _modbusArgsResponseFactory = modbusArgsResponseFactory;
            _modbusArgsErrorFactory = modbusArgsErrorFactory;
        }

        public Task<IArgsResponseOk_01> ReadCoils(
            ISerialPort port,
            byte address,
            IArgsRequest_01 request,
            CancellationToken token = default
        ) => SendRequest(
            port,
            address,
            request,
            _modbusArgsResponseFactory.Create_01,
            _modbusArgsErrorFactory.Create,
            token
        );

        public Task<IArgsResponseOk_02> ReadDiscreteInputs(
            ISerialPort port,
            byte address,
            IArgsRequest_02 request,
            CancellationToken token = default
        ) => SendRequest(
            port,
            address,
            request,
            _modbusArgsResponseFactory.Create_02,
            _modbusArgsErrorFactory.Create,
            token
        );

        public Task<IArgsResponseOk_03> ReadHoldingRegisters(
            ISerialPort port,
            byte address,
            IArgsRequest_03 request,
            CancellationToken token = default
        ) => SendRequest(
            port,
            address,
            request,
            _modbusArgsResponseFactory.Create_03,
            _modbusArgsErrorFactory.Create,
            token
        );

        public Task<IArgsResponseOk_04> ReadInputRegisters(
            ISerialPort port,
            byte address,
            IArgsRequest_04 request,
            CancellationToken token = default
        ) => SendRequest(
            port,
            address,
            request,
            _modbusArgsResponseFactory.Create_04,
            _modbusArgsErrorFactory.Create,
            token
        );

        public Task<IArgsResponseOk_05> WriteSingleCoil(
            ISerialPort port,
            byte address,
            IArgsRequest_05 request,
            CancellationToken token = default
        ) => SendRequest(
            port,
            address,
            request,
            _modbusArgsResponseFactory.Create_05,
            _modbusArgsErrorFactory.Create,
            token
        );

        public Task<IArgsResponseOk_06> WriteSingleRegister(
            ISerialPort port,
            byte address,
            IArgsRequest_06 request,
            CancellationToken token = default
        ) => SendRequest(
            port,
            address,
            request,
            _modbusArgsResponseFactory.Create_06,
            _modbusArgsErrorFactory.Create,
            token
        );

        public Task<IArgsResponseOk_0F> WriteMultipleCoils(
            ISerialPort port,
            byte address,
            IArgsRequest_0F request,
            CancellationToken token = default
        ) => SendRequest(
            port,
            address,
            request,
            _modbusArgsResponseFactory.Create_0F,
            _modbusArgsErrorFactory.Create,
            token
        );

        public Task<IArgsResponseOk_10> WriteMultipleRegisters(
            ISerialPort port,
            byte address,
            IArgsRequest_10 request,
            CancellationToken token = default
        ) => SendRequest(
            port,
            address,
            request,
            _modbusArgsResponseFactory.Create_10,
            _modbusArgsErrorFactory.Create,
            token
        );

        protected static async Task<TResponse> SendRequest<TRequest, TResponse>(
            ISerialPort port,
            byte address,
            TRequest request,
            DCreateResponse<TRequest, TResponse> createResponse,
            DCreateError<IModbusArgsResponseError> createError,
            CancellationToken token
        )
            where TRequest : IModbusArgsRequest
            where TResponse : IModbusArgsResponseOk
        {
            var cmd = request.RTU_Cmd(address);

            await port.WriteAsync(cmd, token);

            var data = await port.ReadAsync(2, token);

            if (data[0] != address)
            {
                throw new ModbusIncorrectResponseException("address");
            }

            var rawCode = data[1];

            if (rawCode == request.RawFunctionCode)
            {
                var response = await createResponse(
                    request,
                    port.ReadAsync,
                    token
                );

                await CheckCRC(port, address, response, token);

                return response;
            }

            if (rawCode == (request.RawFunctionCode | 0x80))
            {
                data = await port.ReadAsync(1, token);

                var error = createError(rawCode, data[0]);

                await CheckCRC(port, address, error, token);

                throw new ModbusProtocolException(error);
            }

            throw new ModbusIncorrectResponseException("function code");
        }

        private readonly IModbusArgsResponseFactory _modbusArgsResponseFactory;
        private readonly IModbusArgsErrorFactory _modbusArgsErrorFactory;

        private static async Task CheckCRC(
            ISerialPort port,
            byte address,
            IModbusArgs args,
            CancellationToken token
        )
        {
            var data = await port.ReadAsync(2, token);

            var crc = args.RTU_CRC(address);

            if (data[0] != crc.LSB() || data[1] != crc.MSB())
            {
                throw new ModbusIncorrectResponseException("CRC");
            }
        }
    }
}
