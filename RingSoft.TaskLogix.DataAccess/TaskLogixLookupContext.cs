using RingSoft.CustomTemplate.Library;
using RingSoft.DbLookup.ModelDefinition;
using RingSoft.TaskLogix.DataAccess.Model;

namespace RingSoft.TaskLogix.DataAccess
{
    public class TaskLogixLookupContext : CustomTemplateLookupContext
    {
        public TableDefinition<TlTask> Tasks { get; set; }

        protected override void SetupTemplateLookupDefinitions()
        {
            
        }

        protected override void SetupTemplateModel()
        {
            
        }
    }
}
