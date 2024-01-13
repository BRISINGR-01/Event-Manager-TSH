using Logic.Interfaces.Repositories;
using Logic.Managers;
using Logic.Models;
using Logic.Utilities;

namespace Domain.Managers
{
    public class CredentialsManager : BaseDbManager<Credentials>
    {
        private readonly ICredentialsRepository _repository;
        public CredentialsManager(ICredentialsRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public override Result Create(Credentials entity)
        {
            return Result.From(() =>
            {
                entity.ValidateEmail();
                base.Create(entity);
            });
        }
        public Result<Credentials> GetByEmail(string email)
        {
            return Result<Credentials>.From(() => _repository.GetByEmail(email));
        }
    }
}