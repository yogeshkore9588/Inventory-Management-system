using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Management_system
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            displayUsername();
        }

        public void displayUsername()
        {
            //string username = Form1.username.Substring(0, 1).ToUpper() + Form1.username.Substring(1);
            //label3.Text = username;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            AdminAddUser addUser = new AdminAddUser();
            addUser.Show();
            //this.Hide();

            adminDashboard1.Visible = false;
            adminAddUser1.Visible = true;
            adminAddCategories1.Visible = false;
            adminAddProducts1.Visible = false;
            cashierCustomersForm1.Visible = false;

            AdminAddUser aauForm = adminAddUser1 as AdminAddUser;

            if (aauForm != null)
            {
                aauForm.refreshData();
            }
        }

        private void clear_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to close", "confirmation Message", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();   
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to logout", "confirmation Message",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            { 
                Form1 loginForm = new Form1();
                loginForm.Show();

                this.Hide();
            }
        } 

        private void adminDashboard4_Load(object sender, EventArgs e)
        {

        }

        private void adminAddUser1_Load(object sender, EventArgs e)
        {

        }

        private void adminAddCategories1_Load(object sender, EventArgs e)
        {

        }

        private void adminAddProducts2_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void adminAddProducts1_Load(object sender, EventArgs e)
        {

        }

        private void adminDashboard3_Load(object sender, EventArgs e)
        {

        }

        private void adminDashboard1_Load(object sender, EventArgs e)
        {

        }

        private void adminDashboard2_Load(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void addUsers_clearBtn_Click(object sender, EventArgs e)
        {

        }

        private void addUsers_renameBtn_Click(object sender, EventArgs e)
        {

        }

        private void addUsers_updateBtn_Click(object sender, EventArgs e)
        {

        }

        private void addUsers_addBtn_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void addUsers_status_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void addUsers_role_Click(object sender, EventArgs e)
        {

        }

        private void addUsers_password_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void addUsers_username_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void adminAddCategories2_Load(object sender, EventArgs e)
        {

        }

        private void adminAddCategories3_Load(object sender, EventArgs e)
        {

        }

        private void addUsers_btn_Click(object sender, EventArgs e)
        {
            AdminAddUser addUser = new AdminAddUser();
            addUser.Show();
            //this.Hide();

            //adminDashboard1.Visible = false;
            //adminAddUser1.Visible = true;
            //adminAddCategories1.Visible = false;
            //adminAddProducts1.Visible = false;
            //cashierCustomersForm1.Visible = false;

            adminDashboard2.Visible = false;
            adminAddUser2.Visible = true;
            adminAddCategories2.Visible = false;
            adminAddProducts2.Visible = false;
            cashierCustomersForm2.Visible = false;

            AdminAddUser aauForm = adminAddUser1 as AdminAddUser;

            if (aauForm != null)
            {
                aauForm.refreshData();
                aauForm.Show();
            }
        }

        private void addCategories_btn_Click(object sender, EventArgs e)
        {
            //adminDashboard1.Visible = false;
            //adminAddUser1.Visible = false;
            //adminAddCategories1.Visible = true;
            //adminAddProducts1.Visible = false;
            //cashierCustomersForm1.Visible = false;

            adminDashboard2.Visible = false;
            adminAddUser2.Visible = false;
            adminAddCategories2.Visible = true;
            adminAddProducts2.Visible = false;
            cashierCustomersForm2.Visible = false;

            AdminAddCategories aacForm = adminAddCategories1 as AdminAddCategories;

            if (aacForm != null)
            {
                aacForm.refreshData();
            }
        }

        private void addProducts_btn_Click(object sender, EventArgs e)
        {
            //adminDashboard1.Visible = false;
            //adminAddUser1.Visible = false;
            //adminAddCategories1.Visible = false;
            //adminAddProducts1.Visible = true;
            //cashierCustomersForm1.Visible = false;

            adminDashboard2.Visible = false;
            adminAddUser2.Visible = false;
            adminAddCategories2.Visible = false;
            adminAddProducts2.Visible = true;
            cashierCustomersForm2.Visible = false;

            AdminAddProducts aapForm = adminAddProducts1 as AdminAddProducts;

            if (aapForm != null)
            {
                aapForm.refreshData();
            }
        }

        private void dashboard_btn_Click(object sender, EventArgs e)
        {
            //adminDashboard1.Visible = true;
            //adminAddUser1.Visible = false;
            //adminAddCategories1.Visible = false;
            //adminAddProducts1.Visible = false;
            //cashierCustomersForm1.Visible = false;

            adminDashboard2.Visible = true;
            adminAddUser2.Visible = false;
            adminAddCategories2.Visible = false;
            adminAddProducts2.Visible = false;
            cashierCustomersForm2.Visible = false;

            AdminDashboard adForm = adminDashboard1 as AdminDashboard;

            if (adForm != null)
            {
                adForm.refreshData();
            }
        }

        private void customers_btn_Click(object sender, EventArgs e)
        {
            //adminDashboard1.Visible = false;
            //adminAddUser1.Visible = false;
            //adminAddCategories1.Visible = false;
            //adminAddProducts1.Visible = false;
            //cashierCustomersForm1.Visible = true;

            adminDashboard2.Visible = false;
            adminAddUser2.Visible = false;
            adminAddCategories2.Visible = false;
            adminAddProducts2.Visible = false;
            cashierCustomersForm2.Visible = true;

            CashierCustomersForm ccfForm = cashierCustomersForm1 as CashierCustomersForm;

            if (ccfForm != null)
            {
                ccfForm.refreshData();
            }
        }
    }
}
