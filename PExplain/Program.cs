using PExplain.PortableExecutable;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PExplain
{
    class Program
    {
        private const string _rowFormat = "| {0,27} | {1,6} | {2,5} | {3,23} | {4,64} |";

        static void Main(string[] args)
        {
            var path = args.FirstOrDefault();
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                PrintHelp();
                return;
            }

            var peFile = new PeFile(path);
            PrintTitle(path);
            PrintDosHeader(peFile.DosHeader);
            PrintCoffHeader(peFile.CoffFileHeader);
            PrintOptionalHeaderStandardFields(peFile.OptionalHeader.StandardFields);
            PrintOptionalHeaderWindowsSpecificFields(peFile.OptionalHeader.WindowsSpecificFields);
            PrintOptionalHeaderDataDirectories(peFile.OptionalHeader.DataDirectories);
            PrintSectionTable(peFile.SectionTable);
            PrintCorMetaHeader(peFile.CorMetaHeader);
        }

        private static void PrintTitle(string path)
        {
            Console.WriteLine($"PExplain of \"{path}\"");
        }

        private static void PrintDosHeader(DosHeader dosHeader)
        {
            Console.WriteLine();
            Console.WriteLine("MS-DOS Header");
            PrintTableHeaders();
            PrintInfo(nameof(dosHeader.Magic), dosHeader.Magic);
            PrintInfo(nameof(dosHeader.BytesOnLastPage), dosHeader.BytesOnLastPage);
            PrintInfo(nameof(dosHeader.PagesInFile), dosHeader.PagesInFile);
            PrintInfo(nameof(dosHeader.Relocations), dosHeader.Relocations);
            PrintInfo(nameof(dosHeader.SizeOfHeader), dosHeader.SizeOfHeader);
            PrintInfo(nameof(dosHeader.MinExtraParagraphs), dosHeader.MinExtraParagraphs);
            PrintInfo(nameof(dosHeader.MaxExtraParagraphs), dosHeader.MaxExtraParagraphs);
            PrintInfo(nameof(dosHeader.InitialSS), dosHeader.InitialSS);
            PrintInfo(nameof(dosHeader.InitialSP), dosHeader.InitialSP);
            PrintInfo(nameof(dosHeader.Checksum), dosHeader.Checksum);
            PrintInfo(nameof(dosHeader.InitialIP), dosHeader.InitialIP);
            PrintInfo(nameof(dosHeader.InitialCS), dosHeader.InitialCS);
            PrintInfo(nameof(dosHeader.RelocTableAddress), dosHeader.RelocTableAddress);
            PrintInfo(nameof(dosHeader.OverlayNumber), dosHeader.OverlayNumber);
            PrintInfo(nameof(dosHeader.Reserved01), dosHeader.Reserved01);
            PrintInfo(nameof(dosHeader.Reserved02), dosHeader.Reserved02);
            PrintInfo(nameof(dosHeader.Reserved03), dosHeader.Reserved03);
            PrintInfo(nameof(dosHeader.Reserved04), dosHeader.Reserved04);
            PrintInfo(nameof(dosHeader.OEMIdentifier), dosHeader.OEMIdentifier);
            PrintInfo(nameof(dosHeader.OEMInfo), dosHeader.OEMInfo);
            PrintInfo(nameof(dosHeader.Reserved05), dosHeader.Reserved05);
            PrintInfo(nameof(dosHeader.Reserved06), dosHeader.Reserved06);
            PrintInfo(nameof(dosHeader.Reserved07), dosHeader.Reserved07);
            PrintInfo(nameof(dosHeader.Reserved08), dosHeader.Reserved08);
            PrintInfo(nameof(dosHeader.Reserved09), dosHeader.Reserved09);
            PrintInfo(nameof(dosHeader.Reserved10), dosHeader.Reserved10);
            PrintInfo(nameof(dosHeader.Reserved11), dosHeader.Reserved11);
            PrintInfo(nameof(dosHeader.Reserved12), dosHeader.Reserved12);
            PrintInfo(nameof(dosHeader.Reserved13), dosHeader.Reserved13);
            PrintInfo(nameof(dosHeader.Reserved14), dosHeader.Reserved14);
            PrintInfo(nameof(dosHeader.CoffHeaderAddress), dosHeader.CoffHeaderAddress);
        }

        private static void PrintCoffHeader(CoffFileHeader coffHeader)
        {
            Console.WriteLine();
            Console.WriteLine("COFF File Header");
            PrintTableHeaders();
            PrintInfo(nameof(coffHeader.PeSignature), coffHeader.PeSignature);
            PrintInfo(nameof(coffHeader.Machine), coffHeader.Machine);
            PrintInfo(nameof(coffHeader.NumberOfSections), coffHeader.NumberOfSections);
            PrintInfo(nameof(coffHeader.TimeDateStamp), coffHeader.TimeDateStamp);
            PrintInfo(nameof(coffHeader.PointerToSymbolTable), coffHeader.PointerToSymbolTable);
            PrintInfo(nameof(coffHeader.NumberOfSymbols), coffHeader.NumberOfSymbols);
            PrintInfo(nameof(coffHeader.SizeOfOptionalHeader), coffHeader.SizeOfOptionalHeader);
            PrintInfo(nameof(coffHeader.Characteristics), coffHeader.Characteristics);
        }

        private static void PrintOptionalHeaderStandardFields(OptionalHeaderStandardFields standardFields)
        {
            Console.WriteLine();
            Console.WriteLine("Optional Header - Standard Fields");
            PrintTableHeaders();
            PrintInfo(nameof(standardFields.Magic), standardFields.Magic);
            PrintInfo(nameof(standardFields.MajorLinkerVersion), standardFields.MajorLinkerVersion);
            PrintInfo(nameof(standardFields.MinorLinkerVersion), standardFields.MinorLinkerVersion);
            PrintInfo(nameof(standardFields.SizeOfCode), standardFields.SizeOfCode);
            PrintInfo(nameof(standardFields.SizeOfInitializedData), standardFields.SizeOfInitializedData);
            PrintInfo(nameof(standardFields.SizeOfUninitializedData), standardFields.SizeOfUninitializedData);
            PrintInfo(nameof(standardFields.AddressOfEntryPoint), standardFields.AddressOfEntryPoint);
            PrintInfo(nameof(standardFields.BaseOfCode), standardFields.BaseOfCode);
            if (standardFields.Magic.Value == OptionalHeaderMagic.PE32)
            {
                PrintInfo(nameof(standardFields.BaseOfData), standardFields.BaseOfData);
            }
        }

        private static void PrintOptionalHeaderWindowsSpecificFields(OptionalHeaderWindowsSpecificFields windowsSpecificFields)
        {
            Console.WriteLine();
            Console.WriteLine("Optional Header - Windows-Specific Fields");
            PrintTableHeaders();
            PrintInfo(nameof(windowsSpecificFields.ImageBase), windowsSpecificFields.ImageBase);
            PrintInfo(nameof(windowsSpecificFields.SectionAlignment), windowsSpecificFields.SectionAlignment);
            PrintInfo(nameof(windowsSpecificFields.FileAlignment), windowsSpecificFields.FileAlignment);
            PrintInfo(nameof(windowsSpecificFields.MajorOperatingSystemVersion), windowsSpecificFields.MajorOperatingSystemVersion);
            PrintInfo(nameof(windowsSpecificFields.MinorOperatingSystemVersion), windowsSpecificFields.MinorOperatingSystemVersion);
            PrintInfo(nameof(windowsSpecificFields.MajorImageVersion), windowsSpecificFields.MajorImageVersion);
            PrintInfo(nameof(windowsSpecificFields.MinorImageVersion), windowsSpecificFields.MinorImageVersion);
            PrintInfo(nameof(windowsSpecificFields.MajorSubsystemVersion), windowsSpecificFields.MajorSubsystemVersion);
            PrintInfo(nameof(windowsSpecificFields.MinorSubsystemVersion), windowsSpecificFields.MinorSubsystemVersion);
            PrintInfo(nameof(windowsSpecificFields.Win32VersionValue), windowsSpecificFields.Win32VersionValue);
            PrintInfo(nameof(windowsSpecificFields.SizeOfImage), windowsSpecificFields.SizeOfImage);
            PrintInfo(nameof(windowsSpecificFields.SizeOfHeaders), windowsSpecificFields.SizeOfHeaders);
            PrintInfo(nameof(windowsSpecificFields.CheckSum), windowsSpecificFields.CheckSum);
            PrintInfo(nameof(windowsSpecificFields.Subsystem), windowsSpecificFields.Subsystem);
            PrintInfo(nameof(windowsSpecificFields.DllCharacteristics), windowsSpecificFields.DllCharacteristics);
            PrintInfo(nameof(windowsSpecificFields.SizeOfStackReserve), windowsSpecificFields.SizeOfStackReserve);
            PrintInfo(nameof(windowsSpecificFields.SizeOfStackCommit), windowsSpecificFields.SizeOfStackCommit);
            PrintInfo(nameof(windowsSpecificFields.SizeOfHeapReserve), windowsSpecificFields.SizeOfHeapReserve);
            PrintInfo(nameof(windowsSpecificFields.SizeOfHeapCommit), windowsSpecificFields.SizeOfHeapCommit);
            PrintInfo(nameof(windowsSpecificFields.LoaderFlags), windowsSpecificFields.LoaderFlags);
            PrintInfo(nameof(windowsSpecificFields.NumberOfRvaAndSizes), windowsSpecificFields.NumberOfRvaAndSizes);
        }

        private static void PrintOptionalHeaderDataDirectories(DataDirectories dataDirectories)
        {
            Console.WriteLine();
            Console.WriteLine("Optional Header - Data Directories");
            PrintTableHeaders();

            var directoryNames = new[]
            {
                "Export Table", "Import Table", "Resource Table", "Exception Table",
                "Certificate Table", "Base Relocation Table", "Debug", "Architecture",
                "Global Ptr", "TLS Table", "Load Config Table", "Bound Import",
                "IAT", "Delay Import Descriptor", "CLR Runtime Header", "Reserved"
            };

            int index = 0;
            foreach (var dataDirectory in dataDirectories.Directories)
            {
                var name = directoryNames.ElementAtOrDefault(index) ?? "Unknown";
                PrintDataDirectory(name, dataDirectory);

                index++;
            }
        }

        private static void PrintDataDirectory(string name, DataDirectory dataDirectory)
        {
            Console.WriteLine($"{name} (2)");
            PrintInfo(nameof(dataDirectory.VirtualAddress), dataDirectory.VirtualAddress);
            PrintInfo(nameof(dataDirectory.Size), dataDirectory.Size);
        }

        private static void PrintSectionTable(SectionTable sectionTable)
        {
            Console.WriteLine();
            Console.WriteLine("Section Table");
            PrintTableHeaders();

            int index = 0;
            foreach (var section in sectionTable)
            {
                Console.WriteLine($"Section {index}: {section.Name.Value.TrimEnd('\0')} (10)");
                PrintInfo(nameof(section.Name), section.Name);
                PrintInfo(nameof(section.VirtualSize), section.VirtualSize);
                PrintInfo(nameof(section.VirtualAddress), section.VirtualAddress);
                PrintInfo(nameof(section.SizeOfRawData), section.SizeOfRawData);
                PrintInfo(nameof(section.PointerToRawData), section.PointerToRawData);
                PrintInfo(nameof(section.PointerToRelocations), section.PointerToRelocations);
                PrintInfo(nameof(section.PointerToLinenumbers), section.PointerToLinenumbers);
                PrintInfo(nameof(section.NumberOfRelocations), section.NumberOfRelocations);
                PrintInfo(nameof(section.NumberOfLinenumbers), section.NumberOfLinenumbers);
                PrintInfo(nameof(section.Characteristics), section.Characteristics);

                index++;
            }
        }

        private static void PrintCorMetaHeader(CorMetaHeader corMetaHeader)
        {
            if (corMetaHeader == null)
            {
                Console.WriteLine("The PE file does not contain a .cormeta header aka CLR header.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine(".cormeta Header aka CLR header");
            PrintTableHeaders();

            PrintInfo(nameof(corMetaHeader.Size), corMetaHeader.Size);
            PrintInfo(nameof(corMetaHeader.MajorRuntimeVersion), corMetaHeader.MajorRuntimeVersion);
            PrintInfo(nameof(corMetaHeader.MinorRuntimeVersion), corMetaHeader.MinorRuntimeVersion);
            PrintDataDirectory(nameof(corMetaHeader.MetaData), corMetaHeader.MetaData);
            PrintInfo(nameof(corMetaHeader.Flags), corMetaHeader.Flags);
            PrintInfo(nameof(corMetaHeader.EntryPointVirtualAddress), corMetaHeader.EntryPointVirtualAddress);
            PrintDataDirectory(nameof(corMetaHeader.Resources), corMetaHeader.Resources);
            PrintDataDirectory(nameof(corMetaHeader.StrongNameSignature), corMetaHeader.StrongNameSignature);
            PrintDataDirectory(nameof(corMetaHeader.CodeManagerTable), corMetaHeader.CodeManagerTable);
            PrintDataDirectory(nameof(corMetaHeader.VTableFixups), corMetaHeader.VTableFixups);
            PrintDataDirectory(nameof(corMetaHeader.ExportAddressTableJumps), corMetaHeader.ExportAddressTableJumps);
            PrintDataDirectory(nameof(corMetaHeader.ManagedNativeHeader), corMetaHeader.ManagedNativeHeader);
        }

        private static void PrintTableHeaders()
        {
            Console.WriteLine(_rowFormat, "Field", "Offset", "Size", "Raw Bytes", "Value");
        }

        private static void PrintInfo<T>(string fieldName, Info<T> info)
        {
            object value;
            if (typeof(T) == typeof(string))
            {
                value = ToLiteral(info.Value.ToString());
            }
            else
            {
                value = info.Value;
            }

            Console.WriteLine(_rowFormat, fieldName, info.Offset, info.Size, ToHexString(info.Bytes), value);
        }

        private static void PrintHelp()
        {
            Console.WriteLine($"PExplain {Assembly.GetExecutingAssembly().GetName().Version}");
            Console.WriteLine($"Usage: PExplain peFilePath");
        }

        private static string ToLiteral(string input)
        {
            using (var writer = new StringWriter())
            {
                using (var provider = CodeDomProvider.CreateProvider("CSharp"))
                {
                    provider.GenerateCodeFromExpression(new CodePrimitiveExpression(input), writer, null);
                    return writer.ToString();
                }
            }
        }

        private static string ToHexString(byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace('-', ' ');
        }
    }
}
