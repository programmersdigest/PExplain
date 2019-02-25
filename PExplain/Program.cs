using PExplain.Output;
using PExplain.PortableExecutable;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace PExplain
{
    class Program
    {
        private const int _maxRawDataColumnWidth = 23;
        private const int _maxValueColumnWidth = 40;

        static void Main(string[] args)
        {
            var path = args.FirstOrDefault();
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                PrintHelp();
                return;
            }

            var peFile = new PeFile(path);
            var resultTable = PrepareResultTable(peFile);
            PrintResultTableToConsole(resultTable);
        }

        private static void PrintResultTableToConsole(Table table, int level = 0)
        {
            var indentation = new string(' ', level * 4);

            switch (level)
            {
                case 0:
                    Console.WriteLine($"{table.Name}");
                    break;
                case 1:
                    Console.WriteLine();
                    Console.WriteLine($"--- {table.Name} --------");
                    break;
                default:
                    Console.WriteLine($"{indentation}{table.Name}:");
                    break;
            }

            string rowFormat = null;
            var rows = table.Entries.OfType<Row>().ToList();

            if (rows.Any())
            {
                var fieldWidth = Math.Max(rows.Max(r => r.Field.Length), "Field".Length);
                var offsetWidth = Math.Max(rows.Max(r => r.Offset.Length), "Offset".Length);
                var sizeWidth = Math.Max(rows.Max(r => r.Size.Length), "Width".Length);
                var rawBytesWidth = Math.Min(Math.Max(rows.Max(r => r.RawData.Length), "Bytes".Length), _maxRawDataColumnWidth);
                var valueWidth = Math.Min(Math.Max(rows.Max(r => r.Value.Length), "Value".Length), _maxValueColumnWidth);
                rowFormat = $"{indentation}| {{0,{fieldWidth}}} | {{1,{offsetWidth}}} | {{2,{sizeWidth}}} | {{3,{rawBytesWidth}}} | {{4,{valueWidth}}} |";

                Console.WriteLine(rowFormat, "Field", "Offset", "Width", "Bytes", "Value");
            }

            foreach (var group in table.Entries)
            {
                switch (group)
                {
                    case Table subtable:
                        PrintResultTableToConsole(subtable, level + 1);
                        break;
                    case Row row:
                        var rawDataLines = Regex.Matches(row.RawData, @"( ?[0-9A-F]{2}){1,8}");
                        var valueLines = row.Value.Split(_maxValueColumnWidth);

                        for (int i = 0; i < rawDataLines.Count && i < valueLines.Count(); i++)
                        {
                            var rawData = rawDataLines.Count >= i ? rawDataLines[i].Value.Trim() : "";
                            var value = valueLines.ElementAtOrDefault(i) ?? "";

                            if (i == 0)
                            {
                                Console.WriteLine(rowFormat, row.Field, row.Offset, row.Size, rawData, value);
                            }
                            else
                            {
                                Console.WriteLine(rowFormat, "", "", "", rawData, value);
                            }
                        }

                        break;
                }
            }
        }

        private static Table PrepareResultTable(PeFile peFile)
        {
            var resultRows = new List<Table> {
                new Table("MS-DOS Header", peFile.DosHeader),
                new Table("COFF File Header", peFile.CoffFileHeader),
                new Table("Optional Header - Standard Fields", peFile.OptionalHeader.StandardFields),
                new Table("Optional Header - Windows-Specific Fields", peFile.OptionalHeader.WindowsSpecificFields),
                PrepareOptionalHeaderDataDirectories(peFile.OptionalHeader.DataDirectories),
                new Table("Section Table", peFile.SectionTable.Select(s => new Table(s.Name.Value.TrimEnd('\0'), s)))
            };
            if (peFile.CorHeader != null)
            {
                resultRows.Add(new Table("COR Header", peFile.CorHeader));
            }

            return new Table($"PExplain of \"{peFile.Path}\"", resultRows);
        }

        private static Table PrepareOptionalHeaderDataDirectories(DataDirectories dataDirectories)
        {
            var directoryNames = new[]
            {
                "Export Table", "Import Table", "Resource Table", "Exception Table",
                "Certificate Table", "Base Relocation Table", "Debug", "Architecture",
                "Global Ptr", "TLS Table", "Load Config Table", "Bound Import",
                "IAT", "Delay Import Descriptor", "CLR Runtime Header", "Reserved"
            };

            return new Table("Optional Header - Data Directories",
                dataDirectories.Select((dd, i) => new Table(directoryNames.ElementAtOrDefault(i) ?? "Unknown", dd)));
        }

        private static void PrintHelp()
        {
            Console.WriteLine($"PExplain {Assembly.GetExecutingAssembly().GetName().Version}");
            Console.WriteLine($"Usage: PExplain peFilePath");
        }
    }
}
