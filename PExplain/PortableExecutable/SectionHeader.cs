using System.Text;

namespace PExplain.PortableExecutable
{
    public class SectionHeader
    {
        public Info<string> Name { get; }
        public Info<uint> VirtualSize { get; }
        public Info<uint> VirtualAddress { get; }
        public Info<uint> SizeOfRawData { get; }
        public Info<uint> PointerToRawData { get; }
        public Info<uint> PointerToRelocations { get; }
        public Info<uint> PointerToLinenumbers { get; }
        public Info<ushort> NumberOfRelocations { get; }
        public Info<ushort> NumberOfLinenumbers { get; }
        public Info<SectionFlags> Characteristics { get; }

        public SectionHeader(PeInfoReader reader)
        {
            Name = reader.ReadString(8, Encoding.UTF8);
            VirtualSize = reader.ReadDWord();
            VirtualAddress = reader.ReadDWord();
            SizeOfRawData = reader.ReadDWord();
            PointerToRawData = reader.ReadDWord();
            PointerToRelocations = reader.ReadDWord();
            PointerToLinenumbers = reader.ReadDWord();
            NumberOfRelocations = reader.ReadWord();
            NumberOfLinenumbers = reader.ReadWord();
            Characteristics = reader.ReadDWordAsEnum<SectionFlags>();
        }
    }
}