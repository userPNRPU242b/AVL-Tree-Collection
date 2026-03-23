using System;

namespace ProductLibrary
{
    public class Product : IComparable<Product>
    {
        private string name;
        private decimal price;
        public string Name
        {
            get => name;
            set => name = string.IsNullOrWhiteSpace(value) ? "Без названия" : value;
        }
        public decimal Price
        {
            get => price;
            set => price = value < 0 ? 0 : value;
        }
        public Product()
        {
            Name = "Товар";
            Price = 0;
        }
        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
        public Product(Product other)
        {
            Name = other.Name;
            Price = other.Price;
        }
        public virtual void Show()
        {
            Console.WriteLine($"Товар: {Name}, Цена: {Price} руб.");
        }
        public virtual void Initialize()
        {
            Console.Write("Введите название товара: ");
            Name = Console.ReadLine();
            Console.Write("Введите цену: ");
            decimal.TryParse(Console.ReadLine(), out decimal p);
            Price = p;
        }
        public virtual void RandomInit(Random rnd)
        {
            string[] names = { "Мяч", "Кукла", "Машина", "Конструктор", "Пазл" };
            Name = names[rnd.Next(names.Length)];
            Price = rnd.Next(100, 1000);
        }
        public override bool Equals(object obj)
        {
            if (obj is Product other)
                return Name == other.Name && Price == other.Price;
            return false;
        }

        public int CompareTo(Product other)
        {
            if (other == null) return 1;

            int nameComparison = string.Compare(this.Name, other.Name, StringComparison.OrdinalIgnoreCase);

            if (nameComparison != 0)
            {
                return nameComparison;
            }

            return this.Price.CompareTo(other.Price);
        }

        public override string ToString()
        {
            return $"[Product] Name: {Name}, Price: {Price:C}";
        }

    }
}