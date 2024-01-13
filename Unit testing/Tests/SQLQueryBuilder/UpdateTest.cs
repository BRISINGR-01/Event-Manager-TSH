using Mocks.SQL;
using SQL_Query_Builder;

namespace Unit_testing.Tests.SQLQueryBuilder
{
    [TestClass]
    public class UpdateTest : Base
    {
        private Update update => new(builderCommandMock);

        [TestMethod]
        public void Update()
        {
            var query = $"UPDATE {stringCommandMock.TableName} SET {MockEntityTable.Id} = @{MockEntityTable.Id}, {MockEntityTable.Name} = @{MockEntityTable.Name} WHERE {MockEntityTable.Id} = @param1";

            update
                .Set(MockEntityTable.Id, null)
                .Set(MockEntityTable.Name, null)
                .Where(MockEntityTable.Id).Equals(null)
                .Execute();

            AssertQuery(query);
        }
        [TestMethod]
        public void MustNotBeSelect()
        {
            Assert.ThrowsException<SQLQueryBuilderException>(() =>
             update
               .Where(MockEntityTable.Id).Equals(null)
               .FinishSelect.Count
            );
        }
    }
}
