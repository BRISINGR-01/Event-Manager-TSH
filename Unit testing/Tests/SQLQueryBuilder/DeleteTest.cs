using Mocks.SQL;
using SQL_Query_Builder;

namespace Unit_testing.Tests.SQLQueryBuilder
{
    [TestClass]
    public class DeleteTest : Base
    {
        private Delete delete => new(builderCommandMock);

        [TestMethod]
        public void Delete()
        {
            var query = $"DELETE FROM {stringCommandMock.TableName} WHERE {MockEntityTable.Id} = @param1";

            delete
                .Where(MockEntityTable.Id).Equals(null)
                .Execute();

            AssertQuery(query);
        }
        [TestMethod]
        public void MustNotBeSelect()
        {
            Assert.ThrowsException<SQLQueryBuilderException>(() =>
             delete
               .Where(MockEntityTable.Id).Equals(null)
               .FinishSelect.Count
            );
        }
    }
}
