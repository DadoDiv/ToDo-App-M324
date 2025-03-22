using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ToDo_App_M324.Application
{
    public class ToDoManager
    {
        private string _csvFilePath;

        public ToDoManager(string csvFilePath)
        {
            _csvFilePath = csvFilePath;
        }

        public List<string> GetToDos()
        {
            if (File.Exists(_csvFilePath))
            {
                return new List<string>(File.ReadAllLines(_csvFilePath));
            }

            return new List<string>();
        }

        public string GetToDo(int number)
        {
            if (File.Exists(_csvFilePath))
            {
                var toDos = File.ReadAllLines(_csvFilePath);

                if (number > 0 && number <= toDos.Length)
                {
                    return toDos[number - 1];
                }
            }

            return string.Empty;
        }

        public void SaveToDos(List<string> toDos)
        {
            File.WriteAllLines(_csvFilePath, toDos);
        }

        public bool AddToDo(List<string> toDos, string toDo)
        {            
            if (!string.IsNullOrWhiteSpace(toDo))
            {
                toDos.Add(toDo);
                return true;
            }
            return false;
        }

        public bool DeleteToDo(List<string> toDos, int number)
        {
            if (number > 0 && number <= toDos.Count)
            {
                toDos.RemoveAt(number - 1);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DeleteAllToDos(List<string> toDos)
        {
            toDos.Clear();
        }
    }
}
