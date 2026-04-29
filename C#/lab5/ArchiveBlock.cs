using System;

namespace lab5
{
    public class ArchiveBlock
    {
        public int Id { get; }
        public byte[] Data { get; }

        public ArchiveBlock(int id, byte[] data)
        {
            Id = id;
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }
    }
}
