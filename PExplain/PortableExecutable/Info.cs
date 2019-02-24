using System;

namespace PExplain.PortableExecutable
{
    public class Info<T> : IInfo
    {
        public static Info<T> ConvertFrom<U>(Info<U> info)
        {
            var value = (T)Convert.ChangeType(info.Value, typeof(T));
            return new Info<T>(info.Offset, info.Bytes, value);
        }

        public long Offset { get; }
        public byte[] Bytes { get; }
        public int Size => Bytes.Length;
        public T Value { get; }
        object IInfo.Value => Value;

        public Info(long offset, byte[] bytes, T value)
        {
            Offset = offset;
            Bytes = bytes;
            Value = value;
        }
    }
}
