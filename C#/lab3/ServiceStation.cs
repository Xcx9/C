using System;

public class ServiceStation
{
    public event Action<string>? ServiceDone;

    public void CompleteService(string carId)
    {
        Console.WriteLine($"[СТАНЦИЯ] Автомобиль {carId} готов.");
        ServiceDone?.Invoke(carId);
    }
}
