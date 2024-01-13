using Mocks.SQL;

namespace Unit_testing.Tests.SQLQueryBuilder
{
    [TestClass]
    public abstract class Base
    {
        protected CommandMock stringCommandMock = new();
        protected CommandMock builderCommandMock = new();
        [TestInitialize]
        public void Initialize()
        {
            stringCommandMock = new();
            builderCommandMock = new();
        }
        protected void AssertQuery(string expected)
        {
            Assert.AreEqual(builderCommandMock.CommandText, expected);
        }
    }
}
