namespace Logic.Interfaces.Repositories.Base
{

    public interface IDbRepository<T> : IRepository<T> where T : IEntity
    {
        public T? GetById(Guid id);
        public List<T> GetAll(int? offsetIndex = null);
        public bool Create(T entity);
        public bool Update(T entity);
        public bool Delete(Guid id);
    }
}
