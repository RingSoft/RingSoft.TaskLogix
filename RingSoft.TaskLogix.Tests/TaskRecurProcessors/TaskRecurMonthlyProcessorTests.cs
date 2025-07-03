using RingSoft.TaskLogix.DataAccess.Model;
using RingSoft.TaskLogix.Library.Processors;

namespace RingSoft.TaskLogix.Tests.TaskRecurProcessors
{
    [TestClass]
    public class TaskRecurMonthlyProcessorTests
    {
        [TestMethod]
        public void TestTaskMonthlyEveryMonth_Day31()
        {
            var taskProc = new TaskProcessor
            {
                StartDate = new DateTime(2025, 5, 1),
                RecurType = TaskRecurTypes.Monthly,
            };
            taskProc.MonthlyProcessor.RecurType = MonthlyRecurTypes.DayXOfEveryYMonths;
            taskProc.MonthlyProcessor.DayXOfEvery = 31;
            taskProc.MonthlyProcessor.OfEveryYMonths = 1;

            taskProc.DoMarkComplete();

            var expectedDate = new DateTime(2025, 6, 30);
            Assert.AreEqual(expectedDate, taskProc.StartDate);
        }

        [TestMethod]
        public void TestTaskMonthlyFirstDay()
        {
            var taskProc = new TaskProcessor
            {
                StartDate = new DateTime(2025, 5, 1),
                RecurType = TaskRecurTypes.Monthly,
            };
            taskProc.MonthlyProcessor.RecurType = MonthlyRecurTypes.XthWeekdayOfEveryYMonths;
            taskProc.MonthlyProcessor.DayType = DayTypes.Day;
            taskProc.MonthlyProcessor.WeekType = WeekTypes.First;

            taskProc.DoMarkComplete();

            var expectedDate = new DateTime(2025, 6, 1);
            Assert.AreEqual(expectedDate, taskProc.StartDate);
        }

        [TestMethod]
        public void TestTaskMonthlySecondDay()
        {
            var taskProc = new TaskProcessor
            {
                StartDate = new DateTime(2025, 5, 1),
                RecurType = TaskRecurTypes.Monthly,
            };
            taskProc.MonthlyProcessor.RecurType = MonthlyRecurTypes.XthWeekdayOfEveryYMonths;
            taskProc.MonthlyProcessor.DayType = DayTypes.Day;
            taskProc.MonthlyProcessor.WeekType = WeekTypes.Second;

            taskProc.DoMarkComplete();

            var expectedDate = new DateTime(2025, 6, 2);
            Assert.AreEqual(expectedDate, taskProc.StartDate);
        }

        [TestMethod]
        public void TestTaskMonthlyFourthDay()
        {
            var taskProc = new TaskProcessor
            {
                StartDate = new DateTime(2025, 5, 1),
                RecurType = TaskRecurTypes.Monthly,
            };
            taskProc.MonthlyProcessor.RecurType = MonthlyRecurTypes.XthWeekdayOfEveryYMonths;
            taskProc.MonthlyProcessor.DayType = DayTypes.Day;
            taskProc.MonthlyProcessor.WeekType = WeekTypes.Fourth;

            taskProc.DoMarkComplete();

            var expectedDate = new DateTime(2025, 6, 4);
            Assert.AreEqual(expectedDate, taskProc.StartDate);
        }

        [TestMethod]
        public void TestTaskMonthlyFirstWeekday()
        {
            var taskProc = new TaskProcessor
            {
                StartDate = new DateTime(2025, 5, 1),
                RecurType = TaskRecurTypes.Monthly,
            };
            taskProc.MonthlyProcessor.RecurType = MonthlyRecurTypes.XthWeekdayOfEveryYMonths;
            taskProc.MonthlyProcessor.DayType = DayTypes.Weekday;
            taskProc.MonthlyProcessor.WeekType = WeekTypes.First;

            taskProc.DoMarkComplete();

            var expectedDate = new DateTime(2025, 6, 2);
            Assert.AreEqual(expectedDate, taskProc.StartDate);
        }
    }
}