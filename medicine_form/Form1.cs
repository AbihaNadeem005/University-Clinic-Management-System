using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace medicine_form
{
    public partial class Form1 : Form
    {
        private Form selectform; 
        public Form1()
        {
            InitializeComponent();
        }

        public Form1(Form selectForm)
        {
            InitializeComponent();
            this.selectform = selectForm;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database_ProjectDataSet2.Medicine_info' table. You can move, or remove it, as needed.
            this.medicine_infoTableAdapter1.Fill(this.database_ProjectDataSet2.Medicine_info);
            //// TODO: This line of code loads data into the 'database_ProjectDataSet1.Medicine_info' table. You can move, or remove it, as needed.
            //this.medicine_infoTableAdapter.Fill(this.database_ProjectDataSet1.Medicine_info);
            //dataGridView1.DataSource = this.database_ProjectDataSet1.Medicine_info;
        }

        private void button5_Click(object sender, EventArgs e) //back button
        {
            if (selectform != null)
            {
                selectform.Show();
                this.Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e) //insert button
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-UIDARLS\\SQLEXPRESS;Initial Catalog=Database Project;Integrated Security=True");

            try
            {
                con.Open();
                SqlCommand cm;

                string S_no = textBox1.Text;
                string Name = textBox4.Text;
                string Potency = textBox6.Text;
                string Pack_no = textBox3.Text;
                string Packing = textBox2.Text;
                string Single_Price = textBox5.Text;
                string Total_Pice = textBox7.Text;


                //Insert into Medicine_info
                string query1 = "Insert into Medicine_info(S_No,Name,Potency,Pack_no,Packing,Single_Price,Total_Price) values (" + textBox1.Text + ",'" + textBox4.Text + "','" + textBox6.Text + "','" + textBox3.Text + "','" + textBox2.Text + "','" + textBox5.Text + "','" + textBox7.Text + "')";
                cm = new SqlCommand(query1, con);
                cm.ExecuteNonQuery();
                MessageBox.Show("Record Inserted Successfully");
                this.medicine_infoTableAdapter.Fill(this.database_ProjectDataSet1.Medicine_info);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            con.Close();

        }

        private void button3_Click(object sender, EventArgs e) //Delete Button
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-UIDARLS\\SQLEXPRESS;Initial Catalog=Database Project;Integrated Security=True");

            try
            {
                SqlCommand cm;
                con.Open();

                //Get the S_no from textbox
                string S_no = textBox1.Text;
                DialogResult confirm = MessageBox.Show("Are you sure you want to delete this medicine record?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm == DialogResult.No)
                {
                    return;
                }
                //Delete from Treatment table
                string query1 = "Delete from Medicine_info where S_no=" + S_no;
                cm = new SqlCommand(query1, con);
                cm.ExecuteNonQuery();
                MessageBox.Show("Record deleted successfully.");
                this.medicine_infoTableAdapter.Fill(this.database_ProjectDataSet1.Medicine_info);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            con.Close();

        }

        private void button2_Click(object sender, EventArgs e) //update button
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-UIDARLS\\SQLEXPRESS;Initial Catalog=Database Project;Integrated Security=True");

            try
            {
                con.Open();
                SqlCommand cm;

                string S_no = textBox1.Text;
                string Name = textBox4.Text;
                string Potency = textBox6.Text;
                string Pack_no = textBox3.Text;
                string Packing = textBox2.Text;
                string Single_Price = textBox5.Text;
                string Total_Pice = textBox7.Text;

                //Update Medicine_info table
                string query1 = "Update Medicine_info set S_No='" + textBox1.Text + "',Name='" + textBox4.Text + "',Potency='" + textBox6.Text + "',Pack_no='" + textBox3.Text +
                        "',Packing='" + textBox2.Text + "',Single_Price='" + textBox5.Text + "',Total_Price='" + textBox7.Text + "'  where S_no='" + textBox1.Text + "'";
                cm = new SqlCommand(query1, con);
                cm.ExecuteNonQuery();
                MessageBox.Show("Record Updated Successfully");
                this.medicine_infoTableAdapter.Fill(this.database_ProjectDataSet1.Medicine_info);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            con.Close();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
       "Are you sure you want to logout?",
       "Confirm Logout",
       MessageBoxButtons.YesNo,
       MessageBoxIcon.Question
   );

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e) //search button
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-UIDARLS\\SQLEXPRESS;Initial Catalog=Database Project;Integrated Security=True");
            try
            {
                con.Open();
                // Take value from Name textbox
                string name = textBox4.Text;

                // SQL query to search by name (using LIKE for partial matches)
                string query = "SELECT * FROM Medicine_info WHERE Name LIKE @name";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@name", "%" + name + "%");  // allows partial search

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Bind results to DataGridView
                dataGridView1.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No medicine found with this name.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox1.Text = row.Cells[0].Value.ToString(); // S_no
                textBox4.Text = row.Cells[1].Value.ToString(); // Name
                textBox6.Text = row.Cells[2].Value.ToString(); // Potency
                textBox3.Text = row.Cells[3].Value.ToString(); // Pack_no
                textBox2.Text = row.Cells[4].Value.ToString(); // Packing
                textBox5.Text = row.Cells[5].Value.ToString(); // Single_Price
                textBox7.Text = row.Cells[6].Value.ToString(); // Total_Price
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }   
    }
}



