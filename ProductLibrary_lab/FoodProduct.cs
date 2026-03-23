using System;

namespace ProductLibrary
{
    public class FoodProduct : Product
    {
        private int shelfLife;
        public int ShelfLife
        {
            get => shelfLife;
            set
            {
                if (value < 0)
                {
                    shelfLife = 0;
                }
                else if (value > 365)
                {
                    shelfLife = 365;
                }
                else
                {
                    shelfLife = value;
                }
            }
        }
        public FoodProduct() : base()
        {
            ShelfLife = 7;
        }
        public FoodProduct(string name, decimal price, int shelfLife) : base(name, price)
        {
            ShelfLife = shelfLife;
        }
        public FoodProduct(FoodProduct other) : base(other)
        {
            ShelfLife = other.ShelfLife;
        }
        public override void Show()
        {
            Console.WriteLine($"Продукт: {Name}, Цена: {Price} руб., Срок годности: {ShelfLife} дней");
        }
        public override void Initialize()
        {
            base.Initialize();
            Console.Write("Введите срок годности (дней): ");
            int.TryParse(Console.ReadLine(), out int days);
            ShelfLife = days;
        }

        public override string ToString()
        {
            return base.ToString() + $", Expiration: {ShelfLife} days";
        }

        public override void RandomInit(Random rnd)
        {
            string[] names = { "Батон", "Булочка", "Рулет", "Пряники", "Ролтон" };
            Name = names[rnd.Next(names.Length)];
            Price = rnd.Next(100, 150);
            ShelfLife = rnd.Next(1, 30);
        }
        public override bool Equals(object obj)
        {
            if (obj is FoodProduct other)
            {
                return base.Equals(other) && ShelfLife == other.ShelfLife;
            }
            return false;
        }
    }
}