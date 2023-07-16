using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client_side
{
    public partial class ShopClientForm : Form
    {
        private readonly IShopData m_shopData;

        public ShopClientForm()
        {
            InitializeComponent();
        }
        public ShopClientForm(IShopData shopData)
        {
            InitializeComponent();
            m_shopData = shopData;
        }

        private void ShopClientForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_shopData.Disconnect();
        }

        private void ShopClientForm_Load(object sender, EventArgs e)
        {
            if (SetHost())
                m_shopData.Connect(m_shopData.AccountNumber);
            else
                Application.Exit();
        }

        private bool SetHost() => new SetHostForm(m_shopData).ShowDialog(this) == DialogResult.OK;

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            m_shopData.Purchase(cmbProducts.Text);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            string orders = "";
            string order = "";

            foreach (var item in m_shopData.m_currentOrders)
            {
                orders += $"{item.Value.ProductName},{item.Value.Quantity},{item.Value.Username}|";
            }

            string[] orderInfo = orders.Split('|');

            foreach (string i in orderInfo)
            {
                string[] nameQuantityUsername = i.Split(',');

                if (!string.IsNullOrEmpty(nameQuantityUsername[0]) && nameQuantityUsername[1] != "0")
                    order += $"{nameQuantityUsername[0]}, {nameQuantityUsername[1]}, {nameQuantityUsername[2]}{Environment.NewLine}";
            }
            txtOrders.Text = order;
        }
        public void DisplayMessage(string message)
        {
            if (message == "NOT_AVAILABLE")
            {
                MessageBox.Show("The product is not available (i.e., is already purchased by another client) and cannot be purchased.", "Not Available", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (message == "NOT_VALID")
            {
                MessageBox.Show("The specified product is not valid.", "Not Valid", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
