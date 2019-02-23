namespace PExplain.PortableExecutable
{
    public class DataDirectory
    {
        public static readonly DataDirectory Empty = new DataDirectory();

        public Info<uint> VirtualAddress { get; }
        public Info<uint> Size { get; }

        public DataDirectory(PeInfoReader reader)
        {
            VirtualAddress = reader.ReadDWord();
            Size = reader.ReadDWord();
        }

        private DataDirectory()
        {
        }
    }
}