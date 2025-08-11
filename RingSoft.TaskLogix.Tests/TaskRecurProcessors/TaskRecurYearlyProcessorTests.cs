using RingSoft.TaskLogix.DataAccess.Model;
using RingSoft.TaskLogix.Library.Processors;

namespace RingSoft.TaskLogix.Tests.TaskRecurProcessors
{
    [TestClass]
    public class TaskRecurYearlyProcessorTests
    {
        [TestMethod]
        public void TestTaskYearlyEveryJune_Day31()
        {
            var taskProc = new TaskProcessor
            {
                UnitTestMode = true,
                StartDate = new DateTime(2025, 6, 1),
                RecurType = TaskRecurTypes.Yearly,
            };
            taskProc.YearlyProcessor.RecurType = YearlylyRecurTypes.EveryMonthDayX;
            taskProc.YearlyProcessor.MonthDay = 31;
            taskProc.YearlyProcessor.EveryMonthType = MonthsInYear.June;

            taskProc.DoMarkComplete();

            var expectedDate = new DateTime(2026, 6, 30);
            Assert.AreEqual(expectedDate, taskProc.StartDate);
        }

        [TestMethod]
        public void TestTaskYearlyJuneFirstDay()
        {
            var taskProc = new TaskProcessor
            {
                UnitTestMode = true,
                StartDate = new DateTime(2024, 6, 1),
                RecurType = TaskRecurTypes.Yearly,
            };
            taskProc.YearlyProcessor.RecurType = YearlylyRecurTypes.TheNthWeekdayTypeOfMonth;
            taskProc.YearlyProcessor.DayType = DayTypes.Day;
            taskProc.YearlyProcessor.WeekType = WeekTypes.First;
            taskProc.YearlyProcessor.WeekMonthType = MonthsInYear.June;

            taskProc.DoMarkComplete();

            var expectedDate = new DateTime(2025, 6, 1);
            Assert.AreEqual(expectedDate, taskProc.StartDate);
        }

        [TestMethod]
        public void TestTaskYearlyJuneFirstWeekday()
        {
            var taskProc = new TaskProcessor
            {
                UnitTestMode = true,
                StartDate = new DateTime(2024, 6, 1),
                RecurType = TaskRecurTypes.Yearly,
            };
            taskProc.YearlyProcessor.RecurType = YearlylyRecurTypes.TheNthWeekdayTypeOfMonth;
            taskProc.YearlyProcessor.DayType = DayTypes.Weekday;
            taskProc.YearlyProcessor.WeekType = WeekTypes.First;
            taskProc.YearlyProcessor.WeekMonthType = MonthsInYear.June;

            taskProc.DoMarkComplete();

            var expectedDate = new DateTime(2025, 6, 2);
            Assert.AreEqual(expectedDate, taskProc.StartDate);
        }

        [TestMethod]
        public void TestTaskYearlyAdjStartDateJuneFirstWeekday()
        {
            var taskProc = new TaskProcessor
            {
                UnitTestMode = true,
                StartDate = new DateTime(2025, 6, 1),
                RecurType = TaskRecurTypes.Yearly,
            };
            taskProc.YearlyProcessor.RecurType = YearlylyRecurTypes.TheNthWeekdayTypeOfMonth;
            taskProc.YearlyProcessor.DayType = DayTypes.Weekday;
            taskProc.YearlyProcessor.WeekType = WeekTypes.First;
            taskProc.YearlyProcessor.WeekMonthType = MonthsInYear.June;

            taskProc.AdjustStartDate();

            var expectedDate = new DateTime(2025, 6, 2);
            Assert.AreEqual(expectedDate, taskProc.StartDate);
        }

        [TestMethod]
        public void TestTaskYerlyRegenerate2YearsAfterCompleted()
        {
            var taskProc = new TaskProcessor
            {
                UnitTestMode = true,
                StartDate = new DateTime(2025, 6, 1),
                RecurType = TaskRecurTypes.Yearly,
            };
            taskProc.YearlyProcessor.RecurType = YearlylyRecurTypes.RegenerateXYearsAfterCompleted;
            taskProc.YearlyProcessor.RegenYearsAfterCompleted = 2;

            taskProc.DoMarkComplete();

            var expectedDate = DateTime.Today.AddYears(2);
            Assert.AreEqual(expectedDate, taskProc.StartDate);
        }
    }
}
