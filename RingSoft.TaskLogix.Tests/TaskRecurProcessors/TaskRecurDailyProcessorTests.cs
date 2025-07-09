using RingSoft.TaskLogix.DataAccess.Model;
using RingSoft.TaskLogix.Library.Processors;

namespace RingSoft.TaskLogix.Tests.TaskRecurProcessors
{
    [TestClass]
    public class TaskRecurDailyProcessorTests
    {
        public static TaskLogixTestDataRepository Globals { get; } =
            new TaskLogixTestDataRepository(new TestDataRegistry());

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            Globals.Initialize();
        }

        [TestMethod]
        public void TestTaskDailyEvery2Days_Friday()
        {
            var taskProc = new TaskProcessor
            {
                StartDate = new DateTime(2025, 6, 27),
                RecurType = TaskRecurTypes.Daily,
            };
            taskProc.DailyProcessor.RecurType = DailyRecurTypes.EveryXDays;
            taskProc.DailyProcessor.RecurDays = 2;

            Globals.ClearData();
            var task = new TlTask();
            Globals.SaveTlTask(task, taskProc);

            taskProc.DoMarkComplete();

            var expectedDate = new DateTime(2025, 6, 29);
            Assert.AreEqual(expectedDate, taskProc.StartDate);
        }

        [TestMethod]
        public void TestTaskDailyNextWeekday_Friday()
        {
            var taskProc = new TaskProcessor
            {
                StartDate = new DateTime(2025, 6, 27),
                RecurType = TaskRecurTypes.Daily,
            };
            taskProc.DailyProcessor.RecurType = DailyRecurTypes.EveryWeekday;

            taskProc.DoMarkComplete();

            var expectedDate = new DateTime(2025, 6, 30);
            Assert.AreEqual(expectedDate, taskProc.StartDate);

        }

        [TestMethod]
        public void TestTaskDailyNextWeekday_Monday()
        {
            var taskProc = new TaskProcessor
            {
                StartDate = new DateTime(2025, 6, 30),
                RecurType = TaskRecurTypes.Daily,
            };
            taskProc.DailyProcessor.RecurType = DailyRecurTypes.EveryWeekday;

            taskProc.DoMarkComplete();

            var expectedDate = new DateTime(2025, 7, 1);
            Assert.AreEqual(expectedDate, taskProc.StartDate);
        }

        [TestMethod]
        public void TestTaskDaily_Every2DaysAfterCompleted()
        {
            var taskProc = new TaskProcessor
            {
                StartDate = new DateTime(2025, 6, 26),
                RecurType = TaskRecurTypes.Daily,
            };

            taskProc.DailyProcessor.RecurType = DailyRecurTypes.RegenerateXDaysAfterCompleted;
            taskProc.DailyProcessor.RegenDaysAfterCompleted = 2;

            taskProc.DoMarkComplete();

            var expectedDate = DateTime.Today.AddDays(2);
            Assert.AreEqual(expectedDate, taskProc.StartDate);
        }

        [TestMethod]
        public void TestTaskDailyNextWeekday_Saturday_AdjustStartDate()
        {
            var taskProc = new TaskProcessor
            {
                StartDate = new DateTime(2025, 6, 28),
                RecurType = TaskRecurTypes.Daily,
            };
            taskProc.DailyProcessor.RecurType = DailyRecurTypes.EveryWeekday;

            taskProc.AdjustStartDate();

            var expectedDate = new DateTime(2025, 6, 30);
            Assert.AreEqual(expectedDate, taskProc.StartDate);
        }

        [TestMethod]
        public void TestTaskDailyNextWeekday_Monday_AdjustStartDate()
        {
            var taskProc = new TaskProcessor
            {
                StartDate = new DateTime(2025, 6, 30),
                RecurType = TaskRecurTypes.Daily,
            };
            taskProc.DailyProcessor.RecurType = DailyRecurTypes.EveryWeekday;

            taskProc.AdjustStartDate();

            var expectedDate = new DateTime(2025, 6, 30);
            Assert.AreEqual(expectedDate, taskProc.StartDate);
        }
    }
}
