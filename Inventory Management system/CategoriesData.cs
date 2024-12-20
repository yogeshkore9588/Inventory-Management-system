using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Inventory_Management_system
{
    internal class CategoriesData
    {
        public int ID { get; set; }
        public string Category { get; set; }
        public string Date { get; set; }
        public List<CategoriesData> AllCategoriesData()
        {
            List<CategoriesData> listData = new List<CategoriesData>();

            SqlConnection connect
                = new SqlConnection("Data Source=LAPTOP-FI1733H9\\SQLEXPRESS;Initial Catalog=InventoryMgtSystem;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
            {

                connect.Open();
                string selectData = "SELECT * FROM categories";

                using (SqlCommand cmd = new SqlCommand(selectData, connect))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CategoriesData cData = new CategoriesData();
                        cData.ID = (int)reader["id"];
                        cData.Category = reader["category"].ToString();
                        cData.Date = reader["date"].ToString();
                        listData.Add(cData);
                    }
                }
            }
            return listData;
        }
    }
}
