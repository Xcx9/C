namespace lab5
{
    static class Config
    {
        public const int ThrottleMaxParallel = 4;
        public const int TotalTasks = 500;
        public const int MinTaskDurationMs = 100;
        public const int MaxTaskDurationMs = 500;
        public const int BlockCount = 50;
        public const int BlockSize = 1024;
        public const int CrcLoopIterations = 100_000;
        public const int CompressionDelayMs = 50;

        public const int RetryMaxAttempts = 3;
        public static readonly TimeSpan[] RetryBackoff =
        {
            TimeSpan.FromSeconds(1),
            TimeSpan.FromSeconds(2),
            TimeSpan.FromSeconds(4)
        };

        public const int CacheParallelRequests = 20;
    }
}
