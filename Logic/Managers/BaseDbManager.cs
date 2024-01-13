using Logic.Interfaces;
using Logic.Interfaces.Repositories.Base;
using Logic.Utilities;

namespace Logic.Managers
{

    public abstract class BaseDbManager<T> where T : IEntity
    {
        protected readonly IDbRepository<T> repository;
        protected BaseDbManager(IDbRepository<T> repository)
        {
            this.repository = repository;
        }
        public Result<List<T>> GetAll(int? offsetIndex = null)
        {
            return Result<List<T>>.From(() => repository.GetAll(offsetIndex));
        }
        public Result<T> GetById(Guid id)
        {
            return Result<T>.From(() => repository.GetById(id));
        }
        virtual public Result Create(T entity)
        {
            return Result.From(() => repository.Create(entity));
        }
        virtual public Result Update(T entity)
        {
            return Result.From(() => repository.Update(entity));
        }
        public Result Delete(Guid id)
        {
            return Result.From(() => repository.Delete(id));
        }
    }
}
