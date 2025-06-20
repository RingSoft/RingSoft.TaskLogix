using RingSoft.DbLookup;
using RingSoft.TaskLogix.DataAccess.Model;

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
        public void TestErrorSave()
        {
            var task = new TlTask();
            var context = SystemGlobals.DataRepository.GetDataContext();
            context.SaveEntity(task, "");

            var entity = context.GetTable<TlTask>();
        }
    }
}
