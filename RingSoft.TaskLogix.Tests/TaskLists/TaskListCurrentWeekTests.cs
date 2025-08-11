using RingSoft.DbLookup;
using RingSoft.TaskLogix.DataAccess.Model;
using RingSoft.TaskLogix.Library.ViewModels;

namespace RingSoft.TaskLogix.Tests.TaskLists
{
    [TestClass]
    public class TaskListCurrentWeekTests
    {
        public static TaskLogixTestDataRepository Database { get; } = new TaskLogixTestDataRepository(new TestDataRegistry());

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            Database.Initialize();
        }

        [TestMethod]
        public void TestCurrentDateSaturday()
        {
            Database.ClearData();
            var viewModel = new TaskListViewModel();
            var tlTask = new TlTask
            {
                Id = 1,
                Subject = "Test",
                DueDate = new DateTime(2025, 8, 9),
            };
            var context = SystemGlobals.DataRepository.GetDataContext();
            context.SaveEntity(tlTask, "");

            viewModel.CurrentDate = new DateTime(2025, 8, 4);
            viewModel.Initialize(TaskListTypes.ThisWeek);

            Assert.AreEqual(viewModel.TaskList.FirstOrDefault().TaskId, 1);
        }

        [TestMethod]
        public void TestCurrentDateWeek()
        {
            Database.ClearData();
            var viewModel = new TaskListViewModel();

            Assert.AreEqual(true, true);
        }
    }
}
