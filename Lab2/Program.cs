using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

class Program
{
    static List<GameCharacter> characters = new List<GameCharacter>();
    static int maxCount;

    static void Main()
    {
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;

        Console.Write("Введіть максимальну кількість персонажів (N > 0): ");
        while (!int.TryParse(Console.ReadLine(), out maxCount) || maxCount <= 0)
            Console.Write("Помилка! Введіть коректне N: ");

        while (true)
        {
            Console.WriteLine("\n1 – Додати об'єкт");
            Console.WriteLine("2 – Переглянути всі об'єкти");
            Console.WriteLine("3 – Знайти об'єкт");
            Console.WriteLine("4 – Продемонструвати поведінку");
            Console.WriteLine("5 – Видалити об'єкт");
            Console.WriteLine("0 – Вийти з програми");

            Console.Write("Ваш вибір: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddCharacter(); break;
                case "2": ShowAll(); break;
                case "3": FindCharacter(); break;
                case "4": DemonstrateBehavior(); break;
                case "5": DeleteCharacter(); break;
                case "0":
                    Console.WriteLine("Роботу програми завершено.");
                    return;
                default:
                    Console.WriteLine("Невірний пункт меню.");
                    break;
            }
        }
    }

    static void AddCharacter()
    {
        if (characters.Count >= maxCount)
        {
            Console.WriteLine("Досягнуто максимальної кількості об'єктів.");
            return;
        }

        GameCharacter c = new GameCharacter();

        try
        {
            Console.Write("Ім'я (3–12 символів): ");
            c.Name = Console.ReadLine();

            Console.Write("Здоров'я (0–100): ");
            c.Health = int.Parse(Console.ReadLine());

            Console.WriteLine("Клас персонажа:");
            foreach (var e in Enum.GetValues(typeof(CharacterClass)))
                Console.WriteLine($"{(int)e} – {e}");

            c.CharacterClass = (CharacterClass)int.Parse(Console.ReadLine());

            characters.Add(c);
            Console.WriteLine("Персонажа додано успішно.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static void ShowAll()
    {
        if (characters.Count == 0)
        {
            Console.WriteLine("Список об'єктів порожній.");
            return;
        }

        Console.WriteLine("Ім'я         | Клас     | Рівень | HP  | Стан");
        Console.WriteLine("---------------------------------------------");
        foreach (var c in characters)
            Console.WriteLine(c.GetInfo());
    }

    static void FindCharacter()
    {
        Console.Write("Введіть ім'я для пошуку: ");
        string name = Console.ReadLine();

        var found = characters.FindAll(c => c.Name == name);
        if (found.Count == 0)
        {
            Console.WriteLine("Персонажа не знайдено.");
            return;
        }

        foreach (var c in found)
            Console.WriteLine(c.GetInfo());
    }

    static void DemonstrateBehavior()
    {
        if (characters.Count == 0)
        {
            Console.WriteLine("Немає об'єктів.");
            return;
        }

        try
        {
            GameCharacter c = characters[0];
            c.Attack();
            c.TakeDamage(30);
            Console.WriteLine("Після атаки:");
            Console.WriteLine(c.GetInfo());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static void DeleteCharacter()
    {
        Console.Write("Введіть ім'я персонажа для видалення: ");
        string name = Console.ReadLine();

        int removed = characters.RemoveAll(c => c.Name == name);
        Console.WriteLine(removed == 0
            ? "Об'єкти не знайдено."
            : $"Видалено об'єктів: {removed}");
    }
}

