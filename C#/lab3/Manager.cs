using System;

public class Manager : IDisposable
{
    private readonly string _name;
    private readonly ServiceStation _station;
    private bool _disposed;

    public Manager(ServiceStation station, int id)
    {
        _name = $"Менеджер-{id}";
        _station = station;
        _station.ServiceDone += HandleServiceDone;
        Console.WriteLine($"[{_name}] подписался на событие.");
    }

    private void HandleServiceDone(string carId)
    {
        Console.WriteLine($"[{_name}] получил уведомление: автомобиль {carId} готов. Объект жив!");
    }

    public void Unsubscribe()
    {
        if (!_disposed)
        {
            _station.ServiceDone -= HandleServiceDone;
            Console.WriteLine($"[{_name}] отписался от события.");
        }
    }

    public void Dispose()
    {
        Unsubscribe();
        _disposed = true;
        GC.SuppressFinalize(this);
    }

    ~Manager()
    {
        Console.WriteLine($"[{_name}] ФИНАЛИЗИРОВАН (собран сборщиком)");
    }
}
