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
    }
}