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
    }
}
