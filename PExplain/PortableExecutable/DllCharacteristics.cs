using System;

namespace PExplain.PortableExecutable
{
    [Flags]
    public enum DllCharacteristics : ushort
    {
        HighEntropyVA = 0x0020,
        DynamicBase = 0x0040,
        ForceIntegrity = 0x0080,
        NxCompat = 0x0100,
        NoIsolation = 0x200,
        NoSEH = 0x0400,
        NoBind = 0x0800,
        AppContainer = 0x1000,
        WdmDriver = 0x2000,
        GuardCF = 0x4000,
        TerminalServerAware = 0x8000
    }
}