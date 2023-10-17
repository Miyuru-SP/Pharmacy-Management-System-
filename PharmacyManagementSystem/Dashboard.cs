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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            //ExpireDate obj1 = new ExpireDate();
            //obj1.Show();
            InitializeComponent();
            CountMed();
            CountSeller();
            CountCust();
            SumAmt();
            GetSellerName();
            GetBestSeller();
            GetBestCustomer();
        }
        SqlConnection con = new SqlConnection(@"Data Source=ASUSTUF;Initial Catalog=Pharmacydb;Integrated Security=True");
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void CountMed()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM MedicineTbl", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            txtMedNum.Text = dt.Rows[0][0].ToString();
            con.Close();
        }
        private void CountSeller()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM SellerTbl", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            txtSellerNum.Text = dt.Rows[0][0].ToString();
            con.Close();
        }
        private void CountCust()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM CustomerTbl", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            txtCustNum.Text = dt.Rows[0][0].ToString();
            con.Close();
        }

        private void SumAmt()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT SUM(BAmount) FROM BillTbl", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            lblSaleAmount.Text = "RS. " + dt.Rows[0][0].ToString();
            con.Close();
        }

        private void SumAmtBySeller()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT SUM(BAmount) FROM BillTbl WHERE Sname = '"+ cbSellerName.Text +"'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            lblSalesSeller.Text = "RS. " + dt.Rows[0][0].ToString();
            con.Close();
        }
        private void GetSellerName()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT SName FROM SellerTbl", con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("SName");
            dt.Load(Rdr);
            cbSellerName.ValueMember = "SName";
            cbSellerName.DataSource = dt;
            con.Close();
        }

        private void GetBestSeller()
        {
            try
            {
                con.Open();
                string InnerQuary = "SELECT MAX(BAmount) from BillTbl";
                DataTable dt1 = new DataTable();    
                SqlDataAdapter sda1 = new SqlDataAdapter(InnerQuary, con);
                sda1.Fill(dt1);
                string Query = "SELECT SName FROM BillTbl WHERE BAmount = '" + dt1.Rows[0][0].ToString() + "'";
                SqlDataAdapter sda  = new SqlDataAdapter(Query, con);
                DataTable dt = new DataTable();
                sda.Fill(dt );
                lblBestSeller.Text = dt.Rows[0][0].ToString();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
        }
        private void GetBestCustomer()
        {
            try
            {
                con.Open();
                string InnerQuary = "SELECT MAX(BAmount) from BillTbl";
                DataTable dt1 = new DataTable();
                SqlDataAdapter sda1 = new SqlDataAdapter(InnerQuary, con);
                sda1.Fill(dt1);
                string Query = "SELECT CustName FROM BillTbl WHERE BAmount = '" + dt1.Rows[0][0].ToString() + "'";
                SqlDataAdapter sda = new SqlDataAdapter(Query, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                lblBestCust.Text = dt.Rows[0][0].ToString();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
        }
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel12_Paint(object sender, PaintEventArgs e)
        {

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

        private void cbSellerName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SumAmtBySeller();
        }

        private void panel11_Click(object sender, EventArgs e)
        {
            
        }

        private void panel11_Click_1(object sender, EventArgs e)
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

        private void label24_Click(object sender, EventArgs e)
        {
            POS obj = new POS();
            obj.Show();
            this.Hide();
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to logout?","Log out",MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Login obj = new Login();
                obj.Show();
                this.Hide();
            }
            
            
        }
        //minimize
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
