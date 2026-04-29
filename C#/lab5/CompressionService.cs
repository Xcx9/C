using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace lab5
{
    public class CompressionService
    {
        private readonly ConcurrentDictionary<int, Task<uint>> _cache = new();
        private readonly object _cacheLock = new();

        public Task<uint> CompressBlockAsync(ArchiveBlock block)
        {
            if (_cache.TryGetValue(block.Id, out var existingTask))
                return existingTask;

            lock (_cacheLock)
            {
                if (_cache.TryGetValue(block.Id, out existingTask))
                    return existingTask;

                var newTask = Task.Run(() => ComputeCrcInternal(block));
                _cache[block.Id] = newTask;
                return newTask;
            }
        }

        private uint ComputeCrcInternal(ArchiveBlock block)
        {
            uint crc = 0;
            for (int i = 0; i < Config.CrcLoopIterations; i++)
            {
                foreach (var b in block.Data)
                    crc ^= (uint)(b + i);
            }

            Task.Delay(Config.CompressionDelayMs).GetAwaiter().GetResult();
            return crc;
        }
    }
}
