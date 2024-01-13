using Logic.Interfaces.Repositories;
using Logic.Utilities;
using Shared;
using Shared.Errors;

namespace Domain.Managers
{
    public class LocalDataManager
    {
        private readonly IConfigurationRepository repository;

        public LocalDataManager(IConfigurationRepository repository)
        {
            this.repository = repository;
        }

        public Result<Guid> GetLastLoggedAs()
        {
            return Result<Guid>.From(() =>
            {
                string id = repository.Read(Constants.LAST_LOGGED_KEY) ?? throw new NotFoundException();

                Guid Id = Guid.Parse(id);
                if (Id == Guid.Empty) throw new NotFoundException();

                return Guid.Parse(id);
            });
        }

        public Result SetLastLoggedAs(Guid? id)
        {
            return Result.From(() => repository.Update(Constants.LAST_LOGGED_KEY, id.ToString()));
        }
    }
}
