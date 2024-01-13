using Logic.Interfaces.Repositories;
using Logic.Managers;
using Logic.Models;
using Logic.Utilities;
using Shared.Enums;

namespace Domain.Managers
{
    public class UserManager : BaseDbManager<User>
    {
        private readonly IUserRepository _repository;
        private readonly ICredentialsRepository credentialsRepository;
        public UserManager(IUserRepository repository, ICredentialsRepository credentialsRepository) : base(repository)
        {
            _repository = repository;
            this.credentialsRepository = credentialsRepository;
        }
        public Result<List<User>> GetBranchContacts(Guid branchId)
        {
            return Result<List<User>>.From(() => _repository.GetBranchContacts(branchId));
        }
        public Result<List<User>> GetAllFromBranch(Guid? branchId = null)
        {
            return Result<List<User>>.From(() => _repository.GetAllFromBranch(branchId));
        }

        public override Result Create(User user)
        {
            return Result.From(() =>
           {
               user.Validate();
               if (!_repository.Create(user))
               {
                   credentialsRepository.Delete(user.Id);
                   throw new Exception("Couldn't create user");
               }
           });
        }
        public Result Create(User user, Credentials credentials)
        {
            var getByEmail = Result<Credentials>.From(() => credentialsRepository.GetByEmail(credentials.Email));
            if (getByEmail.IsSuccessful)
            {
                return Result.FailWith("A user with this email already exists");
            }

            var res = Result.From(() => credentialsRepository.Create(credentials));

            return res.IsSuccessful ? Create(user) : res;
        }
        public Result<List<User>> GetByRoles(List<UserRole> roles, Guid branchId)
        {
            if (roles.Count == 0) return Result<List<User>>.From(() => new());

            return Result<List<User>>.From(() => _repository.GetByRoles(roles, branchId));
        }
        public Result<List<PushNotificationSubscription>> GetSubscriptions(List<UserRole> roles, Guid branchId)
        {
            if (roles.Count == 0) return Result<List<PushNotificationSubscription>>.From(() => new());

            return Result<List<PushNotificationSubscription>>.From(() => _repository.GetSubscriptions(roles, branchId));
        }
        public Result Subscribe(PushNotificationSubscription subscription)
        {
            return Result.From(() => _repository.Subscribe(subscription));
        }
    }
}