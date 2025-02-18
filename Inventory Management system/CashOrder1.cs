﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace Inventory_Management_system
{
    public partial class CashOrder1 : UserControl
    {
        SqlConnection connect
            = new SqlConnection("Data Source=LAPTOP-FI1733H9\\SQLEXPRESS;Initial Catalog=InventoryMgtSystem;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
        public CashOrder1()
        {
            InitializeComponent();

            displayAllAvailableProducts();
            displayAllCategories();
            displayOrders();
            displayTotalPrice();
            refreshData();
        }
        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }

            displayAllAvailableProducts();
            displayAllCategories();
            displayOrders();
            displayTotalPrice();
        }

        public void displayAllAvailableProducts()
        {
            AddProductsData apData = new AddProductsData();
            List<AddProductsData> listData = apData.AllProductsData();

            dataGridView1.DataSource = listData;
        }

        public void displayOrders()
        {
            OrdersData oData = new OrdersData();
            List<OrdersData> listData = oData.allOrdersData();

            dataGridView2.DataSource = listData;
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = cashierOrder_prodID.SelectedItem as string;

            //if (checkConnection())
            {
                if (selectedValue != null)
                {
                    try
                    {
                        connect.Close();
                        connect.Open();

                        string selectData = $"SELECT * FROM products1 WHERE prod_id = '{selectedValue}' AND status = @status";

                        using (SqlCommand cmd = new SqlCommand(selectData, connect))
                        {
                            cmd.Parameters.AddWithValue("@status", "Available");

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string prodName = reader["prod_name"].ToString();
                                    float prodPrice = Convert.ToSingle(reader["price"]);

                                    cashierOrder_prodName.Text = prodName;
                                    cashierOrder_price.Text = prodPrice.ToString("0.00");

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
                }
            }
        }
        public bool checkConnection()
        {
            if (connect.State == ConnectionState.Closed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void displayAllCategories()
        {
            //if(checkConnection())
            {
                try
                {
                    connect.Close();
                    connect.Open();

                    string selectData = "SELECT * FROM categories ";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            cashierOrder_category.Items.Clear();

                            while (reader.Read())
                            {
                                string item = reader.GetString(1);
                                cashierOrder_category.Items.Add(item);
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
            }
        }
        private void cashierOrder_category_SelectedIndexChanged(object sender, EventArgs e)
        {
            cashierOrder_prodID.SelectedIndex = -1;
            cashierOrder_prodID.Items.Clear();
            cashierOrder_prodName.Text = "";
            cashierOrder_price.Text = "";
            //cashierOrder_qty.Value = 0;


            string selectedValue = cashierOrder_category.SelectedItem as string;

            if (selectedValue != null)
            {
                //if (checkConnection())
                {
                    try
                    {
                        connect.Close();
                        connect.Open();

                        string selectData = $"SELECT * FROM products1 WHERE category = '{selectedValue}' AND status = @status";

                        using (SqlCommand cmd = new SqlCommand(selectData, connect))
                        {
                            cmd.Parameters.AddWithValue("@status", "Available");

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string value = reader["prod_id"].ToString();
                                    cashierOrder_prodID.Items.Add(value);
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
                }
            }
        }
        private float totalPrice = 0;
        public void displayTotalPrice()
        {
            IDGenerator();

            //if(checkConnection())
            {
                try
                {
                    connect.Close();
                    connect.Open();

                    string selectData = "SELECT SUM(total_price) FROM orders1 WHERE customer_id = @cID";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        cmd.Parameters.AddWithValue("@cID", idGen);

                        object result = cmd.ExecuteScalar();

                        if (result != DBNull.Value)
                        {
                            totalPrice = Convert.ToSingle(result);

                            cashierOrder_totalPrice.Text = totalPrice.ToString("0.00");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection failed: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }
        private void cashierOrder_addBtn_Click(object sender, EventArgs e)
        {
            IDGenerator();

            if (cashierOrder_category.SelectedIndex == -1 || cashierOrder_prodID.SelectedIndex == -1 ||
                cashierOrder_prodName.Text == "" || cashierOrder_price.Text == "" || cashierOrder_qty.Value == 0)
            {
                MessageBox.Show("", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //  if (checkConnection())
                {
                    try
                    {
                        connect.Close();
                        connect.Open();

                        float getPrice = 0;
                        string selectOrder = "SELECT * FROM products1 WHERE prod_id = @prodID";

                        using (SqlCommand getOrder = new SqlCommand(selectOrder, connect))
                        {
                            getOrder.Parameters.AddWithValue("@prodID", cashierOrder_prodID.SelectedItem);

                            using (SqlDataReader reader = getOrder.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    object rawValue = reader["price"];

                                    if (rawValue != DBNull.Value)
                                    {
                                        getPrice = Convert.ToSingle(rawValue);
                                    }
                                }
                            }
                        }

                        string insertData = $"INSERT INTO orders1 (customer_id, prod_id,prod_name,category, qty,orig_price,total_price,order_date)" +
                                                            $"VALUES(@cID, @prodID, @prodName, @cat, @qty, @origPrice, @totalprice, @date)";

                        /* string insertData = "INSERT INTO orders1 (customer_id, prod_id,prod_name,category, qty,orig_price,total_price,order_date)" +
                             "VALUES(@cID, @prodID, @prodName, @cat, @qty, @origPrice, @totalprice, @date)";
                        */
                        using (SqlCommand cmd = new SqlCommand(insertData, connect))
                        {
                            cmd.Parameters.AddWithValue("@cID", idGen);
                            cmd.Parameters.AddWithValue("prodID", cashierOrder_prodID.SelectedItem);
                            cmd.Parameters.AddWithValue("@prodName", cashierOrder_prodName.Text.Trim());
                            cmd.Parameters.AddWithValue("@cat", cashierOrder_category.SelectedItem);
                            cmd.Parameters.AddWithValue("@qty", cashierOrder_qty.Value);
                            cmd.Parameters.AddWithValue("@origPrice", getPrice);

                            float totalIP = (getPrice * (int)cashierOrder_qty.Value);

                            cmd.Parameters.AddWithValue("@totalprice", totalIP);

                            DateTime today = DateTime.Today;
                            cmd.Parameters.AddWithValue("@date", today);

                            int n = cmd.ExecuteNonQuery();
                            if (n > 0)
                            {
                                MessageBox.Show("order placed successfully..");
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
                }
            }
            displayOrders();
            displayTotalPrice();
        }

        private int idGen;
        public void IDGenerator()
        {
            using (SqlConnection connect
            = new SqlConnection("Data Source=LAPTOP-FI1733H9\\SQLEXPRESS;Initial Catalog=InventoryMgtSystem;Integrated Security=True;Encrypt=True;TrustServerCertificate=True")) ;
            {
                connect.Close();
                connect.Open();

                string selectData = "SELECT MAX(customer_id)FROM customers";

                using (SqlCommand cmd = new SqlCommand(selectData, connect))
                {
                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        int temp = Convert.ToInt32(result);

                        if (temp == 0)
                        {
                            idGen = 1;
                        }
                        else
                        {
                            idGen = temp + 1;
                        }
                    }
                    else
                    {
                        idGen = 1;
                    }
                }
            }
            connect.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cashierOrder_removeBtn_Click(object sender, EventArgs e)
        {
            if (prodID == 0)
            {
                MessageBox.Show("Please select item first", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to Remove ID: " + prodID + "?", "Confirmation Message",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //if (checkConnection())
                    {
                        try
                        {
                            connect.Close();
                            connect.Open();

                            string deleteData = "DELETE  FROM orders1 WHERE id = @id";

                            using (SqlCommand cmd = new SqlCommand(deleteData, connect))
                            {
                                cmd.Parameters.AddWithValue("@id", prodID);
                                cmd.ExecuteNonQuery();

                                MessageBox.Show("Removed Successfully..!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    }
                }
            }

            displayOrders();
            displayTotalPrice();
        }

        private int prodID = 0;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];

                prodID = (int)row.Cells[0].Value;
            }
        }

        public void clearFields()
        {
            cashierOrder_category.SelectedIndex = -1;
            cashierOrder_prodID.SelectedIndex = -1;
            cashierOrder_prodName.Text = "";
            cashierOrder_price.Text = "";
            cashierOrder_qty.Value = 0;
        }
        private void cashierOrder_clear_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void cashierOrder_payOrder_Click(object sender, EventArgs e)
        {
            IDGenerator();

            if (cashierOrder_amount.Text == "" || dataGridView2.Rows.Count < 0)
            {
                MessageBox.Show("Something went wrong", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show("Are you sure to pay your orders?",
                    "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //if (checkConnection())
                    {
                        try
                        {
                            connect.Close();
                            connect.Open();

                            string insertData = "INSERT INTO customers (customer_id, total_price, amount, change, order_date)" +
                                "VALUES(@cID, @totalPrice, @amount, @change, @date)";

                            using (SqlCommand cmd = new SqlCommand(insertData, connect))
                            {
                                cmd.Parameters.AddWithValue("@cID", idGen);
                                //cmd.Parameters.AddWithValue("@prodID", cashierOrder_prodID.SelectedItem);
                                cmd.Parameters.AddWithValue("@totalPrice", cashierOrder_totalPrice.Text);
                                cmd.Parameters.AddWithValue("@amount", cashierOrder_amount.Value.ToString());
                                cmd.Parameters.AddWithValue("@change", cashierOrder_change.Text);

                                DateTime today = DateTime.Today;
                                cmd.Parameters.AddWithValue("@date", today);

                                 cmd.ExecuteNonQuery();
                                clearFields();

                                MessageBox.Show("Paid Successfully..!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        catch (Exception ex)
                        { 
                            MessageBox.Show("Connection failed: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            connect.Close();
                        }
                    }
                }
            }
            displayTotalPrice();

            //cashierOrder_amount.Text = "";
            //cashierOrder_change.Text = "";
        }

        private void cashierOrder_amount_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void cashierOrder_change_Click(object sender, EventArgs e)
        {

        }

        private int rowIndex = 0;
        private void cashierOrder_receipt_Click(object sender, EventArgs e)
        {
            if(cashierOrder_amount.Text == "" || dataGridView2.Rows.Count < 0)
            {
                MessageBox.Show("Please order first", "Error Message", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
                printDocument1.BeginPrint += new PrintEventHandler(printDocument1_BeginPrint);

                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();

                cashierOrder_amount.Text = "";
                cashierOrder_change.Text = "";
            }
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            rowIndex = 0;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            displayTotalPrice();

            float y = 0;
            int count = 0;
            int colWidth = 120;
            int headerMargin = 10;
            int tableMargin = 10;//10 20 30

            Font font = new Font("Tahoma", 12);
            Font bold = new Font("tahoma", 12, FontStyle.Bold);
            Font headerFont = new Font("Tahoma", 16, FontStyle.Bold);
            Font labelFont = new Font("Tahoma", 14, FontStyle.Bold);

            float margin = e.MarginBounds.Top;

            StringFormat alignCenter = new StringFormat();
            alignCenter.Alignment = StringAlignment.Center;
            alignCenter.LineAlignment = StringAlignment.Center;

            string headerText = "MarcoMan's Inventory Management System";
            y = (margin + count * headerFont.GetHeight(e.Graphics) + headerMargin);
            e.Graphics.DrawString(headerText, headerFont, Brushes.Black, e.MarginBounds.Left +
                (dataGridView2.Columns.Count / 2) * colWidth, y, alignCenter);


            count++;

            y += tableMargin;

            string[] header = { "ID", "CID", "PID", "PName", "Category", "OrigPrice","QTY", "TotalPrice" };

            for (int q = 0; q < header.Length; q++)
            {
                y = margin + count * bold.GetHeight(e.Graphics) + tableMargin;
                e.Graphics.DrawString(header[q], bold, Brushes.Black, e.MarginBounds.Left + q * colWidth, y, alignCenter);
            }
            count++;

            float rSpace = e.MarginBounds.Bottom - y;

            while(rowIndex < dataGridView2.Rows.Count)
            {
                DataGridViewRow row = dataGridView2.Rows[rowIndex];

                for(int q = 0; q < dataGridView2.Columns.Count; q++)
                {
                    object cellValue = row.Cells[q].Value;
                    string cell = (cellValue != null) ? cellValue.ToString() :string.Empty;

                    y = margin + count * font.GetHeight(e.Graphics) + tableMargin;
                    e.Graphics.DrawString(cell, font, Brushes.Black, e.MarginBounds.Left + q * colWidth, y, alignCenter );
                }
                count++;
                rowIndex++;

                if(y + font.GetHeight(e.Graphics) > e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }
            }

            int labelmargin = (int)Math.Min(rSpace, 200);

            DateTime today = DateTime.Now;  

            float labelX = e.MarginBounds.Right - e.Graphics.MeasureString("------------------", labelFont).Width;
            e.Graphics.DrawString("Total Price: \t$" + totalPrice + "\nAmount: \t$" + cashierOrder_amount.Text.Trim() + 
                "\n\t\t---------------\nChange: \t$" + cashierOrder_change.Text.Trim(), labelFont, Brushes.Black, labelX, y);

            labelmargin = (int)Math.Min(rSpace, -40);

            string labelText = today.ToString();    
            y = e.MarginBounds.Bottom - labelmargin - labelFont.GetHeight(e.Graphics);
            e.Graphics.DrawString(labelText, labelFont, Brushes.Black, e.MarginBounds.Right 
                - e.Graphics.MeasureString("-------------------", labelFont).Width, y);
        }

        private void cashierOrder_amount_ValueChanged(object sender, EventArgs e)
        {
           /* float totalPrice = float.Parse(cashierOrder_totalPrice.Text);
            float amt = float.Parse(cashierOrder_amount.Value.ToString());
            float final_amt = totalPrice - amt;
            cashierOrder_change.Text = final_amt.ToString();
           */
           
        }

        private void cashierOrder_amount_KeyDown_1(object sender, KeyEventArgs e)
       {
           if (e.KeyCode == Keys.Enter)
           {
                try
                {
                    float getAmount = Convert.ToSingle(cashierOrder_totalPrice.Text);
                    float amt = float.Parse(cashierOrder_amount.Value.ToString());
                    float getChange = (getAmount - amt);

                    if (getChange <= -1)
                    {
                        cashierOrder_amount.Text = "";
                        cashierOrder_change.Text = "";
                    }
                    else
                    {
                        cashierOrder_change.Text = getChange.ToString("0.00");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something went wrong: 3", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cashierOrder_amount.Text = "";
                    cashierOrder_change.Text = "";
                }
           }
        }
    }
}
