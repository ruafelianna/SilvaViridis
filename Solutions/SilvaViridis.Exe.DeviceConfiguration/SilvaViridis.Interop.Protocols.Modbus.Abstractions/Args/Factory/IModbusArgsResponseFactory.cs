namespace SilvaViridis.Interop.Protocols.Modbus.Abstractions.Args.Factory
{
    public interface IModbusArgsResponseFactory
    {
        DCreateResponse<IArgsRequest_01, IArgsResponseOk_01> Create_01 { get; }

        DCreateResponse<IArgsRequest_02, IArgsResponseOk_02> Create_02 { get; }

        DCreateResponse<IArgsRequest_03, IArgsResponseOk_03> Create_03 { get; }

        DCreateResponse<IArgsRequest_04, IArgsResponseOk_04> Create_04 { get; }

        DCreateResponse<IArgsRequest_05, IArgsResponseOk_05> Create_05 { get; }

        DCreateResponse<IArgsRequest_06, IArgsResponseOk_06> Create_06 { get; }

        DCreateResponse<IArgsRequest_0F, IArgsResponseOk_0F> Create_0F { get; }

        DCreateResponse<IArgsRequest_10, IArgsResponseOk_10> Create_10 { get; }
    }
}
