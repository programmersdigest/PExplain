using System.IO;
using System.Text;

namespace PExplain.PortableExecutable
{
    public class DosHeader
    {
        public Info<string> FileSignature { get; set; }
        public Info<ushort> BytesOnLastPage { get; set; }
        public Info<ushort> PagesInFile { get; set; }
        public Info<ushort> Relocations { get; set; }
        public Info<ushort> SizeOfHeader { get; set; }
        public Info<ushort> MinExtraParagraphs { get; set; }
        public Info<ushort> MaxExtraParagraphs { get; set; }
        public Info<ushort> InitialSS { get; set; }
        public Info<ushort> InitialSP { get; set; }
        public Info<ushort> Checksum { get; set; }
        public Info<ushort> InitialIP { get; set; }
        public Info<ushort> InitialCS { get; set; }
        public Info<ushort> RelocTableAddress { get; set; }
        public Info<ushort> OverlayNumber { get; set; }
        public Info<ushort> Reserved01 { get; set; }
        public Info<ushort> Reserved02 { get; set; }
        public Info<ushort> Reserved03 { get; set; }
        public Info<ushort> Reserved04 { get; set; }
        public Info<ushort> OEMIdentifier { get; set; }
        public Info<ushort> OEMInfo { get; set; }
        public Info<ushort> Reserved05 { get; set; }
        public Info<ushort> Reserved06 { get; set; }
        public Info<ushort> Reserved07 { get; set; }
        public Info<ushort> Reserved08 { get; set; }
        public Info<ushort> Reserved09 { get; set; }
        public Info<ushort> Reserved10 { get; set; }
        public Info<ushort> Reserved11 { get; set; }
        public Info<ushort> Reserved12 { get; set; }
        public Info<ushort> Reserved13 { get; set; }
        public Info<ushort> Reserved14 { get; set; }
        public Info<ushort> CoffHeaderAddress { get; set; }
        public Info<string> DosStub { get; set; }

        public DosHeader(PeInfoReader reader)
        {
            FileSignature = reader.ReadString(2, Encoding.ASCII);
            BytesOnLastPage = reader.ReadWord();
            PagesInFile = reader.ReadWord();
            Relocations = reader.ReadWord();
            SizeOfHeader = reader.ReadWord();
            MinExtraParagraphs = reader.ReadWord();
            MaxExtraParagraphs = reader.ReadWord();
            InitialSS = reader.ReadWord();
            InitialSP = reader.ReadWord();
            Checksum = reader.ReadWord();
            InitialIP = reader.ReadWord();
            InitialCS = reader.ReadWord();
            RelocTableAddress = reader.ReadWord();
            OverlayNumber = reader.ReadWord();
            Reserved01 = reader.ReadWord();
            Reserved02 = reader.ReadWord();
            Reserved03 = reader.ReadWord();
            Reserved04 = reader.ReadWord();
            OEMIdentifier = reader.ReadWord();
            OEMInfo = reader.ReadWord();
            Reserved05 = reader.ReadWord();
            Reserved06 = reader.ReadWord();
            Reserved07 = reader.ReadWord();
            Reserved08 = reader.ReadWord();
            Reserved09 = reader.ReadWord();
            Reserved10 = reader.ReadWord();
            Reserved11 = reader.ReadWord();
            Reserved12 = reader.ReadWord();
            Reserved13 = reader.ReadWord();
            Reserved14 = reader.ReadWord();
            CoffHeaderAddress = reader.ReadWord();
            DosStub = reader.ReadString(CoffHeaderAddress.Value - (int)reader.BaseStream.Position, Encoding.ASCII);
        }
    }
}