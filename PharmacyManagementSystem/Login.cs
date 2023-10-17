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

namespace PharmacyManagementSystem
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=ASUSTUF;Initial Catalog=Pharmacydb;Integrated Security=True");
        public static string User;

        private void label3_Click(object sender, EventArgs e)
        {
            AdminLogin obj = new AdminLogin();
            obj.Show();
            this.Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(txtUserName.Text == ""|| txtLoginPass.Text == "")
            {
                MessageBox.Show("Enter Both UserName and Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT (*) FROM SellerTbl WHERE SName = '"+ txtUserName.Text +"' AND SPass = '"+ txtLoginPass.Text +"'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    User = txtUserName.Text;
                    SellPOS obj = new SellPOS();
                    obj.Show();
                    this.Hide();
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Wrong User Name or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtLoginPass.Clear();
                    txtUserName.Clear();    

                }
                con.Close();
            }
        }
    }
}
