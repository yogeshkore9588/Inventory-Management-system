﻿using System;
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
    public partial class CashierCustomersForm : UserControl
    {
        public CashierCustomersForm()
        {
            InitializeComponent();

            displayCustomers();
        }
        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }
            displayCustomers();
        }

        public void displayCustomers()
        {
            CustomersData cData = new CustomersData();

            List<CustomersData> listData = cData.allCustomers();

            dataGridView1.DataSource = listData;
        }
    }
}
