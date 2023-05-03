using azuresqlapp.Models;
using System.Data.SqlClient;

namespace azuresqlapp.Services
{
    public class ProductService
    {
        private static string db_source = "demo-server12.database.windows.net";
        private static string db_user = "Admin1234";
        private static string db_password = "Admin@123456789";
        private static string db_database = "demo-cluster";

        private SqlConnection GetConnection()
        {
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = db_source;
            builder.UserID = db_user;
            builder.Password = db_password;
            builder.InitialCatalog = db_database;
            return new SqlConnection(builder.ConnectionString);
        }

        public List<Product> GetProducts()
        {
            SqlConnection conn = GetConnection();

            List<Product> _product_lst = new List<Models.Product>();

            string statement = "SELECT ProductId, ProductName, Quantity from Products";

            conn.Open();

            SqlCommand cmd = new SqlCommand(statement, conn);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductId = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2)
                    };

                    _product_lst.Add(product);
                }
            }
            conn.Close();
            return _product_lst;

        }
    }
};
