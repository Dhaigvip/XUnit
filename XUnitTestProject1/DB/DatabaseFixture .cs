using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace XUnitTestProject1.DB
{
    public class DatabaseFixture
    {
        SqlConnection connection;
        int fooUserID;

        public DatabaseFixture()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=Test;Integrated Security=True";
            connection = new SqlConnection(connectionString);
            connection.Open();

            string sql = @"INSERT INTO User1 VALUES ('Rahul', 'Dhaigude'); SELECT SCOPE_IDENTITY();";

            using (SqlCommand cmd = new SqlCommand(sql, connection))
                fooUserID = Convert.ToInt32(cmd.ExecuteScalar());
        }

        public SqlConnection Connection
        {
            get { return connection; }
        }

        public int FooUserID
        {
            get { return fooUserID; }
        }

        public void Dispose()
        {
            string sql = @"DELETE FROM User1 WHERE ID = @id;";

            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@id", fooUserID);
                cmd.ExecuteNonQuery();
            }

            connection.Close();
        }
    }
}
