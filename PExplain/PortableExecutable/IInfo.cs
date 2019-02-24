namespace PExplain.PortableExecutable
{
    public interface IInfo
    {
        long Offset { get; }
        byte[] Bytes { get; }
        int Size { get; }
        object Value { get; }
    }
}
