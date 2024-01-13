using SQL_Query_Builder.Interfaces;

namespace Mocks.SQL
{
    public class MockEntityFactory : IEntityFactory
    {
        public T Create<T>(IDbDataReader reader)
        {
            return (T)(object)new MockEntity();
        }
    }
}
