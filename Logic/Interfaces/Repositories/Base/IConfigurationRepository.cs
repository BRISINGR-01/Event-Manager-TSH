namespace Logic.Interfaces.Repositories
{
    public interface IConfigurationRepository
    {
        public string? Read(string key);
        public bool Update(string key, string? value);
    }
}
