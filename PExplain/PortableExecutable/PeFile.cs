using System.IO;
using System.Linq;

namespace PExplain.PortableExecutable
{
    public class PeFile
    {
        private readonly long _coffFileHeaderOffset;
        private readonly long _optionalHeaderOffset;
        private readonly long _sectionTableOffset;

        public string Path { get; }
        public DosHeader DosHeader { get; }
        public CoffFileHeader CoffFileHeader { get; }
        public OptionalHeader OptionalHeader { get; }
        public SectionTable SectionTable { get; }
        public CorMetaHeader CorHeader { get; }

        public PeFile(string path)
        {
            Path = path;

            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var reader = new PeInfoReader(stream))
            {
                DosHeader = new DosHeader(reader);

                stream.Seek(DosHeader.CoffHeaderAddress.Value, SeekOrigin.Begin);

                _coffFileHeaderOffset = stream.Position;
                CoffFileHeader = new CoffFileHeader(reader);

                _optionalHeaderOffset = stream.Position;
                OptionalHeader = new OptionalHeader(reader);

                _sectionTableOffset = _optionalHeaderOffset + CoffFileHeader.SizeOfOptionalHeader.Value;
                stream.Seek(_sectionTableOffset, SeekOrigin.Begin);
                SectionTable = new SectionTable(reader, CoffFileHeader.NumberOfSections.Value);

                // Read CLR header.
                var clrHeaderDirectory = OptionalHeader.DataDirectories.ClrRuntimeHeader;
                if (clrHeaderDirectory != DataDirectory.Empty)
                {
                    var section = SectionTable.First(s => s.VirtualAddress.Value <= clrHeaderDirectory.VirtualAddress.Value &&
                                                          s.VirtualAddress.Value + s.VirtualSize.Value >= clrHeaderDirectory.VirtualAddress.Value);
                    var clrHeaderOffset = section.PointerToRawData.Value + (clrHeaderDirectory.VirtualAddress.Value - section.VirtualAddress.Value);

                    stream.Seek(clrHeaderOffset, SeekOrigin.Begin);
                    CorHeader = new CorMetaHeader(reader);
                }
            }
        }
    }
}
