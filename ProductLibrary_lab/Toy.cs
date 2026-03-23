using System;

namespace ProductLibrary
{
    public class Toy : Product
    {
        private int ageLimit;
        public int AgeLimit
        {
            get => ageLimit;
            set => ageLimit = value < 0 ? 0 : value > 18 ? 18 : value;
        }
        public Toy() : base()
        {
            AgeLimit = 3;
        }
        public Toy(string name, decimal price, int ageLimit) : base(name, price)
        {
            AgeLimit = ageLimit;
        }
        public Toy(Toy other) : base(other)
        {
            AgeLimit = other.AgeLimit;
        }
        public override void Show()
        {
            Console.WriteLine($"Игрушка: {Name}, Цена: {Price} руб., Возраст: {AgeLimit}+");
        }
        public override void Initialize()
        {
            base.Initialize();
            Console.Write("Введите возрастное ограничение: ");
            int.TryParse(Console.ReadLine(), out int age);
            AgeLimit = age;
        }
        public override void RandomInit(Random rnd)
        {
            string[] names = { "Мяч", "Кукла", "Машина", "Конструктор", "Пазл" };
            Name = names[rnd.Next(names.Length)];
            Price = rnd.Next(100, 1000);
            AgeLimit = rnd.Next(1, 18);
        }
        public override bool Equals(object obj)
        {
            if (obj is Toy other)
                return base.Equals(other) && AgeLimit == other.AgeLimit;
            return false;
        }
    }
}