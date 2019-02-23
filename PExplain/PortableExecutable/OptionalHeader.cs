namespace PExplain.PortableExecutable
{
    public class OptionalHeader
    {
        public OptionalHeaderStandardFields StandardFields { get; }
        public OptionalHeaderWindowsSpecificFields WindowsSpecificFields { get; }
        public DataDirectories DataDirectories { get; }

        public OptionalHeader(PeInfoReader reader)
        {
            StandardFields = new OptionalHeaderStandardFields(reader);
            WindowsSpecificFields = new OptionalHeaderWindowsSpecificFields(reader, StandardFields.Magic.Value);
            DataDirectories = new DataDirectories(reader, WindowsSpecificFields.NumberOfRvaAndSizes.Value);
        }
    }
}