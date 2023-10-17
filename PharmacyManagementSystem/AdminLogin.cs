using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacyManagementSystem
{
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(txtAdminPass.Text == "")
            {

            }
            else if(txtAdminPass.Text == "Admin")
            {
                Dashboard obj = new Dashboard();
                obj.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong Admin Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAdminPass.Clear();
            }
        }
    }
}
