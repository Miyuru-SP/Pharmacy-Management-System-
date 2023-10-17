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
    public partial class POS : Form
    {
        public POS()                        
        {
            timer1 = new Timer();
            timer1.Start();
            
            InitializeComponent();
            ShowMed();
            ShowBill();
            Sname.Text = "Admin" ;                    //Show user name at the top of the page
            GetCustomer();
            //Test();
        }
        SqlConnection con = new SqlConnection(@"Data Source=ASUSTUF;Initial Catalog=Pharmacydb;Integrated Security=True");

        private void GetCustomer()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT CustNum FROM CustomerTbl", con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CustNum", typeof(int));
            dt.Load(Rdr);
            cbCustId.ValueMember = "CustNum";
            cbCustId.DataSource = dt;
            con.Close();
        }
        private void GetCustName()
        {
            con.Open();
            string Query = "SELECT * FROM CustomerTbl WHERE CustNum = '" + cbCustId.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(Query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                txtCustName.Text = dr["CustName"].ToString();
            }
            con.Close();
        }
        private void ShowMed()
        {
            con.Open();
            string Query = "Select MedNum, MedName, MedType, MedQty, MedPrice, MedExpireDate from MedicineTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            MedicineDGV.DataSource = ds.Tables[0];
            con.Close();

        }
        private void ShowBill()
        {
            con.Open();
            string Query = "Select * from BillTbl where Sname = '"+ Sname.Text +"'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            TransactionGDV.DataSource = ds.Tables[0];
            con.Close();

        }

        int key = 0, Stock;
        int n = 1, GrdTotal = 0;
        int MedId, MedPrice, MedQty, MedTot, pos = 60;
        string MedName;

        private void cbCustId_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCustName();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("The Maple Safety Pharmacy", new Font("Century Gothic", 12, FontStyle.Regular), Brushes.Red, new Point(26));
            e.Graphics.DrawString(" ID Medicine Price Quantity Total", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Red, new Point(26, 40));
            foreach(DataGridViewRow row in BillGDV.Rows)
            {
                //Item = Convert.ToInt32(row.Cells["Column1"].Value);

                MedId = Convert.ToInt32(row.Cells["Column1"].Value);
                MedName = "" + row.Cells["Column2"].Value;
                MedPrice = Convert.ToInt32(row.Cells["Column3"].Value);
                MedQty = Convert.ToInt32(row.Cells["Column4"].Value);
                MedTot = Convert.ToInt32(row.Cells["Column5"].Value);

                //MedId = Convert.ToInt32(row.Cells["Column2"].Value);
                //MedName = "" + row.Cells["Column3"].Value;
                //MedPrice = Convert.ToInt32(row.Cells["Column4"].Value);
                //MedQty = Convert.ToInt32(row.Cells["Column5"].Value);
                //MedTot = Convert.ToInt32(row.Cells["Column6"].Value);

                //e.Graphics.DrawString("" + Item, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(26, pos));
                e.Graphics.DrawString("" + MedId, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(26, pos));
                e.Graphics.DrawString("" + MedName, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(45, pos));
                e.Graphics.DrawString("" + MedPrice, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(120, pos));
                e.Graphics.DrawString("" + MedQty, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(170, pos));
                e.Graphics.DrawString("" + MedTot, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(235, pos));
                pos = pos + 20;
            }
            e.Graphics.DrawString("Grand Total : RS. " + GrdTotal, new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Blue, new Point(50, pos + 50));
            e.Graphics.DrawString("**************** MSP Contribution ****************", new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(10, pos + 85));
            BillGDV.Rows.Clear();
            BillGDV.Refresh();
            GrdTotal = 0;
            n = 0;
        }

        private void TransactionDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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

        private void BillGDV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        //Show Time
        private void timer1_Tick(object sender, EventArgs e)
        {
            txtDate.Text = ("Date : " + DateTime.Now.ToLongDateString());
            txtTime.Text = ("Time : " + DateTime.Now.ToLongTimeString());
        }

        private void txtDate_Click(object sender, EventArgs e)
        {

        }

        //private void GetMed()
        //{
        //    con.Open();
        //    SqlCommand cmd = new SqlCommand("SELECT MedNum FROM MedicineTbl", con);
        //    SqlDataReader Rdr;
        //    Rdr = cmd.ExecuteReader();
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("MedNum", typeof(int));
        //    dt.Load(Rdr);
        //    cbMedID.ValueMember = "MedNum";
        //    cbMedID.DataSource = dt;
        //    con.Close();
        //}
        //private void GetMedName()
        //{
        //    con.Open();
        //    string Query = "SELECT * FROM MedicineTbl WHERE MedNum = '" + cbMedID.SelectedValue.ToString() + "'";
        //    SqlCommand cmd = new SqlCommand(Query, con);
        //    DataTable dt = new DataTable();
        //    SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //    sda.Fill(dt);
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        txtMedName.Text = dr["MedName"].ToString();
        //    }
        //    con.Close();
        //}

        //private void GetMedPrice()
        //{
        //    con.Open();
        //    string Query = "SELECT * FROM MedicineTbl WHERE MedNum = '" + cbMedID.SelectedValue.ToString() + "'";
        //    SqlCommand cmd = new SqlCommand(Query, con);
        //    DataTable dt = new DataTable();
        //    SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //    sda.Fill(dt);
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        txtMedPrice.Text = dr["MedPrice"].ToString();
        //    }
        //    con.Close();
        //}

        //private void cbMedID_SelectionChangeCommitted(object sender, EventArgs e)
        //{
        //    GetMedName();
        //    GetMedPrice();
        //}

        private void UpdateQty()
        {
            try
            {
                int NewQty = Stock - Convert.ToInt32(txtMedQty.Text);
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE MedicineTbl SET MedQty = @MQ WHERE MedNum = @MKey", con);

                cmd.Parameters.AddWithValue("@MQ", NewQty);                
                cmd.Parameters.AddWithValue("@MKey", key);
                cmd.ExecuteNonQuery();
                //MessageBox.Show("Medicine Updated", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();

                ShowMed();
                //Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InsertBill()
        {
            if(txtCustName.Text == "")
            {

            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO BillTbl(SName, CustNum, CustName, BDate, BAmount) VALUES (@SN, @CN, @CUN, @BD, @BA)", con);
                    cmd.Parameters.AddWithValue("@SN", Sname.Text);
                    cmd.Parameters.AddWithValue("@CN", cbCustId.Text);
                    cmd.Parameters.AddWithValue("@CUN", txtCustName.Text);
                    cmd.Parameters.AddWithValue("@BD", DateTime.Today.Date);
                    cmd.Parameters.AddWithValue("@BA", GrdTotal);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bill Saved", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();
                    ShowBill();
                    //Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            InsertBill();
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
            if(printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
            Reset();
            txtTotValue.Text = string.Empty;
        }

        private void btnAddtoBill_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();


                //if ()
                //{

                //}

                //else
                //{

                //}

                if (txtMedQty.Text == "")
                {
                    MessageBox.Show("Enter Valid Quantity", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (Convert.ToInt32(txtMedQty.Text) > Stock)
                {
                    MessageBox.Show("Quantity is not Available", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int total = Convert.ToInt32(txtMedQty.Text) * Convert.ToInt32(txtMedPrice.Text);
                    DataGridViewRow newRow = new DataGridViewRow();
                    newRow.CreateCells(BillGDV);
                    newRow.Cells[0].Value = n;
                    newRow.Cells[1].Value = cbMedID.Text;
                    newRow.Cells[2].Value = txtMedName.Text;
                    newRow.Cells[3].Value = txtMedQty.Text;
                    newRow.Cells[4].Value = txtMedPrice.Text;
                    newRow.Cells[5].Value = total;
                    BillGDV.Rows.Add(newRow);
                    GrdTotal = GrdTotal + total;
                    txtTotValue.Text = "RS. " + GrdTotal;
                    n++;
                    UpdateQty();
                    txtMedName.Clear();
                    txtMedPrice.Clear();
                    txtMedQty.Clear();
                    //cbMedID.Items.Clear();
                    cbMedID.Text = "";
                }
                con.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception Occurs", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        private void MedicineDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMedName.Text = MedicineDGV.SelectedRows[0].Cells[1].Value.ToString();               ////By clicking an data it auto fill in text boxes
            cbMedID.Text = MedicineDGV.SelectedRows[0].Cells[0].Value.ToString();
            Stock = Convert.ToInt32( MedicineDGV.SelectedRows[0].Cells[3].Value.ToString());
            txtMedPrice.Text = MedicineDGV.SelectedRows[0].Cells[4].Value.ToString();
            //cbMedManu.Text = MedicineDGV.SelectedRows[0].Cells[5].Value.ToString();
            //txtMedManuName.Text = MedicineDGV.SelectedRows[0].Cells[6].Value.ToString();
            if (txtMedName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(MedicineDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
        private void Reset()
        {
           // txtCustName.Clear();
            txtMedName.Clear();
            txtMedPrice.Clear();
            txtMedQty.Clear();
           // cbMedID.Items.Clear();
            cbMedID.Text = "";
            //cbCustId.Items.Clear();                       //Customer buy more than one drug per time, so clearing cus id is inappropiate


        }

        
        
    }
}
