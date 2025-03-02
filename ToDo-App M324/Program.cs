using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static string filePath = "todo_list.csv";
    static List<string> toDos = new List<string>();

    static void Main()
    {
        LoadToDos();

        while (true)
        {
            Console.WriteLine("\nToDo-Liste: ");
            Console.WriteLine("1. ToDo hinzufügen");
            Console.WriteLine("2. ToDo entfernen");
            Console.WriteLine("3. ToDos anzeigen");
            Console.WriteLine("4. ToDos speichern");
            Console.WriteLine("5. Alle ToDos entfernen");
            Console.WriteLine("6. Beenden");
            Console.Write("Auswahl: ");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    AddToDo();
                    break;
                case "2":
                    RemoveToDo();
                    break;
                case "3":
                    ShowToDos();
                    break;
                case "4":
                    SaveToDos();
                    Console.WriteLine("ToDos gespeichert!");
                    break;
                case "5":
                    DeleteAllToDos();
                    break;
                case "6":
                    SaveToDos();
                    return;
                default:
                    Console.WriteLine("Ungültige Auswahl!");
                    break;
            }
        }
    }

    static void LoadToDos()
    {
        if (File.Exists(filePath))
        {
            toDos = new List<string>(File.ReadAllLines(filePath));
        }
    }

    static void SaveToDos()
    {
        File.WriteAllLines(filePath, toDos);
    }

    static void AddToDo()
    {
        Console.Write("Neue ToDo: ");
        var ToDo = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(ToDo))
        {
            toDos.Add(ToDo);
            Console.WriteLine("ToDo hinzugefügt!");
        }
    }

    static void RemoveToDo()
    {
        ShowToDos();
        Console.Write("Nummer der zu löschenden ToDo: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= toDos.Count)
        {
            toDos.RemoveAt(index - 1);
            Console.WriteLine("ToDo entfernt!");
        }
        else
        {
            Console.WriteLine("Ungültige Eingabe!");
        }
    }

    static void ShowToDos()
    {
        Console.WriteLine("\nAktuelle ToDos:");
        if (toDos.Count == 0)
        {
            Console.WriteLine("Keine ToDos vorhanden.");
        }
        else
        {
            for (int i = 0; i < toDos.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {toDos[i]}");
            }
        }
    }

    static void DeleteAllToDos()
    {
        Console.Write("Sind Sie sich sicher, dass Sie ALLE ToDos löschen wollen? y/n:");
        var input = Console.ReadKey();
        Console.WriteLine();

        if (input.Key != ConsoleKey.Y)
        {
            return;
        }
        
        toDos.Clear();
        Console.WriteLine("Alle ToDos wurden erfolgreich gelöscht.");
    }
}
