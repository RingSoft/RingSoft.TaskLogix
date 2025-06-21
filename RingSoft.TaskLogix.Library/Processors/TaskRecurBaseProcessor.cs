namespace RingSoft.TaskLogix.Library.Processors
{
    public abstract class TaskRecurBaseProcessor<TEntity> where TEntity : class, new()
    {
        public TaskRecurBaseProcessor()
        {

        }

        public abstract TEntity GetEntityData();

        public abstract void SetPropsFromEntity(TEntity entity);

        public abstract bool DoMarkComplete();
    }
}
