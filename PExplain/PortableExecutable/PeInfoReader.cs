using System;
using System.IO;
using System.Text;

namespace PExplain.PortableExecutable
{
    public class PeInfoReader : IDisposable
    {
        public Stream BaseStream { get; }

        public PeInfoReader(Stream stream)
        {
            BaseStream = stream;
        }

        public void Dispose()
        {
            BaseStream?.Dispose();
        }

        public Info<byte> ReadByte()
        {
            return Read(1, (bytes, offset) => bytes[0]);
        }

        public Info<ushort> ReadWord()
        {
            return Read(2, BitConverter.ToUInt16);
        }

        public Info<uint> ReadDWord()
        {
            return Read(4, BitConverter.ToUInt32);
        }

        public Info<ulong> ReadQWord()
        {
            return Read(8, BitConverter.ToUInt64);
        }

        public Info<string> ReadString(int size, Encoding encoding)
        {
            return Read(size, (bytes, offset) => encoding.GetString(bytes));
        }

        public Info<TEnum> ReadWordAsEnum<TEnum>()
        {
            var rawInfo = ReadWord();
            var value = (TEnum)Enum.ToObject(typeof(TEnum), rawInfo.Value);
            return new Info<TEnum>(rawInfo.Offset, rawInfo.Bytes, value);
        }

        public Info<TEnum> ReadDWordAsEnum<TEnum>()
        {
            var rawInfo = ReadDWord();
            var value = (TEnum)Enum.ToObject(typeof(TEnum), rawInfo.Value);
            return new Info<TEnum>(rawInfo.Offset, rawInfo.Bytes, value);
        }

        public Info<TEnum> ReadQWordAsEnum<TEnum>()
        {
            var rawInfo = ReadQWord();
            var value = (TEnum)Enum.ToObject(typeof(TEnum), rawInfo.Value);
            return new Info<TEnum>(rawInfo.Offset, rawInfo.Bytes, value);
        }

        private Info<T> Read<T>(int size, Func<byte[], int, T> converter)
        {
            var offset = BaseStream.Position;

            var bytes = new byte[size];
            BaseStream.Read(bytes, 0, size);

            var value = converter(bytes, 0);

            return new Info<T>(offset, bytes, value);
        }
    }
}
