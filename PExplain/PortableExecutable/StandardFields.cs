namespace PExplain.PortableExecutable
{
    public class OptionalHeaderStandardFields
    {
        public Info<OptionalHeaderMagic> Magic { get; }
        public Info<byte> MajorLinkerVersion { get; }
        public Info<byte> MinorLinkerVersion { get; }
        public Info<uint> SizeOfCode { get; }
        public Info<uint> SizeOfInitializedData { get; }
        public Info<uint> SizeOfUninitializedData { get; }
        public Info<uint> AddressOfEntryPoint { get; }
        public Info<uint> BaseOfCode { get; }
        public Info<uint> BaseOfData { get; }

        public OptionalHeaderStandardFields(PeInfoReader reader)
        {
            Magic = reader.ReadWordAsEnum<OptionalHeaderMagic>();
            MajorLinkerVersion = reader.ReadByte();
            MinorLinkerVersion = reader.ReadByte();
            SizeOfCode = reader.ReadDWord();
            SizeOfInitializedData = reader.ReadDWord();
            SizeOfUninitializedData = reader.ReadDWord();
            AddressOfEntryPoint = reader.ReadDWord();
            BaseOfCode = reader.ReadDWord();

            if (Magic.Value == OptionalHeaderMagic.PE32)
            {
                BaseOfData = reader.ReadDWord();
            }
        }
    }
}