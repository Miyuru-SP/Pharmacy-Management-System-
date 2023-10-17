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
    public partial class Customer : Form
    {
        public Customer()
        {
            InitializeComponent();
            ShowCust();
        }
        SqlConnection con = new SqlConnection(@"Data Source=ASUSTUF;Initial Catalog=Pharmacydb;Integrated Security=True");

        private void ShowCust()
        {
            con.Open();
            string Query = "Select * from CustomerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CustomerDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        string gender = "";
        
        private void Reset()
        {
            txtCustName.Clear();
            txtCustAddress.Clear();
            txtCustPhone.Clear();
            rb1.Checked= false;
            rb2.Checked= false;
            //CustBOB.ResetText();
        }
        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (txtCustName.Text == "" || txtCustAddress.Text == "" || txtCustPhone.Text == "")
            {
                MessageBox.Show("Missing Information", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO CustomerTbl(CustName, CustPhone, CustAddress, CustDOB, CustGender) VALUES (@CN, @CP, @CA, @CDOB, @CG)", con);
                    cmd.Parameters.AddWithValue("@CN", txtCustName.Text);
                    cmd.Parameters.AddWithValue("@CP", txtCustPhone.Text);
                    cmd.Parameters.AddWithValue("@CA", txtCustAddress.Text);
                    cmd.Parameters.AddWithValue("@CDOB", CustBOB.Value.Date);
                    cmd.Parameters.AddWithValue("@CG", gender);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Added", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();
                    ShowCust();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }
        private void rb1_Click(object sender, EventArgs e)
        {
            if (rb1.Checked == true)
            {
                gender = "Male";
            }
            else
            {
                gender = "Female";
            }
        }

        private void rb2_Click(object sender, EventArgs e)
        {
            if (rb2.Checked == true)
            {
                gender = "Female";
            }
            else
            {
                gender = "Male";
            }
        }
        int key = 0;
        private void CustomerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)           //Show the values on datagrid
        {
            txtCustName.Text = CustomerDGV.SelectedRows[0].Cells[1].Value.ToString();               ////By clicking an data it auto fill in text boxes
            txtCustPhone.Text = CustomerDGV.SelectedRows[0].Cells[2].Value.ToString();
            txtCustAddress.Text = CustomerDGV.SelectedRows[0].Cells[3].Value.ToString();
            CustBOB.Text = CustomerDGV.SelectedRows[0].Cells[4].Value.ToString();
            //rb1.Checked = CustomerDGV.SelectedRows[0].Cells[5].Value.Equals(rb1);
            //rb2.Checked = CustomerDGV.SelectedRows[0].Cells[6].Value.Equals();

            if (txtCustName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(CustomerDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)                      
        {
            if (key == 0)
            {
                MessageBox.Show("Select the Customer", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM CustomerTbl WHERE CustNum=@MKey", con);
                    cmd.Parameters.AddWithValue("@MKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Deleted", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();
                    ShowCust();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void bunifuThinButton22_Click(object sender, EventArgs e)                                  
        {
            if (txtCustName.Text == "" || txtCustAddress.Text == "" || txtCustPhone.Text == "")
            {
                MessageBox.Show("Missing Information", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {

                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE CustomerTbl SET CustName = @CN, CustPhone = @CP, CustAddress = @CA, CustDOB = @CDOB, CustGender = @CG WHERE CustNum = @CKey", con);
                    cmd.Parameters.AddWithValue("@CN", txtCustName.Text);
                    cmd.Parameters.AddWithValue("@CP", txtCustPhone.Text);
                    cmd.Parameters.AddWithValue("@CA", txtCustAddress.Text);
                    cmd.Parameters.AddWithValue("@CDOB", CustBOB.Value.Date);
                    cmd.Parameters.AddWithValue("@CG", gender);
                    cmd.Parameters.AddWithValue("@CKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Updated", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();
                    ShowCust();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }

        private void CustBOB_onValueChanged(object sender, EventArgs e)
        {

        }                                                                                               

        private void panel11_Click(object sender, EventArgs e)
        {
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
        }

        private void label20_Click(object sender, EventArgs e)
        {
            Medicines obj = new Medicines();
            obj.Show();
            this.Hide();
        }

        private void label21_Click(object sender, EventArgs e)
        {
            Manufacturer obj = new Manufacturer();
            obj.Show();
            this.Hide();
        }

        private void label22_Click(object sender, EventArgs e)
        {
            Customer obj = new Customer();
            obj.Show();
            this.Hide();
        }

        private void panel15_Click(object sender, EventArgs e)
        {
            Seller obj = new Seller();
            obj.Show();
            this.Hide();
        }

        private void panel17_Click(object sender, EventArgs e)
        {
            POS obj = new POS();
            obj.Show();
            this.Hide();
        }

        private void label19_Click(object sender, EventArgs e)                         
        {

            if (MessageBox.Show("Are you sure you want to logout?", "Log out", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Login obj = new Login();
                obj.Show();
                this.Hide();
            }

        }

        private void Customer_Load(object sender, EventArgs e)
        {

        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            
        }

        private void btnMax_Click(object sender, EventArgs e)
        {
           
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel18_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnMin_Click_1(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnMax_Click_1(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
            }
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        //private void btnMini_Click(object sender, EventArgs e)
        //{
        //    WindowState = FormWindowState.Minimized;
        //}

        //private void btnMax_Click(object sender, EventArgs e)
        //{
        //    if (WindowState == FormWindowState.Normal)
        //    {
        //        WindowState= FormWindowState.Maximized;
        //    }
        //    else
        //    {
        //        WindowState = FormWindowState.Normal;
        //    }
        //}

        //private void btnClose_Click(object sender, EventArgs e)
        //{
        //    Application.Exit();
        //}
    }
}
