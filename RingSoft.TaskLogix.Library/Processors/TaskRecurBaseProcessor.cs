using RingSoft.DbLookup;
using RingSoft.TaskLogix.DataAccess.Model;

namespace RingSoft.TaskLogix.Library.Processors
{
    public abstract class TaskRecurProcessorBase
    {
        public TaskProcessor TaskProcessor { get; private set; }

        public TaskRecurProcessorBase(TaskProcessor taskProcessor)
        {
            TaskProcessor = taskProcessor;
        }

        public abstract void DoMarkComplete();

        public abstract void AdjustStartDate();

        public abstract void LoadRecurProcessor(TlTask task);

        public abstract bool SaveRecurProcessor(TlTask task, IDbContext context);

        public abstract string GetRecurText();
    }
}
