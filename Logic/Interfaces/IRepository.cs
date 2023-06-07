namespace Logic.Interfaces.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        public Guid BranchId { get; }
        List<T> GetAll(int? offsetIndex = null);
        bool Create(T entity);
        bool Update(T entity);
        bool Delete(Guid id);
        public T? FindSingleBy(Guid id);
    }
}
