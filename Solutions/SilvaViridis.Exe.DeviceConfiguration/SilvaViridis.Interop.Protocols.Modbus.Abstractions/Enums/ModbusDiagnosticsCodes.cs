namespace SilvaViridis.Interop.Protocols.Modbus.Abstractions.Enums
{
    public enum ModbusDiagnosticsCodes : ushort
    {
        ReturnQueryData = 0x0000,

        RestartCommunicationsOption = 0x0001,

        ReturnDiagnosticRegister = 0x0002,

        ChangeASCIIInputDelimiter = 0x0003,

        ForceListenOnlyMode = 0x0004,

        ClearCountersAndDiagnosticRegister = 0x000A,

        ReturnBusMessageCount = 0x000B,

        ReturnBusCommunicationErrorCount = 0x000C,

        ReturnBusExceptionErrorCount = 0x000D,

        ReturnServerMessageCount = 0x000E,

        ReturnServerNoResponseCount = 0x000F,

        ReturnServerNAKCount = 0x0010,

        ReturnServerBusyCount = 0x0011,

        ReturnBusCharacterOverrunCount = 0x0012,

        ClearOverrunCounterAndFlag = 0x0014,
    }
}
