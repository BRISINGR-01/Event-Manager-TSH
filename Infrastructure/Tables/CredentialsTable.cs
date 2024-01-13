namespace Infrastructure.Tables
{
    public static class CredentialsTable
    {
        public static readonly string TableName = "credentials";
        public static readonly string Id = "user_id";
        public static readonly string Email = "user_identification";
        public static readonly string PasswordHash = "password_hash";
        public static readonly string Salt = "salt";
    }
}
