using RingSoft.TaskLogix.SqlServer.Migrations;

namespace RingSoft.TaskLogix.Library.Processors
{
    public class TaskRecurWeeklyProcessor : TaskRecurProcessorBase
    {
        public TaskRecurWeeklyProcessor(TaskProcessor taskProcessor) : base(taskProcessor)
        {
        }

        public TlTaskRecurWeekly GetEntityData()
        {
            throw new NotImplementedException();
        }

        public void SetPropsFromEntity(TlTaskRecurWeekly entity)
        {
            throw new NotImplementedException();
        }

        public override bool DoMarkComplete()
        {
            throw new NotImplementedException();
        }
    }
}
