namespace PExplain.PortableExecutable
{
    public enum OptionalHeaderMagic : ushort
    {
        ROM = 0x107,
        PE32 = 0x10b,
        PE32Plus = 0x20b
    }
}