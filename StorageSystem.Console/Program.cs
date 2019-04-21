using StorageSystem.Models;
using StorageSystem.Services.Abstract;
using System.Collections.Generic;

namespace StorageSystem.Console
{
    public class Program
    {
        private static Service _service;

        public static void Main(string[] args)
        {
            string userChoice;
            int choice;

            System.Console.WriteLine("Вывод товаров:");

            _service = new Service();

            var toys = _service.Select<Toy>();

            _service.Dispose();

            if (toys.Count == 0)
            {
                System.Console.WriteLine("Товаров нет!");

                System.Console.WriteLine("Хотите добавить новый товар? y/n");
                userChoice = System.Console.ReadLine();

                switch (userChoice)
                {
                    case "y":
                        Insert();
                        break;
                    case "n":
                        System.Console.WriteLine("Оке");
                        break;
                    default:
                        System.Console.WriteLine("У Вас был выбор...");
                        break;
                }
                System.Console.ReadLine();
                return;
            }

            int i = 0;

            System.Console.WriteLine(string.Format("{0}{1,-20} {2,-20} {3,-20} {4,-20}","№)", "Название", "Цвет", "Материал", "Цена"));
            foreach (var toy in toys)
            {
                System.Console.WriteLine(string.Format("{0}){1,-20} {2,-20} {3,-20} {4,-20}", ++i, toy.Name, toy.Color, toy.Material, toy.Price));
            }

            System.Console.WriteLine("Выберите действие:");
            System.Console.WriteLine("1) Добавить новый товар");
            System.Console.WriteLine("2) Изменить информацию о товаре");
            System.Console.WriteLine("3) Удалить товар");

            userChoice = System.Console.ReadLine();
            bool isParsed = int.TryParse(userChoice, out choice);

            if (!isParsed)
            {
                System.Console.WriteLine("Вместо номера действия Вы ввели хз что... Вопрос: ЗАЧЕМ?");
                System.Console.ReadLine();
                return;
            }

            switch (choice)
            {
                case 1:
                    Insert();
                    break;
                case 2:
                    Update(toys);
                    break;
                case 3:
                    Delete(toys);
                    break;
                default:
                    System.Console.WriteLine("Такой опции не существует!");
                    break;
            }

            System.Console.ReadLine();
        }

        public static void Insert()
        {
            string toyInfo;

            var toy = new Toy();

            System.Console.WriteLine("Введите название игрушки:");
            toyInfo = System.Console.ReadLine();
            toy.Name = toyInfo;

            System.Console.WriteLine("Введите цвет игрушки:");
            toyInfo = System.Console.ReadLine();
            toy.Color = toyInfo;

            System.Console.WriteLine("Введите материал игрушки:");
            toyInfo = System.Console.ReadLine();
            toy.Material = toyInfo;

            System.Console.WriteLine("Введите цену игрушки:");
            toyInfo = System.Console.ReadLine();

            int price = 0;
            bool isParsed = int.TryParse(toyInfo, out price);

            if (!isParsed)
            {
                System.Console.WriteLine("Вместо цены Вы ввели хз что... Потому цена обнулится");
            }
            toy.Price = price;

            _service = new Service();
            _service.Insert(toy);
            _service.Dispose();

            System.Console.WriteLine("Информация о товаре успешно добавлена!");
        }

        public static void Update(List<Toy> toys)
        {
            string userChoice;
            int choice;

            var list = new List<Toy>();
            list.AddRange(toys);

            System.Console.WriteLine("Введите номер товара:");
            userChoice = System.Console.ReadLine();
            bool isParsed = int.TryParse(userChoice, out choice);

            if (!isParsed)
            {
                System.Console.WriteLine("Зачем Вы вводите НЕ цифры?");
                System.Console.ReadLine();
                return;
            }

            if (choice > list.Count || choice <= 0)
            {
                System.Console.WriteLine("Товара под таким номером не существует!");
                System.Console.ReadLine();
                return;
            }

            var toy = toys[choice - 1];

            System.Console.WriteLine("Что хотите изменить?");
            System.Console.WriteLine("1) Название игрушки");
            System.Console.WriteLine("2) Цвет игрушки");
            System.Console.WriteLine("3) Материал игрушки");
            System.Console.WriteLine("4) Цену на игрушку");

            userChoice = System.Console.ReadLine();
            isParsed = int.TryParse(userChoice, out choice);

            if (!isParsed)
            {
                System.Console.WriteLine("Вместо того, чтобы выбрать характеристику, которую надо изменить, Вы ввели хз что... Вопрос: ЗАЧЕМ?");
                System.Console.ReadLine();
                return;
            }

            string changedData;

            switch (choice)
            {
                case 1:
                    System.Console.WriteLine("Введите новое название:");
                    changedData = System.Console.ReadLine();
                    toy.Name = changedData;
                    break;
                case 2:
                    System.Console.WriteLine("Введите новый цвет:");
                    changedData = System.Console.ReadLine();
                    toy.Color = changedData;
                    break;
                case 3:
                    System.Console.WriteLine("Введите новый материал:");
                    changedData = System.Console.ReadLine();
                    toy.Material = changedData;
                    break;
                case 4:
                    {
                        System.Console.WriteLine("Введите новую цену:");
                        changedData = System.Console.ReadLine();

                        int price = 0;
                        isParsed = int.TryParse(changedData, out price);

                        if (!isParsed)
                        {
                            System.Console.WriteLine("Вместо цены Вы ввели хз что... Потому цена обнулится");
                        }
                        toy.Price = price;
                    }
                    break;
                default:
                    System.Console.WriteLine("Такой опции не существует!");
                    return;
            }

            _service = new Service();
            _service.Update(toy);
            _service.Dispose();

            System.Console.WriteLine("Информация о товаре успешно обновлена!");
        }

        public static void Delete(List<Toy> toys)
        {
            string userChoice;
            int choice;

            var list = new List<Toy>();
            list.AddRange(toys);

            System.Console.WriteLine("Введите номер товара:");
            userChoice = System.Console.ReadLine();
            bool isParsed = int.TryParse(userChoice, out choice);

            if (!isParsed)
            {
                System.Console.WriteLine("Зачем Вы вводите НЕ цифры?");
                System.Console.ReadLine();
                return;
            }

            if (choice > list.Count || choice <= 0)
            {
                System.Console.WriteLine("Товара под таким номером не существует!");
                System.Console.ReadLine();
                return;
            }

            var toy = toys[choice - 1];

            _service = new Service();
            _service.Delete(toy);
            _service.Dispose();

            System.Console.WriteLine("Информация о товаре успешно удалена!");
        }
    }
}
