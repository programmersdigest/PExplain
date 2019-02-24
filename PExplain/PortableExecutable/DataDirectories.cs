using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PExplain.PortableExecutable
{
    public class DataDirectories : IEnumerable<DataDirectory>
    {
        private readonly List<DataDirectory> _directories = new List<DataDirectory>();

        public DataDirectory ExportTable => _directories.ElementAtOrDefault(0) ?? DataDirectory.Empty;
        public DataDirectory ImportTable => _directories.ElementAtOrDefault(1) ?? DataDirectory.Empty;
        public DataDirectory ResourceTable => _directories.ElementAtOrDefault(2) ?? DataDirectory.Empty;
        public DataDirectory ExceptionTable => _directories.ElementAtOrDefault(3) ?? DataDirectory.Empty;
        public DataDirectory CertificateTable => _directories.ElementAtOrDefault(4) ?? DataDirectory.Empty;
        public DataDirectory BaseRelocationTable => _directories.ElementAtOrDefault(5) ?? DataDirectory.Empty;
        public DataDirectory Debug => _directories.ElementAtOrDefault(6) ?? DataDirectory.Empty;
        public DataDirectory Architecture => _directories.ElementAtOrDefault(7) ?? DataDirectory.Empty;
        public DataDirectory GlobalPtr => _directories.ElementAtOrDefault(8) ?? DataDirectory.Empty;
        public DataDirectory TlsTable => _directories.ElementAtOrDefault(9) ?? DataDirectory.Empty;
        public DataDirectory LoadConfigTable => _directories.ElementAtOrDefault(10) ?? DataDirectory.Empty;
        public DataDirectory BoundImport => _directories.ElementAtOrDefault(11) ?? DataDirectory.Empty;
        public DataDirectory IAT => _directories.ElementAtOrDefault(12) ?? DataDirectory.Empty;
        public DataDirectory DelayImportDescriptor => _directories.ElementAtOrDefault(13) ?? DataDirectory.Empty;
        public DataDirectory ClrRuntimeHeader => _directories.ElementAtOrDefault(14) ?? DataDirectory.Empty;
        public DataDirectory Reserved => _directories.ElementAtOrDefault(15) ?? DataDirectory.Empty;

        public DataDirectories(PeInfoReader reader, uint numberOfRvaAndSizes)
        {
            for (uint i = 0; i < numberOfRvaAndSizes; i++)
            {
                _directories.Add(new DataDirectory(reader));
            }
        }

        public uint ComputeDirectoryOffset(DataDirectory dataDirectory)
        {
            if (dataDirectory == DataDirectory.Empty)
            {
                return 0;
            }

            var directoryIndex = _directories.IndexOf(dataDirectory);
            uint offset = 0;
            for (var i = 0; i < directoryIndex; i++)
            {
                offset += _directories[i].Size.Value;
            }
            return offset;
        }

        public IEnumerator<DataDirectory> GetEnumerator()
        {
            return _directories.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _directories.GetEnumerator();
        }
    }
}