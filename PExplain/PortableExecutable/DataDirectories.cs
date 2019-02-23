using System.Collections.Generic;
using System.Linq;

namespace PExplain.PortableExecutable
{
    public class DataDirectories
    {
        public readonly List<DataDirectory> Directories = new List<DataDirectory>();

        public DataDirectory ExportTable => Directories.ElementAtOrDefault(0) ?? DataDirectory.Empty;
        public DataDirectory ImportTable => Directories.ElementAtOrDefault(1) ?? DataDirectory.Empty;
        public DataDirectory ResourceTable => Directories.ElementAtOrDefault(2) ?? DataDirectory.Empty;
        public DataDirectory ExceptionTable => Directories.ElementAtOrDefault(3) ?? DataDirectory.Empty;
        public DataDirectory CertificateTable => Directories.ElementAtOrDefault(4) ?? DataDirectory.Empty;
        public DataDirectory BaseRelocationTable => Directories.ElementAtOrDefault(5) ?? DataDirectory.Empty;
        public DataDirectory Debug => Directories.ElementAtOrDefault(6) ?? DataDirectory.Empty;
        public DataDirectory Architecture => Directories.ElementAtOrDefault(7) ?? DataDirectory.Empty;
        public DataDirectory GlobalPtr => Directories.ElementAtOrDefault(8) ?? DataDirectory.Empty;
        public DataDirectory TlsTable => Directories.ElementAtOrDefault(9) ?? DataDirectory.Empty;
        public DataDirectory LoadConfigTable => Directories.ElementAtOrDefault(10) ?? DataDirectory.Empty;
        public DataDirectory BoundImport => Directories.ElementAtOrDefault(11) ?? DataDirectory.Empty;
        public DataDirectory IAT => Directories.ElementAtOrDefault(12) ?? DataDirectory.Empty;
        public DataDirectory DelayImportDescriptor => Directories.ElementAtOrDefault(13) ?? DataDirectory.Empty;
        public DataDirectory ClrRuntimeHeader => Directories.ElementAtOrDefault(14) ?? DataDirectory.Empty;
        public DataDirectory Reserved => Directories.ElementAtOrDefault(15) ?? DataDirectory.Empty;

        public DataDirectories(PeInfoReader reader, uint numberOfRvaAndSizes)
        {
            for (uint i = 0; i < numberOfRvaAndSizes; i++)
            {
                Directories.Add(new DataDirectory(reader));
            }
        }

        public uint ComputeDirectoryOffset(DataDirectory dataDirectory)
        {
            if (dataDirectory == DataDirectory.Empty)
            {
                return 0;
            }

            var directoryIndex = Directories.IndexOf(dataDirectory);
            uint offset = 0;
            for (var i = 0; i < directoryIndex; i++)
            {
                offset += Directories[i].Size.Value;
            }
            return offset;
        }
    }
}