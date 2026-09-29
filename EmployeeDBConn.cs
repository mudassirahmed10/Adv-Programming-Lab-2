using System.Data;
using Microsoft.Data.SqlClient;

namespace lab2CrudOperation
{
    // Ye class database se baat karti hai (Insert, Update, Delete, View)
    internal class EmployeeDBConn
    {
        // Connection string sirf aik dafa likhni parti hai
        private string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Employeedb;Integrated Security=True;TrustServerCertificate=True;";

        // INSERT: naya employee add karta hai
        public bool Insert(string id, string name, string cell, string address)
        {
            string query = "INSERT INTO employeeTable (Id, Name, Cell, Address) VALUES (@Id, @Name, @Cell, @Address)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Cell", cell);
                cmd.Parameters.AddWithValue("@Address", address);

                con.Open();
                int rows = cmd.ExecuteNonQuery();   // kitni rows par asar hua
                return rows > 0;                    // agar aik bhi row add hui to true
            }
        }

        // UPDATE: puranay employee ka data badalta hai
        public bool Update(string id, string name, string cell, string address)
        {
            string query = "UPDATE employeeTable SET Name = @Name, Cell = @Cell, Address = @Address WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Cell", cell);
                cmd.Parameters.AddWithValue("@Address", address);

                con.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }

        // DELETE: employee ko Id se delete karta hai
        public bool Delete(string id)
        {
            string query = "DELETE FROM employeeTable WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }



        // SEARCH: Id ke zarye employee search karta hai
        public DataTable SearchById(string id)
        {
            string query = "SELECT Id, Name, Cell, Address FROM employeeTable WHERE Id = @Id";
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
            }

            return dt;
        }

        // VIEW: saaray employees ka data DataTable mein laata hai
        public DataTable FetchAll()
        {
            string query = "SELECT Id, Name, Cell, Address FROM employeeTable";
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
            }

            return dt;
        }
    }
}
