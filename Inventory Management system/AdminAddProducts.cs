using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Inventory_Management_system
{
    public partial class AdminAddProducts : UserControl
    {
        SqlConnection 
            connect = new SqlConnection("Data Source=LAPTOP-FI1733H9\\SQLEXPRESS;Initial Catalog=InventoryMgtSystem;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
        public void showAll()
        {
            displayCategories();
            displayAllProducts();
        }
        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }
            displayCategories();
            displayAllProducts();
        }
        public AdminAddProducts()
        {
            InitializeComponent();

            showAll();
            
        }
        public void displayAllProducts()
        {
            //AddProductsData apData = new AddProductsData();
            //List<AddProductsData> listData = apData.AllProductsData();+

            //if (checkConnection())
            {
                connect.Close();
                connect.Open();
                SqlDataAdapter adp = new SqlDataAdapter("select * from products1",connect);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                dataGridView1.DataSource = dt;
            }  
        }
        public bool emptyFields()
        {
            if (addProducts_productID.Text == "" || addProducts_productName.Text == "" || comboBox1.SelectedIndex == -1
               || addProducts_price.Text == "" || addProducts_stock.Text == "" || comboBox3.SelectedIndex == -1
               || addProducts_imageView.Image == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void displayCategories()
        {
            if (checkConnection())
            {
                try
                {
                    connect.Close ();
                    connect.Open();

                    string selectData = "SELECT * FROM categories";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                comboBox1.Items.Add(reader["category"].ToString()); 
                            }
                        }
                    }
                } 
                catch (Exception ex)
                {

                }
                finally
                {
                    connect.Close();
                }
            }
        }
        private void Add_Click(object sender, EventArgs e)
        {
            if(emptyFields())
            {
                MessageBox.Show("Empty Fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //if (checkConnection())
                //{
                //  connect.Close() ;
                //}               
                
                  try
                  {
                    connect.Close();
                        connect.Open();

                        string selectData = "SELECT * FROM products1 WHERE prod_id = @prodID";

                        using (SqlCommand cmd = new SqlCommand(selectData, connect))
                        {
                            cmd.Parameters.AddWithValue("@prodID", addProducts_productID.Text.Trim());

                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            DataTable table = new DataTable();

                            adapter.Fill(table);
                            if (table.Rows.Count > 0)
                            {
                                MessageBox.Show("Product ID: " + addProducts_productID.Text.Trim() + " is exiting already",
                                    "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

                                string relativePath = Path.Combine("Product_Directory", addProducts_productID.Text.Trim() + ".jpg");
                                string path = Path.Combine(baseDirectory, relativePath);

                                string directoryPath = Path.GetDirectoryName(path);

                                if (!Directory.Exists(directoryPath))// if not exist
                                {
                                    Directory.CreateDirectory(directoryPath);
                                }
                                File.Copy(addProducts_imageView.ImageLocation, path, true);

                                string insertData = "INSERT INTO products1 (prod_id, prod_name, category, price, stock, image_patch, status, date_insert)" +
                                    "VALUES(@prodID, @prodName, @cat, @price, @stock, @path, @status, @date)";

                                using (SqlCommand insertD = new SqlCommand(insertData, connect))
                                {
                                    insertD.Parameters.AddWithValue("@prodID", addProducts_productID.Text.Trim());
                                    insertD.Parameters.AddWithValue("@prodName", addProducts_productName.Text.Trim());
                                    insertD.Parameters.AddWithValue("@cat", comboBox1.SelectedItem);
                                    insertD.Parameters.AddWithValue("@price", addProducts_price.Text.Trim());
                                    insertD.Parameters.AddWithValue("@stock", addProducts_stock.Text.Trim());
                                    insertD.Parameters.AddWithValue("@path", path);
                                    insertD.Parameters.AddWithValue("@status", comboBox3.SelectedItem);

                                    DateTime today = DateTime.Today;
                                    insertD.Parameters.AddWithValue("@date", today);

                                    insertD.ExecuteNonQuery();
                                    clearfields();
                                    displayAllProducts();

                                    MessageBox.Show("Added successfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                  }
                  catch (Exception ex)
                  {
                      MessageBox.Show("Failed connection: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  }
                  finally
                  {
                      connect.Close();
                  }
                  showAll(); 
            }  
        }
        public bool checkConnection()
        {
            if (connect.State != ConnectionState.Open)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void addProducts_importBtn_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image Files (*.jpg; *.png)|*.jpg;*.png";
                string imagePath = "";

                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath = dialog.FileName;
                    
                    addProducts_imageView.ImageLocation = imagePath;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error:" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        public void clearfields()
        {
            addProducts_productID.Text = "";
            addProducts_productName.Text = "";
            comboBox1.SelectedIndex = -1;
            addProducts_price.Text = "";
            addProducts_stock.Text = "";
            comboBox3.SelectedIndex = -1;
            addProducts_imageView.Image = null;
        }
        private void addProducts_clearBtn_Click(object sender, EventArgs e)
        {
            clearfields();
        }

        private void addProducts_updateBtn_Click(object sender, EventArgs e)
        {
            if (emptyFields())
            {
                MessageBox.Show("Empty Fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to Update Product ID: "
                    + addProducts_productID.Text.Trim() + "?", "Confirmation Message", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                   
                        try
                        {
                            connect.Close();
                            connect.Open();

                            string updateData = "UPDATE products1 SET prod_id = @prodID, prod_name = @prodName" +
                                ", category = @cat, price = @price, stock = @stock, status = @status WHERE id = @id";

                            using (SqlCommand updateD = new SqlCommand(updateData, connect))
                            {
                                updateD.Parameters.AddWithValue("@prodID", addProducts_productID.Text.Trim());
                                updateD.Parameters.AddWithValue("@prodName", addProducts_productName.Text.Trim());
                                updateD.Parameters.AddWithValue("@cat", comboBox1.SelectedItem);
                                updateD.Parameters.AddWithValue("price", addProducts_price.Text.Trim());
                                updateD.Parameters.AddWithValue("@stock", addProducts_stock.Text.Trim());
                                updateD.Parameters.AddWithValue("@status", comboBox3.SelectedItem);
                                updateD.Parameters.AddWithValue("@id", getID);

                                updateD.ExecuteNonQuery();
                                clearfields();
                                displayAllProducts();

                                MessageBox.Show("Updated successfully..!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Failed connection:" + ex, "Error Message", MessageBoxButtons.OK,MessageBoxIcon.Error);
                        }
                        finally
                        {
                            connect.Close();
                        }
                }
            }
        }

        private int getID = 0;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                getID = (int)row.Cells[0].Value;

                addProducts_productID.Text = row.Cells[1].Value.ToString();
                addProducts_productName.Text = row.Cells[2].Value.ToString();
                comboBox1.Text = row.Cells[3].Value.ToString();
                addProducts_price.Text = row.Cells[4].Value.ToString();
                addProducts_stock.Text = row.Cells[5].Value.ToString();

                string imagepath = row.Cells[6].Value.ToString();
                try
                {
                    if(imagepath != null)
                    {
                        addProducts_imageView.Image = Image.FromFile(imagepath);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Image:" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                comboBox3.Text = row.Cells[7].Value.ToString();
            }
        }

        private void addProducts_removeBtn_Click(object sender, EventArgs e)
        {
            if (emptyFields())
            {
                MessageBox.Show("Empty Fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
            else
            {
                    try
                    {
                        connect.Close();
                        connect.Open();

                        string deleteData = "DELETE FROM products1 WHERE id =@id";

                        using (SqlCommand deleteD = new SqlCommand(deleteData, connect))
                        {
                            deleteD.Parameters.AddWithValue("@id", getID);

                            deleteD.ExecuteNonQuery();
                            clearfields();
                            displayAllProducts();

                            MessageBox.Show("Deleted successfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed connection: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect?.Close();
                    }
            }    
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}