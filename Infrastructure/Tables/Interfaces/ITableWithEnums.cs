using Logic.Interfaces;

namespace Infrastructure.Tables.Interfaces
{
    public interface ITableWithEnums : ITableWithId
    {
        string EnumToSQLValue(Enum role);
    }
}
