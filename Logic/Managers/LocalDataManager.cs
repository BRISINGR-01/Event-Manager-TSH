using Logic.Interfaces.Repositories;
using Shared;

namespace Domain.Managers
{
    public class LocalDataManager
    {
        private readonly ILocalRepository repository;

        public LocalDataManager(ILocalRepository repository)
        {
            this.repository = repository;
        }

        public Guid? GetLastLoggedAs()
        {
            try
            {
                string? id = repository.Read(Constants.LAST_LOGGED_KEY);

                if (id == null) return null;
                
                Guid Id = Guid.Parse(id);
                if (Id == Guid.Empty) return null;
                
                return Guid.Parse(id);
            } catch
            {
                return null;
            }
        }

        public void SetLastLoggedAs(Guid? id)
        {
            try
            {
                repository.Update(Constants.LAST_LOGGED_KEY, id.ToString());
            } catch { }
        }
    }
}
