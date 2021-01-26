namespace Bd_Course_Project
{
    public abstract class DataBaseEntity : IDataBaseEntity
    {
        protected IDataAdapter DataAdapter { get; set; }

        public DataBaseEntity(long? id, IDataAdapter dataAdapter)
        {
            Id = id;
            DataAdapter = dataAdapter;
        }

        public DataBaseEntity()
        {
            Id = null;
        }

        public long? Id { get; set; }

        public abstract long Add();

        public abstract void Remove();

        public abstract void Update();
    }
}
