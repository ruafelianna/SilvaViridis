using SilvaViridis.Common.Localization.Abstractions;
using SilvaViridis.Exe.DeviceConfiguration.Client.Assets.Translations;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Connections.Enums;
using SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Protocols.Enums;
using System;
using System.Collections.Generic;

namespace SilvaViridis.Exe.DeviceConfiguration.Client.ViewModels.Interfaces.Connections.DTOs
{
    public record ConnectionOption(
        AvailableConnections Connection,
        ITranslationUnit Name,
        IEnumerable<AvailableProtocols> SupportedProtocols
    )
    {
        static ConnectionOption()
        {
            SerialPort = new(
                AvailableConnections.SerialPort,
                Strings.Conn_SerialPort,
                [
                    AvailableProtocols.ModbusRTU,
                ]
            );
        }

        public static ConnectionOption SerialPort { get; }

        public static ConnectionOption Create(AvailableConnections connection)
            => connection switch {
                AvailableConnections.SerialPort => SerialPort,
                _ => throw new NotImplementedException(),
            };
    }
}
