using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Xunit;

namespace XUnitTestProject1.DB
{
    public class ClassFixtureTests : IClassFixture<DatabaseFixture>
    {
        DatabaseFixture database;
        public ClassFixtureTests(DatabaseFixture data)
        {
            database = data;
        }
        [Fact]
        public void ConnectionIsEstablished()
        {
            Assert.NotNull(database.Connection);
        }

        [Fact]
        public void FooUserWasInserted()
        {
            string sql = "SELECT COUNT(*) FROM User1 WHERE ID = @id;";

            using (SqlCommand cmd = new SqlCommand(sql, database.Connection))
            {
                cmd.Parameters.AddWithValue("@id", database.FooUserID);

                int rowCount = Convert.ToInt32(cmd.ExecuteScalar());

                Assert.Equal(1, rowCount);
            }
        }
    }
}
