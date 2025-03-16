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
            var todos = _toDoManager.LoadToDos();
            Assert.AreEqual(2, todos.Count);
            Assert.AreEqual("Task 1", todos[0]);
            Assert.AreEqual("Task 2", todos[1]);
        }

        [Test]
        public void LoadToDos_FileDoesNotExist()
        {
            var todos = _toDoManager.LoadToDos();
            Assert.AreEqual(0, todos.Count);
        }

        [Test]
        public void SaveToDos()
        {
            var todos = new List<string> { "Task A", "Task B" };
            _toDoManager.SaveToDos(todos);
            var fileContent = File.ReadAllLines(_testFilePath);
            Assert.AreEqual(2, fileContent.Length);
            Assert.AreEqual("Task A", fileContent[0]);
            Assert.AreEqual("Task B", fileContent[1]);
        }

        [Test]
        public void AddToDo_ValidToDo()
        {
            var todos = new List<string>();
            bool result = _toDoManager.AddToDo(todos, "New Task");
            Assert.IsTrue(result);
            Assert.AreEqual(1, todos.Count);
            Assert.AreEqual("New Task", todos[0]);
        }

        [Test]
        public void AddToDo_EmptyOrWhitespace()
        {
            var todos = new List<string>();
            Assert.IsFalse(_toDoManager.AddToDo(todos, ""));
            Assert.IsFalse(_toDoManager.AddToDo(todos, " "));
            Assert.AreEqual(0, todos.Count);
        }

        [Test]
        public void RemoveToDo_ValidIndex()
        {
            var todos = new List<string> { "Task 1", "Task 2" };
            bool result = _toDoManager.RemoveToDo(todos, 1);
            Assert.IsTrue(result);
            Assert.AreEqual(1, todos.Count);
            Assert.AreEqual("Task 2", todos[0]);
        }

        [Test]
        public void RemoveToDo_InvalidIndex()
        {
            var todos = new List<string> { "Task 1" };
            Assert.IsFalse(_toDoManager.RemoveToDo(todos, 0));
            Assert.IsFalse(_toDoManager.RemoveToDo(todos, 2));
            Assert.AreEqual(1, todos.Count);
        }

        [Test]
        public void DeleteAllToDos()
        {
            var todos = new List<string> { "Task 1", "Task 2" };
            _toDoManager.DeleteAllToDos(todos);
            Assert.AreEqual(0, todos.Count);
        }
    }
}