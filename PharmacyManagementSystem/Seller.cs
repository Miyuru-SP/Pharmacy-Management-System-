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
    public partial class Seller : Form
    {
        public Seller()
        {
            InitializeComponent();
            ShowSeller();
        }
        SqlConnection con = new SqlConnection(@"Data Source=ASUSTUF;Initial Catalog=Pharmacydb;Integrated Security=True");

        private void ShowSeller()
        {
            con.Open();
            string Query = "Select * from SellerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            SellerDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        string gender = "";
        private void Reset()
        {
            txtSellerName.Clear();
            txtSellerPhone.Clear();
            txtSellerAddress.Clear();
            txtSellerPass.Clear();
            srb1.Checked = false;
            srb2.Checked = false;
            
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtSellerName.Text == "" || txtSellerAddress.Text == "" || txtSellerPhone.Text == "" || txtSellerPass.Text == "" )
            {
                MessageBox.Show("Missing Information", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {

                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO SellerTbl(SName, SDOB, Sphone, SAddress, SPass, SGender) VALUES (@SN, @SDOB, @SP, @SA, @SPASS, @SG)", con);
                    cmd.Parameters.AddWithValue("@SN", txtSellerName.Text);
                    cmd.Parameters.AddWithValue("@SDOB", SellerDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@SP", txtSellerPhone.Text);
                    cmd.Parameters.AddWithValue("@SA", txtSellerAddress.Text);
                    cmd.Parameters.AddWithValue("@SG", gender);                    
                    cmd.Parameters.AddWithValue("@SPASS", txtSellerPass.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller Added", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();
                    ShowSeller();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }
        
        
        int key = 0;
        private void SellerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtSellerName.Text = SellerDGV.SelectedRows[0].Cells[1].Value.ToString();               ////By clicking an data it auto fill in text boxes
            SellerDOB.Text = SellerDGV.SelectedRows[0].Cells[2].Value.ToString();
            txtSellerPhone.Text = SellerDGV.SelectedRows[0].Cells[3].Value.ToString();
            txtSellerAddress.Text = SellerDGV.SelectedRows[0].Cells[4].Value.ToString();
            txtSellerPass.Text = SellerDGV.SelectedRows[0].Cells[5].Value.ToString();
            

            if (txtSellerName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(SellerDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void srb1_Click(object sender, EventArgs e)
        {
            if(srb1.Checked == true)
            {
                gender = "Male";
            }
            else
            {
                gender = string.Empty;
            }
        }

        private void srb2_Click(object sender, EventArgs e)
        {
            if (srb2.Checked == true)
            {
                gender = "Female";
            }
            else
            {
                gender = string.Empty;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtSellerName.Text == "" || txtSellerAddress.Text == "" || txtSellerPhone.Text == "" || txtSellerPass.Text == "")
            {
                MessageBox.Show("Missing Information", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {

                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE SellerTbl SET SName = @SN, SDOB = @SDOB, SPhone = @SP, SAddress = @SA, Spass = @SPASS, SGender = @SG WHERE SNum = @SKey", con);
                    cmd.Parameters.AddWithValue("@SN", txtSellerName.Text);
                    cmd.Parameters.AddWithValue("@SDOB", SellerDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@SP", txtSellerPhone.Text);
                    cmd.Parameters.AddWithValue("@SA", txtSellerAddress.Text);
                    cmd.Parameters.AddWithValue("@SG", gender);
                    cmd.Parameters.AddWithValue("@SPASS", txtSellerPass.Text);
                    cmd.Parameters.AddWithValue("@SKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller Updated", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();
                    ShowSeller();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select the Seller", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM SellerTbl WHERE SNum=@SKey", con);
                    cmd.Parameters.AddWithValue("@SKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller Deleted", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();
                    ShowSeller();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
        }

        private void panel12_Click(object sender, EventArgs e)
        {
            Medicines obj = new Medicines();
            obj.Show();
            this.Hide();
        }

        private void panel13_Click(object sender, EventArgs e)
        {
            Manufacturer obj = new Manufacturer();
            obj.Show();
            this.Hide();

        }

        private void panel14_Click(object sender, EventArgs e)
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

        private void label24_Click(object sender, EventArgs e)
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMax_Click(object sender, EventArgs e)
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

        private void btnMin_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void txtSellerName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSellerName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode== Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }
    }
    
}
