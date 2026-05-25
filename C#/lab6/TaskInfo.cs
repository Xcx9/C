// Models/TaskInfo.cs
using System;

namespace ThreadProfiler.Models
{
    public class TaskInfo
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ManagedThreadId { get; set; }
        public string Status { get; set; } = "Running";
        public double DurationMs => (EndTime - StartTime).TotalMilliseconds;

        public TaskInfo(int id, DateTime start, int threadId)
        {
            Id = id;
            StartTime = start;
            ManagedThreadId = threadId;
        }
    }
}
