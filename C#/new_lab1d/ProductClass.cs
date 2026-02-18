public class ProductClass
{
    private string _name;
    private int _quantity;
    private readonly int _productId;
    private static int _nextId = 1;

    public ProductCategory Category { get; init; }

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Название товара не может быть пустым.");
            _name = value;
        }
    }

    public int Quantity
    {
        get => _quantity;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(Quantity), "Количество не может быть отрицательным.");
            _quantity = value;
        }
    }

    public ProductClass()
    {
        _productId = _nextId++;
        Name = "Неизвестный товар";
        Quantity = 0;
        Category = ProductCategory.Other;
    }

    public ProductClass(string name, int quantity, ProductCategory category)
    {
        _productId = _nextId++;
        Name = name;                
        Quantity = quantity;        
        Category = category;
    }

    public override string ToString() => $"[ID:{_productId}] {Name}, количество: {Quantity}, категория: {Category}";
}
