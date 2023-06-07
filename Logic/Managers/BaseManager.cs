using Logic.Interfaces;
using Logic.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Models;
using Shared.Errors;
using Shared.Enums;
using Logic.Models.Events;

namespace Logic.Managers
{
    public abstract class BaseManager<T, TRepo> where T : IEntity where TRepo : IRepository<T>
    {
        private readonly TRepo repository;
        protected readonly IdentityUser _user;
        protected BaseManager(TRepo repository, IdentityUser user)
        {
            _user = user;
            this.repository = repository;
        }
        protected TRepo VerifiedRepository(UserRole? roles = null)
        {
            if (roles == null) return repository;

            if (_user == null || _user.BranchId == Guid.Empty) throw new AccessDeniedException();

            bool hasPermissions = (_user.Role & roles) == _user.Role;

            if (!hasPermissions) throw new AccessDeniedException();

            return repository;
        }

        public Result<List<T>> GetAll(int? offsetIndex = null)
        {
            return Result<List<T>>.From(() => VerifiedRepository().GetAll(offsetIndex));
        }
        virtual public Result Create(T entity)
        {
            var res = Result<bool>.From(() => VerifiedRepository(UserRole.Administrator).Create(entity), CRUD.CREATE, typeof(T).Name.ToLower());

            return res.IsSuccessful && res.Value ? Result.Success : res.Fail;
        }
        public Result Update(T entity)
        {
            var res = Result<bool>.From(() => VerifiedRepository(UserRole.EventOrganizer).Update(entity), CRUD.UPDATE, typeof(T).Name.ToLower());

            return res.IsSuccessful && res.Value ? Result.Success : res.Fail;
        }
        public Result Delete(Guid id)
        {
            var res = Result<bool>.From(() => VerifiedRepository(typeof(T) == typeof(Event) ? UserRole.EventOrganizer : UserRole.Administrator).Delete(id), CRUD.DELETE, typeof(T).Name.ToLower());

            return res.IsSuccessful && res.Value ? Result.Success : res.Fail;
        }
    }
}
