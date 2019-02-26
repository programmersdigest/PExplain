[![Build status](https://ci.appveyor.com/api/projects/status/github/programmersdigest/PExplain?branch=master&svg=true)](https://ci.appveyor.com/api/projects/status/github/programmersdigest/PExplain?branch=master&svg=true)
# PExplain
Prints detailed info on the contents of a PE file.

## Status
This is a work in progress. PExplain can currently read the following parts of a PE file:
- MS-DOS header (including MS-DOS stub)
- COFF header (including Windows specific fields)
- Section table
- COR header

# Usage
PExplain <pathToPEFile>

# Output (shortened to DOS header only)
```
--- MS-DOS Header --------
    |              Field | Offset | Width |                   Bytes |                                    Value |
    |      FileSignature |      0 |     2 |                   4D 5A |                                       MZ |
    |    BytesOnLastPage |      2 |     2 |                   90 00 |                                      144 |
    |        PagesInFile |      4 |     2 |                   03 00 |                                        3 |
    |        Relocations |      6 |     2 |                   00 00 |                                        0 |
    |       SizeOfHeader |      8 |     2 |                   04 00 |                                        4 |
    | MinExtraParagraphs |     10 |     2 |                   00 00 |                                        0 |
    | MaxExtraParagraphs |     12 |     2 |                   FF FF |                                    65535 |
    |          InitialSS |     14 |     2 |                   00 00 |                                        0 |
    |          InitialSP |     16 |     2 |                   B8 00 |                                      184 |
    |           Checksum |     18 |     2 |                   00 00 |                                        0 |
    |          InitialIP |     20 |     2 |                   00 00 |                                        0 |
    |          InitialCS |     22 |     2 |                   00 00 |                                        0 |
    |  RelocTableAddress |     24 |     2 |                   40 00 |                                       64 |
    |      OverlayNumber |     26 |     2 |                   00 00 |                                        0 |
    |         Reserved01 |     28 |     2 |                   00 00 |                                        0 |
    |         Reserved02 |     30 |     2 |                   00 00 |                                        0 |
    |         Reserved03 |     32 |     2 |                   00 00 |                                        0 |
    |         Reserved04 |     34 |     2 |                   00 00 |                                        0 |
    |      OEMIdentifier |     36 |     2 |                   00 00 |                                        0 |
    |            OEMInfo |     38 |     2 |                   00 00 |                                        0 |
    |         Reserved05 |     40 |     2 |                   00 00 |                                        0 |
    |         Reserved06 |     42 |     2 |                   00 00 |                                        0 |
    |         Reserved07 |     44 |     2 |                   00 00 |                                        0 |
    |         Reserved08 |     46 |     2 |                   00 00 |                                        0 |
    |         Reserved09 |     48 |     2 |                   00 00 |                                        0 |
    |         Reserved10 |     50 |     2 |                   00 00 |                                        0 |
    |         Reserved11 |     52 |     2 |                   00 00 |                                        0 |
    |         Reserved12 |     54 |     2 |                   00 00 |                                        0 |
    |         Reserved13 |     56 |     2 |                   00 00 |                                        0 |
    |         Reserved14 |     58 |     2 |                   00 00 |                                        0 |
    |  CoffHeaderAddress |     60 |     2 |                   80 00 |                                      128 |
    |            DosStub |     62 |    66 | 00 00 0E 1F BA 0E 00 B4 | \0\0\u000e\u001f?\u000e\0?\t?!?\u0001L?! |
    |                    |        |       | 09 CD 21 B8 01 4C CD 21 | This program cannot be run in DOS mode.\ |
    |                    |        |       | 54 68 69 73 20 70 72 6F |                     r\r\n$\0\0\0\0\0\0\0 |
```

# Relevant Materials
Format of PE files: https://docs.microsoft.com/en-us/windows/desktop/Debug/pe-format
Building a disasembler: https://codingwithspike.wordpress.com/2012/08/12/building-a-net-disassembler-part-3-parsing-the-text-section/
System.Reflection.PortableExecutable: https://github.com/dotnet/corefx/tree/master/src/System.Reflection.Metadata/src/System/Reflection/PortableExecutable
x86 Disassembly/Windows Executable Files: https://en.wikibooks.org/wiki/X86_Disassembly/Windows_Executable_Files
