using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Args;
using SilvaViridis.Interop.Protocols.Modbus.Abstractions.Args.Factory;

namespace SilvaViridis.Interop.Protocols.Modbus.Args.Factory
{
    public class ModbusArgsResponseFactory : IModbusArgsResponseFactory
    {
        public DCreateResponse<IArgsRequest_01, IArgsResponseOk_01> Create_01
            => ArgsResponseOk_01.Create;

        public DCreateResponse<IArgsRequest_02, IArgsResponseOk_02> Create_02
            => ArgsResponseOk_02.Create;

        public DCreateResponse<IArgsRequest_03, IArgsResponseOk_03> Create_03
            => ArgsResponseOk_03.Create;

        public DCreateResponse<IArgsRequest_04, IArgsResponseOk_04> Create_04
            => ArgsResponseOk_04.Create;

        public DCreateResponse<IArgsRequest_05, IArgsResponseOk_05> Create_05
            => ArgsResponseOk_05.Create;

        public DCreateResponse<IArgsRequest_06, IArgsResponseOk_06> Create_06
            => ArgsResponseOk_06.Create;

        public DCreateResponse<IArgsRequest_0F, IArgsResponseOk_0F> Create_0F
            => ArgsResponseOk_0F.Create;

        public DCreateResponse<IArgsRequest_10, IArgsResponseOk_10> Create_10
            => ArgsResponseOk_10.Create;
    }
}
