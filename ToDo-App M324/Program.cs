using System;
using System.Collections.Generic;
using System.IO;
using ToDo_App_M324.Application;

class Program
{
	private static string filePath = "todo_list.csv";
	private static List<string> toDos = new List<string>();
	private static ToDoManager toDoManager = new ToDoManager(filePath);

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
		toDos = toDoManager.LoadToDos();
	}

	static void SaveToDos()
	{
		toDoManager.SaveToDos(toDos);
	}

	static void AddToDo()
	{
		Console.Write("Neue ToDo: ");
		var toDo = Console.ReadLine() ?? "";
		if (toDoManager.AddToDo(toDos, toDo))
		{
			Console.WriteLine("ToDo hinzugefügt!");
		}
	}

	static void RemoveToDo()
	{
		ShowToDos();
		Console.Write("Nummer der zu löschenden ToDo: ");
		if (int.TryParse(Console.ReadLine(), out int index) && toDoManager.RemoveToDo(toDos, index))
		{
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
		
		toDoManager.DeleteAllToDos(toDos);
		Console.WriteLine("Alle ToDos wurden erfolgreich gelöscht.");
	}
}
