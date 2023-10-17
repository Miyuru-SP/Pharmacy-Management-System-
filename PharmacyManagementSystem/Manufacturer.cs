using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PharmacyManagementSystem
{
    public partial class Manufacturer : Form
    {
        public Manufacturer()
        {
            InitializeComponent();
            ShowManu();
        }
        SqlConnection con = new SqlConnection(@"Data Source=ASUSTUF;Initial Catalog=Pharmacydb;Integrated Security=True");

        private void ShowManu()
        {
            con.Open();
            string Query = "Select * from ManufacturerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ManufacturerDGV.DataSource= ds.Tables[0];
            con.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtManuAddress.Text == "" || txtManuName.Text == "" || txtManuPhone.Text == "")
            {
                MessageBox.Show("Missing Information", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO ManufacturerTbl(ManuName, ManuAddress, ManuPhone, ManuDate) VALUES (@MN, @MA, @MP, @MJD)", con);
                    cmd.Parameters.AddWithValue("@MN", txtManuName.Text);
                    cmd.Parameters.AddWithValue("@MA", txtManuAddress.Text);
                    cmd.Parameters.AddWithValue("@MP", txtManuPhone.Text);
                    cmd.Parameters.AddWithValue("@MJD", ManuDate.Value.Date);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Manufacturer Added","Success!",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    con.Close();
                    ShowManu();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                

            }
        }
        int key = 0;

        private void ManufacturerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtManuName.Text = ManufacturerDGV.SelectedRows[0].Cells[1].Value.ToString();               ////By clicking an data it auto fill in text boxes
            txtManuAddress.Text = ManufacturerDGV.SelectedRows[0].Cells[2].Value.ToString();               
            txtManuPhone.Text = ManufacturerDGV.SelectedRows[0].Cells[3].Value.ToString();                 
            ManuDate.Text = ManufacturerDGV.SelectedRows[0].Cells[4].Value.ToString();                    
            if(txtManuName.Text == "")                                                                     
            {                                                                                              
                key = 0;                                                                                   
            }                                                                                              
            else                                                                                           
            {                                                                                              
                key = Convert.ToInt32(ManufacturerDGV.SelectedRows[0].Cells[0].Value.ToString());          
            }                                                                                            ////..................  
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select the Manufacturer","Erorr",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM ManufacturerTbl WHERE ManuId=@MKey", con);
                    cmd.Parameters.AddWithValue("@MKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Manufacturer Deleted", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();
                    ShowManu();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtManuAddress.Text == "" || txtManuName.Text == "" || txtManuPhone.Text == "")
            {
                MessageBox.Show("Missing Information", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE ManufacturerTbl SET ManuName = @MN, ManuAddress = @MA, ManuPhone = @MP, ManuDate =@MJD WHERE ManuId = @MKey", con);
                    cmd.Parameters.AddWithValue("@MN", txtManuName.Text);
                    cmd.Parameters.AddWithValue("@MA", txtManuAddress.Text);
                    cmd.Parameters.AddWithValue("@MP", txtManuPhone.Text);
                    cmd.Parameters.AddWithValue("@MJD", ManuDate.Value.Date);
                    cmd.Parameters.AddWithValue("@MKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Manufacturer Updated", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();
                    ShowManu();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }
        private void Reset()
        {
            txtManuAddress.Clear();
            txtManuName.Clear();
            txtManuPhone.Clear();
            ManuDate.Text = "";
            key = 0;
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to logout?", "Log out", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Login obj = new Login();
                obj.Show();
                this.Hide();
            }
        }

        private void label14_Click(object sender, EventArgs e)
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

        private void panel13_Click(object sender, EventArgs e)
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

        private void label23_Click(object sender, EventArgs e)
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

        private void btnMin_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
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

        private void btnClose_Click(object sender, EventArgs e)
        {

            Application.Exit();

        }
    }
}
