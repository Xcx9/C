// Services/ITaskSimulator.cs
using System;
using System.Reactive.Subjects;

namespace ThreadProfiler.Services
{
    public interface ITaskSimulator
    {
        IObservable<TaskEvent> TaskEvents { get; }
        void Start(int taskCount);
        void Stop();
    }

    public enum TaskEventType { Started, Completed }

    public class TaskEvent
    {
        public TaskEventType Type { get; }
        public TaskInfo Task { get; }

        public TaskEvent(TaskEventType type, TaskInfo task)
        {
            Type = type;
            Task = task;
        }
    }
}
