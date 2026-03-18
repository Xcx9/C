public class Part
{
    public string Name { get; set; }
    public decimal BasePrice { get; set; }
    public bool IsOriginal { get; set; }

    public Part(string name, decimal basePrice, bool isOriginal)
    {
        Name = name;
        BasePrice = basePrice;
        IsOriginal = isOriginal;
    }
}
