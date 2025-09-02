using RingSoft.DbLookup;
using RingSoft.DbLookup.AutoFill;
using RingSoft.DbMaintenance;
using RingSoft.TaskLogix.DataAccess.Model;
using RingSoft.TaskLogix.Library;
using RingSoft.TaskLogix.Library.ViewModels;

//First--Comment Out Parallelize in MSTestSettings.cs file.
namespace RingSoft.TaskLogix.Tests.TaskLists
{
    [TestClass]
    public sealed class TaskListTests
    {
        private static TestGlobals _globals;
        private static DbMaintenanceTestGlobals<TaskMaintenanceViewModel, TestTaskMaintView> _maintViewModel;

        static TaskListTests()
        {
            _globals = new TestGlobals();
            _maintViewModel =
                new DbMaintenanceTestGlobals<TaskMaintenanceViewModel, TestTaskMaintView>(_globals.DataRepository);
        }

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            _globals.Initialize();
            _maintViewModel.Initialize();
        }

        [TestMethod]
        public void TestCurrentDayMonday_ThisWeek_Saturday()
        {
            _globals.DataRepository.ClearData();

            _maintViewModel.ViewModel.NewCommand.Execute(null);
            _maintViewModel.ViewModel.KeyAutoFillValue = new AutoFillValue(null, "Test");
            _maintViewModel.ViewModel.DueDate = new DateTime(2025, 8, 9);
            _maintViewModel.ViewModel.SaveCommand.Execute(null);

            var context = SystemGlobals.DataRepository.GetDataContext();
            Assert.IsNotNull(context);
            var table = context.GetTable<TlTask>();

            var viewModel = new TaskListViewModel();
            viewModel.CurrentDate = new DateTime(2025, 8, 4);
            viewModel.Initialize(TaskListTypes.ThisWeek);

            Assert.AreEqual(1, viewModel.TaskList.FirstOrDefault().TaskId);
            Assert.AreEqual(new DateTime(2025, 8, 6), viewModel.StartDate);
            Assert.AreEqual(new DateTime(2025, 8, 9), viewModel.EndDate);

            viewModel.CurrentDate = new DateTime(2025, 8, 1);
            viewModel.Initialize(TaskListTypes.ThisWeek);

            Assert.AreEqual(false, viewModel.TaskList.Any());
            Assert.AreEqual(null, viewModel.StartDate);
            Assert.AreEqual(null, viewModel.EndDate);

            viewModel.CurrentDate = new DateTime(2025, 8, 2);
            viewModel.Initialize(TaskListTypes.ThisWeek);

            Assert.AreEqual(false, viewModel.TaskList.Any());
            Assert.AreEqual(null, viewModel.StartDate);
            Assert.AreEqual(null, viewModel.EndDate);
            
        }

        [TestMethod]
        public void TestCurrentDateSaturday_Old()
        {
            _globals.DataRepository.ClearData();

            var viewModel = new TaskListViewModel();
            var tlTask = new TlTask
            {
                Id = 1,
                Subject = "Test",
                DueDate = new DateTime(2025, 8, 9),
            };
            var context = SystemGlobals.DataRepository.GetDataContext();

            _globals.DataRepository.ClearData();
            tlTask = new TlTask
            {
                Id = 1,
                Subject = "Test",
                DueDate = new DateTime(2025, 8, 20),
            };
            context.SaveEntity(tlTask, "");

            viewModel.CurrentDate = new DateTime(2025, 8, 4);
            viewModel.Initialize(TaskListTypes.ThisMonth);

            Assert.AreEqual(1, viewModel.TaskList.FirstOrDefault().TaskId);
            Assert.AreEqual(new DateTime(2025, 8, 10), viewModel.StartDate);
            Assert.AreEqual(new DateTime(2025, 8, 31), viewModel.EndDate);

            viewModel.CurrentDate = new DateTime(2025, 8, 9);
            viewModel.Initialize(TaskListTypes.ThisMonth);

            Assert.AreEqual(1, viewModel.TaskList.FirstOrDefault().TaskId);
            Assert.AreEqual(new DateTime(2025, 8, 11), viewModel.StartDate);
            Assert.AreEqual(new DateTime(2025, 8, 31), viewModel.EndDate);

            viewModel.CurrentDate = new DateTime(2025, 8, 8);
            viewModel.Initialize(TaskListTypes.ThisMonth);

            Assert.AreEqual(1, viewModel.TaskList.FirstOrDefault().TaskId);
            Assert.AreEqual(new DateTime(2025, 8, 10), viewModel.StartDate);
            Assert.AreEqual(new DateTime(2025, 8, 31), viewModel.EndDate);

            viewModel.CurrentDate = new DateTime(2025, 8, 30);
            tlTask.DueDate = new DateTime(2025, 8, 31);
            viewModel.Initialize(TaskListTypes.ThisMonth);

            Assert.AreEqual(false, viewModel.TaskList.Any());
            Assert.AreEqual(null, viewModel.StartDate);
            Assert.AreEqual(null, viewModel.EndDate);

            //------------------------------------------------------------------

            _globals.DataRepository.ClearData();
            tlTask = new TlTask
            {
                Id = 1,
                Subject = "Test",
                DueDate = new DateTime(2025, 9, 5),
            };
            context.SaveEntity(tlTask, "");

            viewModel.CurrentDate = new DateTime(2025, 8, 1);
            viewModel.Initialize(TaskListTypes.NextMonth);

            Assert.AreEqual(1, viewModel.TaskList.FirstOrDefault().TaskId);
            Assert.AreEqual(new DateTime(2025, 9, 1), viewModel.StartDate);
            Assert.AreEqual(new DateTime(2025, 9, 30), viewModel.EndDate);

            viewModel.CurrentDate = new DateTime(2025, 8, 31);
            viewModel.Initialize(TaskListTypes.NextMonth);

            Assert.AreEqual(false, viewModel.TaskList.Any());
            Assert.AreEqual(new DateTime(2025, 9, 7), viewModel.StartDate);
            Assert.AreEqual(new DateTime(2025, 9, 30), viewModel.EndDate);

            viewModel.CurrentDate = new DateTime(2025, 8, 31);
            tlTask.DueDate = new DateTime(2025, 9, 1);
            viewModel.Initialize(TaskListTypes.NextMonth);

            Assert.AreEqual(false, viewModel.TaskList.Any());
            Assert.AreEqual(new DateTime(2025, 9, 7), viewModel.StartDate);
            Assert.AreEqual(new DateTime(2025, 9, 30), viewModel.EndDate);

        }
    }
}
