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
    }
}
