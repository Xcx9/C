using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace lab5
{
    class Program
    {
        static async Task Main()
        {
            Console.WriteLine("===== 1. THROTTLING DEMO =====");
            await RunThrottlingDemo();

            Console.WriteLine("\n===== 2. RETRY DEMO =====");
            await RunRetryDemo();

            Console.WriteLine("\n===== 3. ASYNC CACHE DEMO =====");
            await RunCacheDemo();

            Console.WriteLine("\nВсе сценарии завершены.");
        }

        static async Task RunThrottlingDemo()
        {
            var semaphore = new SemaphoreSlim(Config.ThrottleMaxParallel);
            var random = new Random();
            var stopwatch = Stopwatch.StartNew();

            var tasks = new List<Task>();
            for (int i = 0; i < Config.TotalTasks; i++)
            {
                int taskId = i;
                tasks.Add(Task.Run(async () =>
                {
                    await semaphore.WaitAsync();
                    try
                    {
                        Console.WriteLine($"Задача {taskId} запущена (потоков в работе: {Config.ThrottleMaxParallel - semaphore.CurrentCount})");
                        int delay = random.Next(Config.MinTaskDurationMs, Config.MaxTaskDurationMs + 1);
                        await Task.Delay(delay);
                        Console.WriteLine($"Задача {taskId} завершена");
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                }));
            }

            await Task.WhenAll(tasks);
            stopwatch.Stop();
            Console.WriteLine($"\nОбщее время с троттлингом (N={Config.ThrottleMaxParallel}): {stopwatch.ElapsedMilliseconds} мс");

            stopwatch.Restart();
            var unrestrictedTasks = new List<Task>();
            for (int i = 0; i < Config.TotalTasks; i++)
            {
                int taskId = i;
                unrestrictedTasks.Add(Task.Run(async () =>
                {
                    int delay = random.Next(Config.MinTaskDurationMs, Config.MaxTaskDurationMs + 1);
                    await Task.Delay(delay);
                }));
            }
            await Task.WhenAll(unrestrictedTasks);
            stopwatch.Stop();
            Console.WriteLine($"Общее время без троттлинга: {stopwatch.ElapsedMilliseconds} мс");
            Console.WriteLine("(С троттлингом задачи выполняются порциями, но общее время сопоставимо, " +
                              "при этом нагрузка на пул потоков контролируется.)");
        }

        static async Task RunRetryDemo()
        {
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
            int attempt = 0;
            Func<Task<string>> flakyOperation = async () =>
            {
                int current = Interlocked.Increment(ref attempt);
                Console.WriteLine($"  Попытка {current}");
                await Task.Delay(200);
                if (current % 2 == 0)
                    return $"Success on attempt {current}";
                throw new InvalidOperationException($"Simulated failure on attempt {current}");
            };

            try
            {
                string result = await RetryPolicy.ExecuteWithRetry(flakyOperation, cts.Token);
                Console.WriteLine($"Результат: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Retry исчерпан: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Демонстрация Retry завершена.");
            }
        }

        static async Task RunCacheDemo()
        {
            var compressionService = new CompressionService();
            var block = new ArchiveBlock(1, new byte[Config.BlockSize]);
            var stopwatch = Stopwatch.StartNew();

            var tasks = new List<Task<uint>>();
            for (int i = 0; i < Config.CacheParallelRequests; i++)
            {
                int requestId = i;
                tasks.Add(Task.Run(async () =>
                {
                    Console.WriteLine($"Запрос {requestId} для блока {block.Id}");
                    uint crc = await compressionService.CompressBlockAsync(block);
                    Console.WriteLine($"Запрос {requestId} получил результат: {crc}");
                    return crc;
                }));
            }

            await Task.WhenAll(tasks);
            stopwatch.Stop();

            Console.WriteLine($"\nВремя обработки 20 запросов с кэшем: {stopwatch.ElapsedMilliseconds} мс");
            Console.WriteLine("Обратите внимание: 'Расчёт CRC' (внутренняя функция) выполнился только один раз.");
        }
    }
}
