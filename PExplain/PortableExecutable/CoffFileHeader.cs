using System.Text;

namespace PExplain.PortableExecutable
{
    public class CoffFileHeader
    {
        public Info<string> PeSignature { get; }
        public Info<MachineTypes> Machine { get; }
        public Info<ushort> NumberOfSections { get; }
        public Info<uint> TimeDateStamp { get; }
        public Info<uint> PointerToSymbolTable { get; }
        public Info<uint> NumberOfSymbols { get; }
        public Info<ushort> SizeOfOptionalHeader { get; }
        public Info<Characteristics> Characteristics { get; }

        public CoffFileHeader(PeInfoReader reader)
        {
            PeSignature = reader.ReadString(4, Encoding.ASCII);
            Machine = reader.ReadWordAsEnum<MachineTypes>();
            NumberOfSections = reader.ReadWord();
            TimeDateStamp = reader.ReadDWord();
            PointerToSymbolTable = reader.ReadDWord();
            NumberOfSymbols = reader.ReadDWord();
            SizeOfOptionalHeader = reader.ReadWord();
            Characteristics = reader.ReadWordAsEnum<Characteristics>();
        }
    }
}