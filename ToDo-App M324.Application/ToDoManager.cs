namespace ToDo_App_M324.Application
{
    public class ToDoManager
    {
        private string _csvFilePath;

        public ToDoManager(string csvFilePath)
        {
            _csvFilePath = csvFilePath;
        }

        public List<string> LoadToDos()
        {
            if (File.Exists(_csvFilePath))
            {
                return new List<string>(File.ReadAllLines(_csvFilePath));
            }

            return new List<string>();
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

        public bool RemoveToDo(List<string> toDos, int index)
        {
            if (index > 0 && index <= toDos.Count)
            {
                toDos.RemoveAt(index - 1);
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
