namespace PExplain.PortableExecutable
{
    public class OptionalHeaderWindowsSpecificFields
    {
        public Info<ulong> ImageBase { get; }
        public Info<uint> SectionAlignment { get; }
        public Info<uint> FileAlignment { get; }
        public Info<ushort> MajorOperatingSystemVersion { get; }
        public Info<ushort> MinorOperatingSystemVersion { get; }
        public Info<ushort> MajorImageVersion { get; }
        public Info<ushort> MinorImageVersion { get; }
        public Info<ushort> MajorSubsystemVersion { get; }
        public Info<ushort> MinorSubsystemVersion { get; }
        public Info<uint> Win32VersionValue { get; }
        public Info<uint> SizeOfImage { get; }
        public Info<uint> SizeOfHeaders { get; }
        public Info<uint> CheckSum { get; }
        public Info<WindowsSubsystems> Subsystem { get; }
        public Info<DllCharacteristics> DllCharacteristics { get; }
        public Info<ulong> SizeOfStackReserve { get; }
        public Info<ulong> SizeOfStackCommit { get; }
        public Info<ulong> SizeOfHeapReserve { get; }
        public Info<ulong> SizeOfHeapCommit { get; }
        public Info<uint> LoaderFlags { get; }
        public Info<uint> NumberOfRvaAndSizes { get; }

        public OptionalHeaderWindowsSpecificFields(PeInfoReader reader, OptionalHeaderMagic magic)
        {
            if (magic == OptionalHeaderMagic.PE32)
            {
                ImageBase = Info<ulong>.ConvertFrom(reader.ReadDWord());
            }
            else
            {
                ImageBase = reader.ReadQWord();
            }

            SectionAlignment = reader.ReadDWord();
            FileAlignment = reader.ReadDWord();
            MajorOperatingSystemVersion = reader.ReadWord();
            MinorOperatingSystemVersion = reader.ReadWord();
            MajorImageVersion = reader.ReadWord();
            MinorImageVersion = reader.ReadWord();
            MajorSubsystemVersion = reader.ReadWord();
            MinorSubsystemVersion = reader.ReadWord();
            Win32VersionValue = reader.ReadDWord();
            SizeOfImage = reader.ReadDWord();
            SizeOfHeaders = reader.ReadDWord();
            CheckSum = reader.ReadDWord();
            Subsystem = reader.ReadWordAsEnum<WindowsSubsystems>();
            DllCharacteristics = reader.ReadWordAsEnum<DllCharacteristics>();

            if (magic == OptionalHeaderMagic.PE32)
            {
                SizeOfStackReserve = Info<ulong>.ConvertFrom(reader.ReadDWord());
                SizeOfStackCommit = Info<ulong>.ConvertFrom(reader.ReadDWord());
                SizeOfHeapReserve = Info<ulong>.ConvertFrom(reader.ReadDWord());
                SizeOfHeapCommit = Info<ulong>.ConvertFrom(reader.ReadDWord());
            }
            else
            {
                SizeOfStackReserve = reader.ReadQWord();
                SizeOfStackCommit = reader.ReadQWord();
                SizeOfHeapReserve = reader.ReadQWord();
                SizeOfHeapCommit = reader.ReadQWord();
            }

            LoaderFlags = reader.ReadDWord();
            NumberOfRvaAndSizes = reader.ReadDWord();
        }
    }
}