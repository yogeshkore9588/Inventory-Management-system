using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminAddUser addUser = new AdminAddUser();
            addUser.Show();
            //this.Hide();
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
    }
}
