using RingSoft.TaskLogix.DataAccess.Model;
using RingSoft.TaskLogix.Library.Processors;

namespace RingSoft.TaskLogix.Tests.TaskRecurProcessors
{
    [TestClass]
    public class TaskRecurWeeklyProcessorTests
    {
        [TestMethod]
        public void TestTaskWeekly_1Weekday()
        {
            var taskProc = new TaskProcessor
            {
                StartDate = new DateTime(2025, 6, 24),
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

        [TestMethod]
        public void TestTaskWeekly_Every2WeeksAfterCompleted()
        {
            var taskProc = new TaskProcessor
            {
                StartDate = new DateTime(2025, 6, 26),
                RecurType = TaskRecurTypes.Weekly,
            };

            taskProc.WeeklyProcessor.RecurType = WeeklyRecurTypes.RegenerateXWeeksAfterCompleted;
            taskProc.WeeklyProcessor.RegenWeeksAfterCompleted = 2;

            taskProc.DoMarkComplete();

            var expectedDate = DateTime.Today.AddDays(14);
            Assert.AreEqual(expectedDate, taskProc.StartDate);
        }

        [TestMethod]
        public void TestTaskWeekly_2Weekdays_AdjustStartDate()
        {
            var taskProc = new TaskProcessor
            {
                StartDate = new DateTime(2025, 6, 26),
                RecurType = TaskRecurTypes.Weekly,
            };

            taskProc.WeeklyProcessor.Thursday = false;
            taskProc.WeeklyProcessor.Tuesday = true;
            taskProc.WeeklyProcessor.Wednesday = true;

            taskProc.AdjustStartDate();

            var expectedDate = new DateTime(2025, 7, 1);
            Assert.AreEqual(expectedDate, taskProc.StartDate);
        }

    }
}
