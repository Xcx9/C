// Services/TaskSimulator.cs
using System;
using System.Collections.Concurrent;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using ThreadProfiler.Models;

namespace ThreadProfiler.Services
{
    public class TaskSimulator : ITaskSimulator
    {
        private readonly Subject<TaskEvent> _events = new();
        private readonly ConcurrentDictionary<int, Task> _runningTasks = new();
        private readonly Random _random = new();
        private CancellationTokenSource _cts;
        private int _nextId = 1;

        public IObservable<TaskEvent> TaskEvents => _events.AsObservable();

        public void Start(int taskCount)
        {
            Stop();
            _cts = new CancellationTokenSource();
            Task.Run(() => GenerateTasks(taskCount, _cts.Token));
        }

        public void Stop()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        private async Task GenerateTasks(int count, CancellationToken ct)
        {
            for (int i = 0; i < count && !ct.IsCancellationRequested; i++)
            {
                var taskId = Interlocked.Increment(ref _nextId);
                _ = ExecuteTaskAsync(taskId, ct);
                // задержка между запусками для имитации потока задач
                await Task.Delay(_random.Next(20, 150), ct);
            }
        }

        private async Task ExecuteTaskAsync(int id, CancellationToken ct)
        {
            var threadId = Environment.CurrentManagedThreadId;
            var start = DateTime.Now;
            var taskInfo = new TaskInfo(id, start, threadId);
            
            _events.OnNext(new TaskEvent(TaskEventType.Started, taskInfo));

            try
            {
                // симуляция работы задачи (от 100 до 2000 мс)
                var duration = _random.Next(100, 2000);
                await Task.Delay(duration, ct);
                taskInfo.EndTime = DateTime.Now;
                taskInfo.Status = "Completed";
            }
            catch (OperationCanceledException)
            {
                taskInfo.EndTime = DateTime.Now;
                taskInfo.Status = "Cancelled";
            }
            finally
            {
                _events.OnNext(new TaskEvent(TaskEventType.Completed, taskInfo));
            }
        }
    }
}
