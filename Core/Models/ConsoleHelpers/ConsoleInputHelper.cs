using System.Globalization;
using Core.Enums;
using Core.Models.Managers;

namespace Core.Models.ConsoleHelpers;

public static class ConsoleInputHelper
{
    public static void WaitForAnyKeyToReturnToMenu()
    {
        Console.WriteLine("Tryck på valfri knapp för att gå tillbaka till menyn");
        Console.ReadKey(true);
    }

    public static string GetString(string label)
    {
        while (true)
        {
            Console.WriteLine(label + ':');
            var str = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(str))
                return str;
            
            Console.WriteLine("Vänligen försök igen");
        }
    }

    public static int GetInt(string label)
    {
        while (true)
        {
            Console.WriteLine(label + ':');
            var str = Console.ReadLine();

            if (int.TryParse(str, out var res))
                return res;
            
            Console.WriteLine("Vänligen försök igen");
        }
    }

    public static T GetEnum<T>(string label) where T : struct, Enum
    {
        while (true)
        {
            Console.WriteLine(label + ':');
            int i = 1;
            var options = Enum.GetValues<T>();
            foreach (T option in options)
            {
                Console.WriteLine(i + " " + option);
                i++;
            }

            var str = Console.ReadLine();

            if (int.TryParse(str, out int index))
            {
                index--; // User input starts from 1
                if (0 <= index && index < options.Length)
                    return options[index];
            }
            
            Console.WriteLine("Vänligen försök igen");
        }
    }

    public static DateOnly GetDate(string label)
    {
        while (true)
        {
            Console.WriteLine(label + " (ÅÅÅÅ-MM-DD):");
            var str = Console.ReadLine();

            if (DateOnly.TryParseExact(str, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var res))
                return res;
            
            Console.WriteLine("Vänligen försök igen");

        }
    }

    public static Address GetAddress()
    {
        return new Address()
        {
            StreetAddress = GetString("Skriv gatuadress"),
            PostalCode = GetString("Skriv postkod"),
            Town = GetString("Skriv stad"),
            Country = GetString("Skriv land")
        };
    }

    public static List<T> GetMultiSelect<T>(string label, List<T> options, List<string> optionLabels)
    {
        if (options.Count == 0)
        {
            Console.WriteLine("Inga tillgängliga alternativ att lägga till");
            return [];
        }

        List<int> choosenIndexes = [];

        while (true)
        {
            Console.WriteLine($"{label}:");

            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"[{(choosenIndexes.Contains(i) ? 'X' : ' ')}] {i + 1} {optionLabels[i]}");
            }
            Console.WriteLine("Markera eller avmarkera med siffertangent");
            Console.WriteLine("Gå vidare till nästa steg med 'N'");

            char key = Console.ReadKey(true).KeyChar;
            if (key == 'n')
            {
                break;
            }
            else if (int.TryParse(key.ToString(), out int res) && 1 <= res && res < options.Count + 1)
            {
                res--;
                if (choosenIndexes.Contains(res))
                    choosenIndexes.Remove(res);
                else
                    choosenIndexes.Add(res);
            }
            else
            {
                Console.WriteLine($"'{key}' är inte ett korrekt val. Vänligen försök igen.");
            }
        }

        var choosenOptions = new List<T>();
        for (int i = 0; i < options.Count; i++)
        {
            if (choosenIndexes.Contains(i))
                choosenOptions.Add(options[i]);
        }
        return choosenOptions;
    }
}
