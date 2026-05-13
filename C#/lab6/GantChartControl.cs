// Controls/GanttChartControl.cs
using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Threading;
using ReactiveUI;
using ThreadProfiler.Models;

namespace ThreadProfiler.Controls
{
    public class GanttChartControl : Control
    {
        private IEnumerable<TaskInfo> _tasks;
        private double _zoom = 1.0;
        private DateTime _minTime, _maxTime;

        public static readonly DirectProperty<GanttChartControl, IEnumerable<TaskInfo>> TasksProperty =
            AvaloniaProperty.RegisterDirect<GanttChartControl, IEnumerable<TaskInfo>>(
                nameof(Tasks), o => o.Tasks, (o, v) => o.Tasks = v);

        public IEnumerable<TaskInfo> Tasks
        {
            get => _tasks;
            set
            {
                if (SetAndRaise(TasksProperty, ref _tasks, value))
                {
                    UpdateTimeRange();
                    InvalidateVisual();
                }
            }
        }

        public double Zoom
        {
            get => _zoom;
            set
            {
                _zoom = Math.Max(0.5, Math.Min(5.0, value));
                InvalidateVisual();
            }
        }

        public GanttChartControl()
        {
            ClipToBounds = true;
            // поддержка колесика мыши для зума
            this.AddHandler(ScrollViewer.ScrollChangedEvent, (s, e) => { });
        }

        protected override void OnPointerWheelChanged(Avalonia.Input.PointerWheelEventArgs e)
        {
            base.OnPointerWheelChanged(e);
            if (e.Delta.Y > 0) Zoom *= 1.1;
            else if (e.Delta.Y < 0) Zoom /= 1.1;
        }

        private void UpdateTimeRange()
        {
            if (_tasks == null || !_tasks.Any())
            {
                _minTime = _maxTime = DateTime.Now;
                return;
            }
            _minTime = _tasks.Min(t => t.StartTime);
            _maxTime = _tasks.Max(t => t.EndTime == default ? DateTime.Now : t.EndTime);
            if (_minTime == _maxTime) _maxTime = _minTime.AddSeconds(1);
        }

        public override void Render(DrawingContext context)
        {
            base.Render(context);
            if (_tasks == null || !_tasks.Any()) return;

            var width = Bounds.Width;
            var height = Bounds.Height;
            var totalDuration = (_maxTime - _minTime).TotalMilliseconds * Zoom;
            if (totalDuration <= 0) return;

            // Группировка задач по потокам
            var groups = _tasks.GroupBy(t => t.ManagedThreadId).ToList();
            double rowHeight = Math.Max(20, height / groups.Count);

            // Рисуем сетку и метки времени
            var y = 0.0;
            foreach (var group in groups)
            {
                // Фон строки
                context.FillRectangle(Brushes.LightGray, new Rect(0, y, width, rowHeight));
                context.DrawLine(new Pen(Brushes.Gray), new Point(0, y), new Point(width, y));
                // Имя потока
                var text = $"Thread {group.Key}";
                var formatted = new FormattedText(text, Typeface.Default, 12, TextAlignment.Left, TextWrapping.NoWrap, new Size(width, rowHeight));
                context.DrawText(Brushes.Black, new Point(5, y + 2), formatted);

                // Рисуем задачи в этой строке
                foreach (var task in group)
                {
                    var startOffset = (task.StartTime - _minTime).TotalMilliseconds * Zoom;
                    var duration = task.DurationMs * Zoom;
                    var x = startOffset;
                    var rect = new Rect(x, y + 2, duration, rowHeight - 4);
                    var brush = task.Status == "Completed" ? Brushes.Green : Brushes.Orange;
                    context.FillRectangle(brush, rect);
                    context.DrawRectangle(new Pen(Brushes.Black), rect);
                    
                    // ID задачи внутри прямоугольника
                    var idText = task.Id.ToString();
                    var idFormatted = new FormattedText(idText, Typeface.Default, 10, TextAlignment.Center, TextWrapping.NoWrap, new Size(duration, rowHeight));
                    context.DrawText(Brushes.White, new Point(x + duration/2 - idFormatted.Width/2, y + rowHeight/2 - idFormatted.Height/2), idFormatted);
                }
                y += rowHeight;
            }

            // Вертикальные линии времени
            for (double t = 0; t <= totalDuration; t += 100 * Zoom)
            {
                var x = t;
                context.DrawLine(new Pen(Brushes.Gray, 0.5), new Point(x, 0), new Point(x, height));
                var timeLabel = _minTime.AddMilliseconds(t / Zoom).ToString("HH:mm:ss.fff");
                var label = new FormattedText(timeLabel, Typeface.Default, 10, TextAlignment.Left, TextWrapping.NoWrap, new Size(100, 20));
                context.DrawText(Brushes.Black, new Point(x + 2, 2), label);
            }
        }
    }
}
