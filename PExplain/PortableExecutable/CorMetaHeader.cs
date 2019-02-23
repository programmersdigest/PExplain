using System.IO;

namespace PExplain.PortableExecutable
{
    public class CorMetaHeader
    {
        public Info<uint> Size { get; }
        public Info<ushort> MajorRuntimeVersion { get; }
        public Info<ushort> MinorRuntimeVersion { get; }
        public DataDirectory MetaData { get; }
        public Info<CorFlags> Flags { get; }
        public Info<uint> EntryPointVirtualAddress { get; }
        public DataDirectory Resources { get; }
        public DataDirectory StrongNameSignature { get; }
        public DataDirectory CodeManagerTable { get; }
        public DataDirectory VTableFixups { get; }
        public DataDirectory ExportAddressTableJumps { get; }
        public DataDirectory ManagedNativeHeader { get; }

        public CorMetaHeader(PeInfoReader reader)
        {
            Size = reader.ReadDWord();
            MajorRuntimeVersion = reader.ReadWord();
            MinorRuntimeVersion = reader.ReadWord();
            MetaData = new DataDirectory(reader);
            Flags = reader.ReadDWordAsEnum<CorFlags>();
            EntryPointVirtualAddress = reader.ReadDWord();
            Resources = new DataDirectory(reader);
            StrongNameSignature = new DataDirectory(reader);
            CodeManagerTable = new DataDirectory(reader);
            VTableFixups = new DataDirectory(reader);
            ExportAddressTableJumps = new DataDirectory(reader);
            ManagedNativeHeader = new DataDirectory(reader);
        }
    }
}