using RingSoft.CustomTemplate.Library;
using RingSoft.DbLookup.Lookup;
using RingSoft.DbLookup.ModelDefinition;
using RingSoft.TaskLogix.DataAccess.Model;

namespace RingSoft.TaskLogix.DataAccess
{
    public class TaskLogixLookupContext : CustomTemplateLookupContext
    {
        public TableDefinition<TlTask> Tasks { get; set; }

        public LookupDefinition<TaskLookup, TlTask> TaskLookupDefinition { get; set; }

        protected override void SetupTemplateLookupDefinitions()
        {
            TaskLookupDefinition = new LookupDefinition<TaskLookup, TlTask>(Tasks);

            TaskLookupDefinition.AddVisibleColumnDefinition(
                p => p.Subject
                , "Subject"
                , p => p.Subject, 70);

            TaskLookupDefinition.AddVisibleColumnDefinition(
                p => p.DueDate
                , "Due Date"
                , p => p.DueDate, 30);

            Tasks.HasLookupDefinition(TaskLookupDefinition);
        }

        protected override void SetupTemplateModel()
        {
            Tasks.HasDescription("Tasks").HasRecordDescription("Task");
        }
    }
}
