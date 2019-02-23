using System.Collections;
using System.Collections.Generic;

namespace PExplain.PortableExecutable
{
    public class SectionTable : IReadOnlyList<SectionHeader>
    {
        public List<SectionHeader> _SectionTable = new List<SectionHeader>();

        public SectionHeader this[int index] => _SectionTable[index];

        public int Count => _SectionTable.Count;

        public IEnumerator<SectionHeader> GetEnumerator()
        {
            return _SectionTable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _SectionTable.GetEnumerator();
        }

        public SectionTable(PeInfoReader reader, ushort numberOfSections)
        {
            for (var i = 0; i < numberOfSections; i++)
            {
                _SectionTable.Add(new SectionHeader(reader));
            }
        }
    }
}