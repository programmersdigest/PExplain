using PExplain.PortableExecutable;

namespace PExplain.Output
{
    public class Row : IGroup
    {
        public string Field { get; }
        public string Offset { get; }
        public string Size { get; }
        public string RawData { get; }
        public string Value { get; }

        public Row(string field, long offset, int size, byte[] rawData, object value)
        {
            Field = field;
            Offset = offset.ToString();
            Size = size.ToString();
            RawData = rawData.ToHex();

            Value = value?.ToString()?.Escape() ?? "<NULL>";
        }

        public Row(string field, IInfo info) : this(field, info.Offset, info.Size, info.Bytes, info.Value)
        {
        }

        public override string ToString()
        {
            return $"{Field}|{Offset}|{Size}|{RawData}|{Value}";
        }
    }
}
