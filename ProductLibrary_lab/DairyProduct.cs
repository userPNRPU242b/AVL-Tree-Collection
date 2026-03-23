using System;

namespace ProductLibrary
{
    public class DairyProduct : FoodProduct
    {
        private double fatContent;
        public double FatContent
        {
            get => fatContent;
            set
            {
                if (value < 0)
                {
                    fatContent = 0;
                }
                else if (value > 50)
                {
                    fatContent = 50;
                }
                else
                {
                    fatContent = value;
                }
            }
        }
        public DairyProduct() : base()
        {
            FatContent = 2.5;
        }
        public DairyProduct(string name, decimal price, int shelfLife, double fatContent)
            : base(name, price, shelfLife)
        {
            FatContent = fatContent;
        }
        public DairyProduct(DairyProduct other) : base(other)
        {
            FatContent = other.FatContent;
        }
        public override void Show()
        {
            Console.WriteLine($"Молочный продукт: {Name}, Цена: {Price} руб., " +
                            $"Срок годности: {ShelfLife} дней, Жирность: {FatContent}%");
        }
        public override void Initialize()
        {
            base.Initialize();
            Console.Write("Введите жирность (%): ");
            double.TryParse(Console.ReadLine(), out double fat);
            FatContent = fat;
        }

        public override string ToString()
        {
            return base.ToString() + $", Fat: {FatContent}%";
        }


        public override void RandomInit(Random rnd)
        {
            string[] names = { "Кефир", "Творожок", "Йогурт", "Снежок", "Протеиновый коктель" };
            Name = names[rnd.Next(names.Length)];
            Price = rnd.Next(90, 93);
            FatContent = Math.Round(rnd.NextDouble() * 10, 1);
        }
        public override bool Equals(object obj)
        {
            if (obj is DairyProduct other)
            {
                return base.Equals(other) && FatContent == other.FatContent;
            }
            return false;
        }
    }
}