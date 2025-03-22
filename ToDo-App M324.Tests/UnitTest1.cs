using ToDo_App_M324.Application;

namespace ToDo_App_M324.Tests
{
    [TestFixture]
    public class ToDoManagerTests
    {
        private string _testFilePath;
        private ToDoManager _toDoManager;

        [SetUp]
        public void SetUp()
        {
            _testFilePath = "test_todos.csv";
            _toDoManager = new ToDoManager(_testFilePath);
            if (File.Exists(_testFilePath))
            {
                File.Delete(_testFilePath);
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(_testFilePath))
            {
                File.Delete(_testFilePath);
            }
        }

        [Test]
        public void LoadToDos_FileExists()
        {
            File.WriteAllLines(_testFilePath, new[] { "Task 1", "Task 2" });
            var todos = _toDoManager.GetToDos();
            Assert.That(todos.Count, Is.EqualTo(2));
            Assert.That(todos[0], Is.EqualTo("Task 1"));
            Assert.That(todos[1], Is.EqualTo("Task 2"));
        }

        [Test]
        public void LoadToDos_FileDoesNotExist()
        {
            var todos = _toDoManager.GetToDos();
            Assert.That(todos.Count, Is.EqualTo(0));
        }

        [Test]
        public void SaveToDos()
        {
            var todos = new List<string> { "Task A", "Task B" };
            _toDoManager.SaveToDos(todos);
            var fileContent = File.ReadAllLines(_testFilePath);
            Assert.That(fileContent.Length, Is.EqualTo(2));
            Assert.That(fileContent[0], Is.EqualTo("Task A"));
            Assert.That(fileContent[1], Is.EqualTo("Task B"));
        }

        [Test]
        public void AddToDo_ValidToDo()
        {
            var todos = new List<string>();
            bool result = _toDoManager.AddToDo(todos, "New Task");
            Assert.IsTrue(result);
            Assert.That(todos.Count, Is.EqualTo(1));
            Assert.That(todos[0], Is.EqualTo("New Task"));
        }

        [Test]
        public void AddToDo_EmptyOrWhitespace()
        {
            var todos = new List<string>();
            Assert.IsFalse(_toDoManager.AddToDo(todos, ""));
            Assert.IsFalse(_toDoManager.AddToDo(todos, " "));
            Assert.That(todos.Count, Is.EqualTo(0));
        }

        [Test]
        public void RemoveToDo_ValidIndex()
        {
            var todos = new List<string> { "Task 1", "Task 2" };
            bool result = _toDoManager.DeleteToDo(todos, 1);
            Assert.IsTrue(result);
            Assert.That(todos.Count, Is.EqualTo(1));
            Assert.That(todos[0], Is.EqualTo("Task 2"));
        }

        [Test]
        public void RemoveToDo_InvalidIndex()
        {
            var todos = new List<string> { "Task 1" };
            Assert.IsFalse(_toDoManager.DeleteToDo(todos, 0));
            Assert.IsFalse(_toDoManager.DeleteToDo(todos, 2));
            Assert.That(todos.Count, Is.EqualTo(1));
        }

        [Test]
        public void DeleteAllToDos()
        {
            var todos = new List<string> { "Task 1", "Task 2" };
            _toDoManager.DeleteAllToDos(todos);
            Assert.That(todos.Count, Is.EqualTo(0));
        }
    }
}