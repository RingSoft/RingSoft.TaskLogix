using System.Diagnostics;
using RingSoft.DbLookup;
using RingSoft.TaskLogix.DataAccess.Model;
using RingSoft.TaskLogix.Library.Processors;

namespace RingSoft.TaskLogix.Tests
{
    [TestClass]
    public class TaskTests
    {
        public static TaskLogixTestDataRepository DataRepository { get; }
            = new TaskLogixTestDataRepository(new TestDataRegistry());

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            DataRepository.Initialize();
        }

        [TestMethod]
        public void TestTaskWeekly_1Weekday()
        {
            var taskProc = new TaskProcessor
            {
                StartDate = new DateTime(2025,6,24),
                RecurType = TaskRecurTypes.Weekly,
            };

            taskProc.DoMarkComplete();

            var expectedDate = new DateTime(2025, 7, 1);
            Assert.AreEqual(expectedDate, taskProc.StartDate);
        }

        [TestMethod]
        public void TestTaskWeekly_2Weekdays()
        {
            var taskProc = new TaskProcessor
            {
                StartDate = new DateTime(2025, 6, 24),
                RecurType = TaskRecurTypes.Weekly,
            };

            taskProc.WeeklyProcessor.Thursday = true;

            taskProc.DoMarkComplete();

            var expectedDate = new DateTime(2025, 6, 26);
            Assert.AreEqual(expectedDate, taskProc.StartDate);
        }

        [TestMethod]
        public void TestTaskWeekly_1WeekdayEvery2Weeks()
        {
            var taskProc = new TaskProcessor
            {
                StartDate = new DateTime(2025, 6, 24),
                RecurType = TaskRecurTypes.Weekly,
            };

            taskProc.WeeklyProcessor.RecurWeeks = 2;

            taskProc.DoMarkComplete();

            var expectedDate = new DateTime(2025, 7, 8);
            Assert.AreEqual(expectedDate, taskProc.StartDate);
        }

    }
}
