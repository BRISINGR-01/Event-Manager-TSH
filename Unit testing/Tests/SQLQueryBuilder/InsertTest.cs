﻿using Mocks.SQL;
using SQL_Query_Builder;

namespace Unit_testing.Tests.SQLQueryBuilder
{
    [TestClass]
    public class InsertTest : Base
    {
        private Insert insert => new(builderCommandMock);
        [TestMethod]
        public void Insert()
        {

            var query = $"INSERT INTO {stringCommandMock.TableName} ({MockEntityTable.Id}, {MockEntityTable.Name}, {MockEntityTable.Description}) VALUES (@{MockEntityTable.Id}, @{MockEntityTable.Name}, @{MockEntityTable.Description})";

            insert
                .Set(MockEntityTable.Id, null)
                .Set(MockEntityTable.Name, null)
                .Set(MockEntityTable.Description, null)
                .Execute();

            AssertQuery(query);
        }
    }
}
