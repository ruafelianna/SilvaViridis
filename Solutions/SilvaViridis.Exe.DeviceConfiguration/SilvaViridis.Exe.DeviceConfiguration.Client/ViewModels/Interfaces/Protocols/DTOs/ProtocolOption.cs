using SilvaViridis.Common.Localization.Abstractions;
using SilvaViridis.Exe.DeviceConfiguration.Client.Assets.Translations;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Protocols.Enums;
using System;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Protocols.DTOs
{
    public record ProtocolOption(
        AvailableProtocols Protocol,
        ITranslationUnit Name
    )
    {
        static ProtocolOption()
        {
            ModbusRTU = new(
                AvailableProtocols.ModbusRTU,
                Strings.Prot_ModbusRTU
            );
        }

        public static ProtocolOption ModbusRTU { get; }

        public static ProtocolOption Create(AvailableProtocols protocol)
            => protocol switch
            {
                AvailableProtocols.ModbusRTU => ModbusRTU,
                _ => throw new NotImplementedException(),
            };
    }
}
