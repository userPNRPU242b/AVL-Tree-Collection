using System;
using Lab12;
using ProductLibrary;

class Program
{
    static void Main(string[] args)
    {
        MyCollection<Product> shop = new MyCollection<Product>();
        Random rnd = new Random();


        shop.Add(new Toy("Китайское лего", 1500, 12));
        shop.Add(new FoodProduct("Батон Нарезной", 45, 3));
        shop.Add(new DairyProduct("Греческий йогурт Савушкин", 75, 14, 2.5));
        shop.Add(new Toy("Медведь", 1200, 3));
        shop.Add(new DairyProduct("Ряженка", 90, 7, 3.2));

        Console.WriteLine($"В коллекцию добавлено {shop.Count} товаров.\n");

        Console.WriteLine("По алфавиту (и по цене, если имена совпадают):");
        foreach (Product item in shop)
        {
            item.Show();
        }

        Product toRemove = new FoodProduct("Батон Нарезной", 45, 3);

        Console.WriteLine($"\nУдаляем: {toRemove.Name}...");
        if (shop.Remove(toRemove))
        {
            Console.WriteLine("Товар удален. Список обновлен:");
            foreach (var item in shop) Console.WriteLine($"- {item.Name}");
        }
        else
        {
            Console.WriteLine("Товар не найден.");
        }

        MyCollection<Product> clonedShop = (MyCollection<Product>)shop.Clone();

        Console.WriteLine($"Количество в оригинале: {shop.Count}");
        Console.WriteLine($"Количество в клоне: {clonedShop.Count}");

        Console.WriteLine("\nОчищаем оригинал (Clear)...");
        shop.Clear();

        Console.WriteLine($"В оригинале после очистки: {shop.Count}");
        Console.WriteLine($"В клоне: {clonedShop.Count}");

        if (clonedShop.Count > 0 && shop.Count == 0)
        {
            Console.WriteLine("Глубокое копирование работает!");
        }

        Product[] productArray = new Product[clonedShop.Count];
        clonedShop.CopyTo(productArray, 0);
        Console.WriteLine($"Первый элемент в массиве: {productArray[0].Name}");

        Console.ReadKey();
    }
}