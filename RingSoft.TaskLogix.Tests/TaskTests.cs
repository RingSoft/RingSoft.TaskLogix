using RingSoft.TaskLogix.DataAccess.Model;
using RingSoft.TaskLogix.Library;

namespace RingSoft.TaskLogix.Tests
{
    [TestClass]
    public class TaskTests
    {
        [TestMethod]
        public void TestWeekType()
        {
            var weekType = new DateTime(2025, 8, 4).GetWeekType();
            Assert.AreEqual(WeekTypes.First, weekType);

            weekType = new DateTime(2025, 8, 24).GetWeekType();
            Assert.AreEqual(WeekTypes.Fourth, weekType);

            weekType = new DateTime(2025, 8, 29).GetWeekType();
            Assert.AreEqual(WeekTypes.Last, weekType);
        }

        [TestMethod]
        public void TestDayType()
        {
            var dayType = new DateTime(2025, 8, 4).GetDayType();
            Assert.AreEqual(DayTypes.Monday, dayType);

            dayType = new DateTime(2025, 8, 24).GetDayType();
            Assert.AreEqual(DayTypes.Sunday, dayType);

            dayType = new DateTime(2025, 8, 29).GetDayType();
            Assert.AreEqual(DayTypes.Friday, dayType);
        }
    }
}
