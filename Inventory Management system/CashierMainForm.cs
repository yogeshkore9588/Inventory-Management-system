using System;
using System.Windows.Forms;

namespace Inventory_Management_system
{
    public partial class CashierMainForm : Form
    {
        public CashierMainForm()
        {
            InitializeComponent();
        }
        public void displayUsername()
        {
            string username = Form1.username.Substring(0, 1).ToUpper() + Form1.username.Substring(1);
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void logout_btn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to logout?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Form1 cmForm = new Form1();
                cmForm.Show();

                this.Hide();
            }
        }

        private void cashOrder11_Load(object sender, EventArgs e)
        {

        }

        private void CashierMainForm_Load(object sender, EventArgs e)
        {
            cashOrder11.displayAllAvailableProducts();
        }

        private void CashierMainForm_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cashOrder12_Load(object sender, EventArgs e)
        {

        }

        private void clear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close", "confirmation Message",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void dashboard_btn_Click(object sender, EventArgs e)
        {
            //adminDashboard1.Visible = true;
            //adminAddProducts1.Visible = false;
            //cashierCustomersForm1.Visible = false;
            //cashOrder11.Visible = false;

            adminDashboard2.Visible = true;
            adminAddProducts2.Visible = false;
            cashierCustomersForm1.Visible = false;
            cashOrder12.Visible = false;

            AdminDashboard adForm = adminDashboard1 as AdminDashboard;
            if(adForm != null)
            {
                adForm.refreshData();
            }
        }

        private void addProducts_btn_Click(object sender, EventArgs e)
        {
            //adminDashboard1.Visible = false;
            //adminAddProducts1.Visible = true;
            //cashierCustomersForm1.Visible = false;
            //cashOrder11.Visible = false;

            adminDashboard2.Visible = false;
            adminAddProducts2.Visible = true;
            cashierCustomersForm1.Visible = false;
            cashOrder12.Visible = false;

            AdminAddProducts aapForm = adminAddProducts1 as AdminAddProducts;
            if (aapForm != null)
            {
                aapForm.refreshData();
            }
        }

        private void customers_btn_Click(object sender, EventArgs e)
        {
            //adminDashboard1.Visible = false;
            //adminAddProducts1.Visible = false;
            //cashierCustomersForm1.Visible = true;
            //cashOrder11.Visible = false;

            adminDashboard2.Visible = false;
            adminAddProducts2.Visible = false;
            cashierCustomersForm1.Visible = true;
            cashOrder12.Visible = true;

            CashierCustomersForm ccForm = cashierCustomersForm1 as CashierCustomersForm;
            if (ccForm != null)
            {
                ccForm.refreshData();
            }
        }

        private void order_btn_Click(object sender, EventArgs e)
        {
            //adminDashboard1.Visible = false;
            //adminAddProducts1.Visible = false;
            //cashierCustomersForm1.Visible = false;
            //cashOrder11.Visible = true;

            adminDashboard2.Visible = false;
            adminAddProducts2.Visible = false;
            cashierCustomersForm1.Visible = false;
            cashOrder12.Visible = true;

            CashOrder1 coForm = cashOrder11 as CashOrder1;
            if (coForm != null)
            {
                coForm.refreshData();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
