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

        [TestMethod]
        public void TestTaskMonthlyFourthWeekday_May2025()
        {
            var taskProc = new TaskProcessor
            {
                StartDate = new DateTime(2025, 4, 1),
                RecurType = TaskRecurTypes.Monthly,
            };
            taskProc.MonthlyProcessor.RecurType = MonthlyRecurTypes.XthWeekdayOfEveryYMonths;
            taskProc.MonthlyProcessor.DayType = DayTypes.Weekday;
            taskProc.MonthlyProcessor.WeekType = WeekTypes.Fourth;

            taskProc.DoMarkComplete();

            var expectedDate = new DateTime(2025, 5, 6);
            Assert.AreEqual(expectedDate, taskProc.StartDate);
        }

        [TestMethod]
        public void TestTaskMonthlyFourthWeekendDay_May2025()
        {
            var taskProc = new TaskProcessor
            {
                StartDate = new DateTime(2025, 4, 1),
                RecurType = TaskRecurTypes.Monthly,
            };
            taskProc.MonthlyProcessor.RecurType = MonthlyRecurTypes.XthWeekdayOfEveryYMonths;
            taskProc.MonthlyProcessor.DayType = DayTypes.WeekendDay;
            taskProc.MonthlyProcessor.WeekType = WeekTypes.Fourth;

            taskProc.DoMarkComplete();

            var expectedDate = new DateTime(2025, 5, 11);
            Assert.AreEqual(expectedDate, taskProc.StartDate);
        }

        [TestMethod]
        public void TestTaskMonthlyFirstWeekendDay_June2025()
        {
            var taskProc = new TaskProcessor
            {
                StartDate = new DateTime(2025, 5, 1),
                RecurType = TaskRecurTypes.Monthly,
            };
            taskProc.MonthlyProcessor.RecurType = MonthlyRecurTypes.XthWeekdayOfEveryYMonths;
            taskProc.MonthlyProcessor.DayType = DayTypes.WeekendDay;
            taskProc.MonthlyProcessor.WeekType = WeekTypes.First;

            taskProc.DoMarkComplete();

            var expectedDate = new DateTime(2025, 6, 1);
            Assert.AreEqual(expectedDate, taskProc.StartDate);
        }

        [TestMethod]
        public void TestTaskMonthlyLastWeekday_May2025()
        {
            var taskProc = new TaskProcessor
            {
                StartDate = new DateTime(2025, 4, 1),
                RecurType = TaskRecurTypes.Monthly,
            };
            taskProc.MonthlyProcessor.RecurType = MonthlyRecurTypes.XthWeekdayOfEveryYMonths;
            taskProc.MonthlyProcessor.DayType = DayTypes.Weekday;
            taskProc.MonthlyProcessor.WeekType = WeekTypes.Last;

            taskProc.DoMarkComplete();

            var expectedDate = new DateTime(2025, 5, 30);
            Assert.AreEqual(expectedDate, taskProc.StartDate);
        }

        [TestMethod]
        public void TestTaskMonthlyLastWeekendDay_July2025()
        {
            var taskProc = new TaskProcessor
            {
                StartDate = new DateTime(2025, 6, 1),
                RecurType = TaskRecurTypes.Monthly,
            };
            taskProc.MonthlyProcessor.RecurType = MonthlyRecurTypes.XthWeekdayOfEveryYMonths;
            taskProc.MonthlyProcessor.DayType = DayTypes.WeekendDay;
            taskProc.MonthlyProcessor.WeekType = WeekTypes.Last;

            taskProc.DoMarkComplete();

            var expectedDate = new DateTime(2025, 7, 27);
            Assert.AreEqual(expectedDate, taskProc.StartDate);
        }

        [TestMethod]
        public void TestTaskMonthlyFirstWednesday_July2025()
        {
            var taskProc = new TaskProcessor
            {
                StartDate = new DateTime(2025, 6, 1),
                RecurType = TaskRecurTypes.Monthly,
            };
            taskProc.MonthlyProcessor.RecurType = MonthlyRecurTypes.XthWeekdayOfEveryYMonths;
            taskProc.MonthlyProcessor.WeekType = WeekTypes.First;
            taskProc.MonthlyProcessor.DayType = DayTypes.Wednesday;

            taskProc.DoMarkComplete();

            var expectedDate = new DateTime(2025, 7, 2);
            Assert.AreEqual(expectedDate, taskProc.StartDate);
        }

        [TestMethod]
        public void TestTaskMonthlySecondWednesday_July2025()
        {
            var taskProc = new TaskProcessor
            {
                StartDate = new DateTime(2025, 6, 1),
                RecurType = TaskRecurTypes.Monthly,
            };
            taskProc.MonthlyProcessor.RecurType = MonthlyRecurTypes.XthWeekdayOfEveryYMonths;
            taskProc.MonthlyProcessor.WeekType = WeekTypes.Second;
            taskProc.MonthlyProcessor.DayType = DayTypes.Wednesday;

            taskProc.DoMarkComplete();

            var expectedDate = new DateTime(2025, 7, 9);
            Assert.AreEqual(expectedDate, taskProc.StartDate);
        }

        [TestMethod]
        public void TestTaskMonthly_AdjustDate_SecondWednesday_July2025()
        {
            var taskProc = new TaskProcessor
            {
                StartDate = new DateTime(2025, 7, 1),
                RecurType = TaskRecurTypes.Monthly,
            };
            taskProc.MonthlyProcessor.RecurType = MonthlyRecurTypes.XthWeekdayOfEveryYMonths;
            taskProc.MonthlyProcessor.WeekType = WeekTypes.Second;
            taskProc.MonthlyProcessor.DayType = DayTypes.Wednesday;

            taskProc.AdjustStartDate();

            var expectedDate = new DateTime(2025, 7, 9);
            Assert.AreEqual(expectedDate, taskProc.StartDate);
        }
    }
}