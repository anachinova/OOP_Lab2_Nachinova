using System;

public class GameCharacter
{
    private string name;
    private int level;
    private int health;
    private CharacterClass characterClass;

    public int MaxHealth { get; private set; } = 100;

    public string Name
    {
        get { return name; }
        set
        {
            if (string.IsNullOrWhiteSpace(value) ||
                value.Length < 3 || value.Length > 12)
                throw new Exception("Помилка: ім'я має містити 3–12 символів.");
            name = value;
        }
    }
    public int Level
    {
        get { return level; }
        private set   
        {
            if (value < 1 || value > 100)
                throw new Exception("Помилка: рівень має бути від 1 до 100.");
            level = value;
        }
    }
    public int Health
    {
        get { return health; }
        set
        {
            if (value < 0 || value > MaxHealth)
                throw new Exception("Помилка: здоров'я має бути в межах 0–100.");
            health = value;
        }
    }
    public CharacterClass CharacterClass
    {
        get { return characterClass; }
        set
        {
            if (!Enum.IsDefined(typeof(CharacterClass), value))
                throw new Exception("Помилка: некоректний клас персонажа.");
            characterClass = value;
        }
    }
    public bool IsAlive
    {
        get { return health > 0; }
    }
    private void CheckAlive()
    {
        if (!IsAlive)
            throw new Exception("Персонаж мертвий і не може виконати дію.");
    }
    public void Attack()
    {
        CheckAlive();
        Console.WriteLine($"{Name} атакує як {CharacterClass}!");
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
            throw new Exception("Помилка: шкода не може бути від'ємною.");

        Health -= damage;
        if (Health < 0)
            Health = 0;
    }

    public void Heal(int amount)
    {
        if (amount < 0)
            throw new Exception("Помилка: лікування не може бути від'ємним.");

        Health += amount;
        if (Health > MaxHealth)
            Health = MaxHealth;
    }

    public void LevelUp()
    {
        Level++;
    }

    public string GetInfo()
    {
        return $"{Name,-12} | {CharacterClass,-8} | {Level,3} | {Health,3} | {(IsAlive ? "Alive" : "Dead")}";
    }
    public GameCharacter()
    {
        Level = 1;
        Health = MaxHealth;
    }
}

