public struct ProductStruct
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

    public ProductStruct()
    {
        _productId = _nextId++;
        _name = "Неизвестный товар";
        _quantity = 0;
        Category = ProductCategory.Other;
    }

    public ProductStruct(string name, int quantity, ProductCategory category)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Название товара не может быть пустым.");
        if (quantity < 0)
            throw new ArgumentOutOfRangeException(nameof(quantity), "Количество не может быть отрицательным.");

        _productId = _nextId++;
        _name = name;
        _quantity = quantity;
        Category = category;
    }

    public override string ToString() => $"[ID:{_productId}] {Name}, количество: {Quantity}, категория: {Category}";
}
