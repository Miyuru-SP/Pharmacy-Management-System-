using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PharmacyManagementSystem
{
    public partial class Medicines : Form
    {
        public Medicines()
        {
            
            InitializeComponent();
            ShowMed();
            GetManufacturer();
            
            
            
        }
        SqlConnection con = new SqlConnection(@"Data Source=ASUSTUF;Initial Catalog=Pharmacydb;Integrated Security=True");

        
        private void ShowMed()
        {
            con.Open();
            string Query = "Select * from MedicineTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            MedicineDGV.DataSource = ds.Tables[0];
            con.Close();
            
        }

        

        private void Medicines_Load(object sender, EventArgs e)
        {

        }

        private void txtMedQty_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtMedPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtMedName.Text == "" || txtMedQty.Text == "" || txtMedPrice.Text == "" || txtMedManuName.Text == "" || cbMedType.Text == "" ) 
            {
                MessageBox.Show("Missing Information", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Reset();
            }
            else
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Insert into MedicineTbl(MedName, MedType, MedQty, MedPrice, MedExpireDate, MedManuId, MedManuName) Values(@MN, @MT, @MQ, @MP, @MED, @MMI, @MMN )", con);
                    
                    cmd.Parameters.AddWithValue("@MN", txtMedName.Text);
                    cmd.Parameters.AddWithValue("@MT", cbMedType.SelectedItem.ToString());      
                    cmd.Parameters.AddWithValue("@MQ", txtMedQty.Text);
                    cmd.Parameters.AddWithValue("@MP", txtMedPrice.Text);
                    cmd.Parameters.AddWithValue("@MED", ExpireDate.Value.Date);
                    cmd.Parameters.AddWithValue("@MMI", cbMedManu.Text);                //MedMenuId data type is int so can't converted into string value, just input as the plain text int
                    cmd.Parameters.AddWithValue("@MMN", txtMedManuName.Text);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i != 0)
                    {
                        MessageBox.Show("Medicine Added", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                 
                    ShowMed();
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
            txtMedManuName.Clear();
            txtMedName.Clear();
            cbMedType.Text = "";
            txtMedQty.Clear();
            txtMedPrice.Clear();
            cbMedManu.Text = "";
            key = 0;
        }

        private void GetManufacturer()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT ManuId FROM ManufacturerTbl", con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("ManuId", typeof(int));
            dt.Load(Rdr);
            cbMedManu.ValueMember = "ManuId";
            cbMedManu.DataSource = dt;
            con.Close();
        }
        private void GetManuName()
        {
            con.Open();
            string Query = "SELECT * FROM ManufacturerTbl WHERE ManuId = '" + cbMedManu.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(Query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                txtMedManuName.Text = dr["ManuName"].ToString();
            }
            con.Close();
        }

        private void cbMedManu_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetManuName();
        }
        int key = 0;
        private void MedicineDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMedName.Text = MedicineDGV.SelectedRows[0].Cells[1].Value.ToString();               ////By clicking an data it auto fill in text boxes
            cbMedType.Text = MedicineDGV.SelectedRows[0].Cells[2].Value.ToString();
            txtMedQty.Text = MedicineDGV.SelectedRows[0].Cells[3].Value.ToString();
            txtMedPrice.Text = MedicineDGV.SelectedRows[0].Cells[4].Value.ToString();
            ExpireDate.Text = MedicineDGV.SelectedRows[0].Cells[5].Value.ToString();
            cbMedManu.Text = MedicineDGV.SelectedRows[0].Cells[6].Value.ToString();
            txtMedManuName.Text = MedicineDGV.SelectedRows[0].Cells[7].Value.ToString();
            if (txtMedName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(MedicineDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Missing Information", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM MedicineTbl WHERE MedNum=@MKey", con);
                    cmd.Parameters.AddWithValue("@MKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Medicine Deleted", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();
                    ShowMed();
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
            if (txtMedName.Text == "" || txtMedQty.Text == "" || txtMedPrice.Text == "" || txtMedManuName.Text == "" || cbMedType.Text == "")
            {
                MessageBox.Show("Missing Information", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE MedicineTbl SET MedName = @MN , MedType = @MT, MedQty = @MQ, MedPrice = @MP, MedExpireDate = @MED, MedManuId = @MMI, MedManuName = @MMN WHERE MedNum = @MKey", con);

                    cmd.Parameters.AddWithValue("@MN", txtMedName.Text);
                    cmd.Parameters.AddWithValue("@MT", cbMedType.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@MQ", txtMedQty.Text);
                    cmd.Parameters.AddWithValue("@MP", txtMedPrice.Text);
                    cmd.Parameters.AddWithValue("@MED", ExpireDate.Value.Date);
                    cmd.Parameters.AddWithValue("@MMI", cbMedManu.Text);
                    cmd.Parameters.AddWithValue("@MMN", txtMedManuName.Text);
                    cmd.Parameters.AddWithValue("@MKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Medicine Updated", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();

                    ShowMed();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void panel11_Click(object sender, EventArgs e)
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

        private void label21_Click(object sender, EventArgs e)
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

        private void panel17_Click(object sender, EventArgs e)
        {
            POS obj = new POS();
            obj.Show();
            this.Hide();
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

        private void ExpireDate_onValueChanged(object sender, EventArgs e)
        {

        }
        
        //////////////////Nevigate buttons
       
        private void btnMin_Click(object sender, EventArgs e)
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

        private void panel12_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
