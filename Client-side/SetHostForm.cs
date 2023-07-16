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
    public partial class SetHostForm : Form
    {
        private readonly IShopData m_session;

        public SetHostForm()
        {
            InitializeComponent();
        }
        public SetHostForm(IShopData session)
        {
            InitializeComponent();
            m_session = session;
        }

        private void txtHostName_TextChanged(object sender, EventArgs e) => btnSetHost.Enabled = !string.IsNullOrEmpty(txtHostName.Text);
        private void txtAccountNo_TextChanged(object sender, EventArgs e) => btnSetHost.Enabled = !string.IsNullOrEmpty(txtAccountNo.Text);
        private void btnSetHost_Click(object sender, EventArgs e)
        {
            m_session.HostName = txtHostName.Text;
            m_session.AccountNumber = Convert.ToInt32(txtAccountNo.Text);
        }
        public void DisplayMessage(string message)
        {
            if (message == "CONNECT_ERROR")
            {
                MessageBox.Show("The connection attempt was unsuccessful. The account number is invalid.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
