using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductViewControl
{
    class DataAccess
    {
        public static ProductModel GetProductModel(int productModelId)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AdventureWorks2016;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = $@"SELECT DISTINCT ProductModel.ProductModelID, ProductModel.Name, Product.ListPrice, ProductPhoto.LargePhoto
                                FROM Production.ProductModel
                                JOIN Production.Product ON ProductModel.ProductModelID = Product.ProductModelID
                                 JOIN Production.ProductProductPhoto ON Product.ProductID = ProductProductPhoto.ProductID
                                JOIN Production.ProductPhoto ON ProductProductPhoto.ProductPhotoID = ProductPhoto.ProductPhotoID
                                WHERE Product.ProductModelID = {productModelId}";
                var productModel = conn.Query<ProductModel>(sql).FirstOrDefault();
                return productModel;
            }
        }

        public static List<Product> GetProducts(int productModelId)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AdventureWorks2016;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = $@"SELECT ProductId, Size FROM Production.Product 
                                WHERE Product.ProductModelID = {productModelId}
                                ORDER BY Size";
                var products = conn.Query<Product>(sql).ToList();

                List<Product> result = new List<Product>();

                foreach (var product in products)
                {
                    bool found = false;
                    foreach (var p in result)
                    {
                        if(p.Size == product.Size)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        result.Add(product);
                    }
                }

                return result;
            }
            
        }

        public static List<int> GetProductsModelIDs()
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AdventureWorks2016;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT DISTINCT ProductModelID FROM Production.Product" +
                    " WHERE ProductModelID IS NOT NULL";
                var ids = conn.Query<int>(sql).ToList();
                return ids;
            }

        }
    }
}
