using Infrastructure.Tables;
using Logic.Interfaces.Repositories;
using Logic.Models;
using Logic.Utilities;
using Shared;
using Shared.Enums;
using SQL_Query_Builder.Where;

namespace Infrastructure.Repositories
{
    public class UserRepository : DatabaseRepository, IUserRepository
    {
        public UserRepository(DatabaseManager db) : base(db, UserTable.TableName) { }
        public User? GetById(Guid id)
        {
            return sql.Select
                .All
                .Where(UserTable.Id).Equals(id)
                .FinishSelect
                .First<User>();
        }
        public List<User> GetAll(int? offsetIndex = null)
        {
            return sql.Select
                .All
                .OrderBy(UserTable.UserName)
                .Get<User>();
        }
        public bool Create(User user)
        {
            return sql.Insert
                .Set(UserTable.Id, Helpers.NewId)
                .Set(UserTable.BranchId, user.BranchId)
                .Set(UserTable.UserName, Encryption.Encrypt(user.UserName))
                .Set(UserTable.Role, user.Role)
                .Execute();
        }
        public bool Update(User user)
        {
            return sql.Update
                .Set(UserTable.UserName, Encryption.Encrypt(user.UserName))
                .Set(UserTable.Role, user.Role)
                .Where(UserTable.Id).Equals(user.Id)
                .Execute();
        }
        public bool Delete(Guid id)
        {
            return
                sql.Delete
                .Where(UserTable.Id).Equals(id)
                .Execute();
        }
        public List<User> GetBranchContacts(Guid branchId)
        {
            return sql.Select
                .All
                .Where(UserTable.Role).Equals(UserRole.EventOrganizer)
                .Or
                .Where(UserTable.Role).Equals(UserRole.StudentComitee)
                .And
                .Where(UserTable.BranchId).Equals(branchId)
                .FinishSelect
                .OrderBy(UserTable.UserName)
                .Get<User>();
        }
        public List<User> GetAllFromBranch(Guid? branchId = null)
        {
            if (branchId == null)
            {
                return sql.Select
                 .All
                 .OrderBy(UserTable.UserName)
                 .Get<User>();
            }
            else
            {
                return sql.Select
                    .All
                    .Where(UserTable.BranchId).Equals(branchId)
                    .FinishSelect
                    .OrderBy(UserTable.UserName)
                    .Get<User>();
            }
        }

        public List<User> GetByRoles(List<UserRole> roles, Guid branchId)
        {
            var statement = sql.Select
                .All
                .Where(UserTable.BranchId).Equals(branchId);

            foreach (var role in roles)
            {
                statement.Or.Where(UserTable.Role).Equals(role);
            }

            return statement.FinishSelect.Get<User>();
        }
        public List<PushNotificationSubscription> GetSubscriptions(List<UserRole> roles, Guid branchId)
        {
            var statement = sql.Select
                .All
                .Join(PushNotificationSubscriptionTable.TableName)
                .OnColumns(UserTable.Id, PushNotificationSubscriptionTable.UserId)
                .Where(UserTable.BranchId).Equals(branchId);


            for (int i = 0; i < roles.Count; i++)
            {
                ICondition condition;
                if (i == 0)
                {
                    condition = statement.And;
                }
                else
                {
                    condition = statement.Or;
                }
                condition.Where(UserTable.Role).Equals(roles[i]);
            }

            return statement.FinishSelect.Get<PushNotificationSubscription>();
        }
        public bool Subscribe(PushNotificationSubscription subscription)
        {
            var exists = sql
                .FromTable(PushNotificationSubscriptionTable.TableName)
                .Select
                .All
                .Where(PushNotificationSubscriptionTable.UserId).Equals(subscription.UserId)
                .FinishSelect
                .First<PushNotificationSubscription>() != null;

            if (exists)
            {
                return sql
                        .FromTable(PushNotificationSubscriptionTable.TableName)
                        .Update
                        .Set(PushNotificationSubscriptionTable.Endpoint, subscription.Endpoint)
                        .Set(PushNotificationSubscriptionTable.P256DH, subscription.P256DH)
                        .Set(PushNotificationSubscriptionTable.Auth, subscription.Auth)
                        .Where(PushNotificationSubscriptionTable.UserId).Equals(subscription.UserId)
                        .Execute();
            }
            else
            {
                return sql
                        .FromTable(PushNotificationSubscriptionTable.TableName)
                        .Insert
                        .Set(PushNotificationSubscriptionTable.UserId, subscription.UserId)
                        .Set(PushNotificationSubscriptionTable.Endpoint, subscription.Endpoint)
                        .Set(PushNotificationSubscriptionTable.P256DH, subscription.P256DH)
                        .Set(PushNotificationSubscriptionTable.Auth, subscription.Auth)
                        .Execute();
            }
        }
    }
}
