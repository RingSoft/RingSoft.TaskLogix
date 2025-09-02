using RingSoft.TaskLogix.Library;
using RingSoft.TaskLogix.Library.ViewModels;
//First--Comment Out Parallelize in MSTestSettings.cs file.
namespace RingSoft.TaskLogix.Tests
{
    public class TestGlobals
    {
        public TaskLogixTestDataRepository DataRepository { get; }

        public TestGlobals()
        {
            DataRepository = new TaskLogixTestDataRepository(new TestDataRegistry());
        }

        public void Initialize()
        {
            var appGlobals = new AppGlobals();
            appGlobals.UtInitialize(DataRepository);
            AppGlobals.MainViewModel = new MainViewModel();
            AppGlobals.MainViewModel.Initialize(new TestMainView());
        }
    }
}
