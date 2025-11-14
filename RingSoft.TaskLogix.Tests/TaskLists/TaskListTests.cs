using System;
using System.Linq;
using RingSoft.DbLookup;
using RingSoft.DbLookup.AutoFill;
using RingSoft.DbMaintenance;
using RingSoft.TaskLogix.DataAccess.Model;
using RingSoft.TaskLogix.Library;
using RingSoft.TaskLogix.Library.ViewModels;
using System.Threading.Tasks;

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
        public void TestCurrentDayMonday_ThisMonth_Wednesday()
        {
            _globals.DataRepository.ClearData();

            _maintViewModel.ViewModel.NewCommand.Execute(null);
            _maintViewModel.ViewModel.KeyAutoFillValue = new AutoFillValue(null, "Test");
            _maintViewModel.ViewModel.DueDate = new DateTime(2025, 8, 20);
            _maintViewModel.ViewModel.SaveCommand.Execute(null);

            var viewModel = new TaskListViewModel();

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
       }

        [TestMethod]
        public void TestCurrentDaySaturday_ThisMonth_Sunday()
        {
            _globals.DataRepository.ClearData();

            _maintViewModel.ViewModel.NewCommand.Execute(null);
            _maintViewModel.ViewModel.KeyAutoFillValue = new AutoFillValue(null, "Test");
            _maintViewModel.ViewModel.DueDate = new DateTime(2025, 8, 31);
            _maintViewModel.ViewModel.SaveCommand.Execute(null);

            var viewModel = new TaskListViewModel();
            viewModel.CurrentDate = new DateTime(2025, 8, 30);

            viewModel.Initialize(TaskListTypes.ThisMonth);

            Assert.AreEqual(false, viewModel.TaskList.Any());
            Assert.AreEqual(null, viewModel.StartDate);
            Assert.AreEqual(null, viewModel.EndDate);
        }

        [TestMethod]
        public void TestCurrentDayFriday_NextMonth_Friday()
        {
            _globals.DataRepository.ClearData();

            _maintViewModel.ViewModel.NewCommand.Execute(null);
            _maintViewModel.ViewModel.KeyAutoFillValue = new AutoFillValue(null, "Test");
            _maintViewModel.ViewModel.DueDate = new DateTime(2025, 9, 5);
            _maintViewModel.ViewModel.SaveCommand.Execute(null);

            var viewModel = new TaskListViewModel();
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
        }

        [TestMethod]
        public void TestCurrentDaySunday_NextMonth_Monday()
        {
            _globals.DataRepository.ClearData();

            _maintViewModel.ViewModel.NewCommand.Execute(null);
            _maintViewModel.ViewModel.KeyAutoFillValue = new AutoFillValue(null, "Test");
            _maintViewModel.ViewModel.DueDate = new DateTime(2025, 9, 1);
            _maintViewModel.ViewModel.SaveCommand.Execute(null);

            var viewModel = new TaskListViewModel();
            viewModel.CurrentDate = new DateTime(2025, 8, 31);

            viewModel.Initialize(TaskListTypes.NextMonth);

            Assert.AreEqual(false, viewModel.TaskList.Any());
            Assert.AreEqual(new DateTime(2025, 9, 7), viewModel.StartDate);
            Assert.AreEqual(new DateTime(2025, 9, 30), viewModel.EndDate);
        }

        [TestMethod]
        public void TestCurrentDayFriday_NextMonth_Saturday()
        {
            _globals.DataRepository.ClearData();

            _maintViewModel.ViewModel.NewCommand.Execute(null);
            _maintViewModel.ViewModel.KeyAutoFillValue = new AutoFillValue(null, "Test");
            _maintViewModel.ViewModel.DueDate = new DateTime(2025, 11, 1);
            _maintViewModel.ViewModel.SaveCommand.Execute(null);

            var viewModel = new TaskListViewModel();
            viewModel.CurrentDate = new DateTime(2025, 10, 31);

            viewModel.Initialize(TaskListTypes.NextMonth);

            Assert.AreEqual(false, viewModel.TaskList.Any());
            Assert.AreEqual(new DateTime(2025, 11, 2), viewModel.StartDate);
            Assert.AreEqual(new DateTime(2025, 11, 30), viewModel.EndDate);
        }

        [TestMethod]
        public void TestCurrentDaySaturday_NextMonth_Sunday()
        {
            _globals.DataRepository.ClearData();

            _maintViewModel.ViewModel.NewCommand.Execute(null);
            _maintViewModel.ViewModel.KeyAutoFillValue = new AutoFillValue(null, "Test");
            _maintViewModel.ViewModel.DueDate = new DateTime(2026, 2, 1);
            _maintViewModel.ViewModel.SaveCommand.Execute(null);

            var viewModel = new TaskListViewModel();
            viewModel.CurrentDate = new DateTime(2026, 1, 31);

            viewModel.Initialize(TaskListTypes.NextMonth);

            Assert.AreEqual(false, viewModel.TaskList.Any());
            Assert.AreEqual(new DateTime(2026, 2, 2), viewModel.StartDate);
            Assert.AreEqual(new DateTime(2026, 2, 28), viewModel.EndDate);
        }

        [TestMethod]
        public void TestCurrentDay29thAnniversary_NextMonth_Saturday()
        {
            _globals.DataRepository.ClearData();

            _maintViewModel.ViewModel.NewCommand.Execute(null);
            _maintViewModel.ViewModel.KeyAutoFillValue = new AutoFillValue(null, "Test");
            _maintViewModel.ViewModel.DueDate = new DateTime(2025, 11, 1);
            _maintViewModel.ViewModel.SaveCommand.Execute(null);

            var viewModel = new TaskListViewModel();
            viewModel.CurrentDate = new DateTime(2025, 10, 26);

            viewModel.Initialize(TaskListTypes.NextMonth);

            Assert.AreEqual(false, viewModel.TaskList.Any());
            Assert.AreEqual(new DateTime(2025, 11, 2), viewModel.StartDate);
            Assert.AreEqual(new DateTime(2025, 11, 30), viewModel.EndDate);
        }

    }
}
