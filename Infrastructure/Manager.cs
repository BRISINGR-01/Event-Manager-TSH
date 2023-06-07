using Domain.Managers;
using Infrastructure.DatabaseManagers;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Events;
using Logic;
using Logic.Models;
using Shared.Errors;
using System.Collections;
using System.Configuration;

namespace Infrastructure
{
    public class Manager
    {
        private readonly IdentityUser _user;
        private readonly string connectionString;
        private static string OfflineConnectionString
        {
            get
            {
                try
                {
                    ConnectionStringSettings? settings = ConfigurationManager.ConnectionStrings["Local"] ?? throw DeveloperExceptions.CannotFindDatabaseCredentials;
                    return settings.ConnectionString;
                }
                catch
                {
                    throw DeveloperExceptions.CannotFindDatabaseCredentials;
                }
            }
        }
        private static string OnlineConnectionString
        {
            get
            {
                try
                {
                    ConnectionStringSettings? settings = ConfigurationManager.ConnectionStrings["Online"] ?? throw DeveloperExceptions.CannotFindDatabaseCredentials;
                    return settings.ConnectionString;
                }
                catch
                {
                    throw DeveloperExceptions.CannotFindDatabaseCredentials;
                }
            }
        }
        public Manager(IdentityUser user, string? connectionString = null)
        {
            this.connectionString = connectionString ?? OnlineConnectionString;
            _user = user;
            // test that it can connect
            _ = new UserManager(new UserRepository(this.connectionString, _user.BranchId), _user);
        }
        // Managers
        public UserManager User { get => new(new UserRepository(connectionString, _user.BranchId), _user); }
        public EventManager Event { get => new(new EventRepository(connectionString, _user.BranchId), new ParticipanceRepository(connectionString, _user.BranchId), _user); }
        public BranchManager Branch { get => new(new BranchRepository(connectionString, _user.BranchId), _user); }
        public ImageManager Image { get => new(new ImageRepository(connectionString, _user.BranchId), _user); }
        public static LocalDataManager Local { get => new(new LocalRepository()); }

        // static methods that don't need authorization
        public static Result<User?> UserGet(Guid id) => UserManager.Get(new UserRepository(OnlineConnectionString, null), id);
        public static Result<User?> CheckCredentials(string email, string password) => UserManager.CheckCredentials(new UserRepository(OnlineConnectionString, null), email, password);
    
        // Encryption
        public static string? Encrypt(string text) => new Encryption().Encrypt(text);
        public static string? Decrypt(string? encrypted) => new Encryption().Decrypt(encrypted);
    }
}
