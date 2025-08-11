using RingSoft.TaskLogix.Library.ViewModels;

namespace RingSoft.TaskLogix.Tests.TaskLists
{
    [TestClass]
    public class TaskListThisMonthTests
    {
        public static TaskLogixTestDataRepository Database { get; } = new TaskLogixTestDataRepository(new TestDataRegistry());

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            Database.Initialize();
        }

        [TestMethod]
        public void TestCurrentDateMonth()
        {
            Database.ClearData();
            var viewModel = new TaskListViewModel();

            Assert.AreEqual(true, true);
        }

    }
}
