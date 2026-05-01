using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Patientt_Form
{
    public partial class Form1 : Form
    {
        private Form selectform;
        public Form1()
        {
            InitializeComponent();
        }
        public Form1(Form selectform)
        {
            InitializeComponent();
            this.selectform = selectform;
        }

        private void button5_Click(object sender, EventArgs e) //back button
        {
            if (selectform != null)
            {
                selectform.Show();
                this.Hide();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database_ProjectDataSet.UCMSD' table. You can move, or remove it, as needed.
            this.uCMSDTableAdapter1.Fill(this.database_ProjectDataSet.UCMSD);
            dataGridView1.DataSource = this.database_ProjectDataSet.UCMSD;
            textBox6.Text = DateTime.Now.ToString("yyyy-MM-dd");
            textBox7.Text = DateTime.Now.DayOfWeek.ToString();
            textBox8.Text = DateTime.Now.ToString("hh:mm tt");
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e) //insert button
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-UIDARLS\\SQLEXPRESS;Initial Catalog=Database Project;Integrated Security=True");

            try
            {
                con.Open();
                SqlCommand cm;

                int id = Convert.ToInt32(textBox1.Text);
                textBox6.Text = DateTime.Now.ToString("yyyy-MM-dd");
                textBox7.Text = DateTime.Now.DayOfWeek.ToString();
                textBox8.Text = DateTime.Now.ToString("hh:mm tt");
                DateTime parsedDate = DateTime.Parse(textBox6.Text);
                DateTime parsedTime = DateTime.Parse(textBox8.Text);
                TimeSpan timeOnly = parsedTime.TimeOfDay;
                string name = textBox3.Text;
                string dept = textBox4.Text;
                string desg = textBox5.Text;
                string bds = textBox13.Text;
                string cell_no = textBox12.Text;
                string ser_no = textBox2.Text;
                string complains = textBox9.Text;
                string prescribed_medicine = textBox10.Text;
                string attended_by = textBox11.Text;

                //Insert into UCMSD table
                string query1 = "Insert into UCMSD(ID,Ser_No,Date,Day,Time,Name,Department,Designation,Boarder_Day_Scholar,Complains,Prescribed_Medicine,Attended_by,Cell_no) values (" + textBox1.Text + ",'" + textBox2.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox13.Text + "','" + textBox9.Text + "','" + textBox10.Text + "','" + textBox11.Text + "','" + textBox12.Text + "')";
                cm = new SqlCommand(query1, con);
                cm.ExecuteNonQuery();
                //Insert into Patient table
                string query2 = "Insert into Patient(ID,Name,Department,Designation,Boarder_Day_Scholar,Cell_no) values (" + textBox1.Text + ",'" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox13.Text + "','" + textBox12.Text + "')";
                cm = new SqlCommand(query2, con);
                cm.ExecuteNonQuery();
                //Insert into Treatment table
                string query3 = "Insert into Treatment(ID,Ser_No,Date,Day,Time,Complains,Prescribed_Medicine,Attended_by) values (" + textBox1.Text + ",'" + textBox2.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','" + textBox9.Text + "','" + textBox10.Text + "','" + textBox11.Text + "')";
                cm = new SqlCommand(query3, con);
                cm.ExecuteNonQuery();
                MessageBox.Show("Record Inserted Successfully");
                this.uCMSDTableAdapter1.Fill(this.database_ProjectDataSet.UCMSD);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            con.Close();
        }

        private void button4_Click_1(object sender, EventArgs e) //next button
        {
            medicine_form.Form1 medForm = new medicine_form.Form1(this);
            medForm.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_2(object sender, DataGridViewCellEventArgs e) //datagrid view
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox1.Text = row.Cells[0].Value.ToString(); // ID
                textBox2.Text = row.Cells[3].Value.ToString(); // Ser_No
                textBox6.Text = Convert.ToDateTime(row.Cells[1].Value).ToString("yyyy-MM-dd"); //Date
                textBox7.Text = row.Cells[2].Value.ToString(); // Day
                textBox8.Text = row.Cells[4].Value.ToString(); // Time
                textBox3.Text = row.Cells[5].Value.ToString(); // Name
                textBox4.Text = row.Cells[7].Value.ToString(); // Department
                textBox5.Text = row.Cells[6].Value.ToString(); // Designation
                textBox13.Text = row.Cells[8].Value.ToString(); // Boarder_Day_Scholar
                textBox12.Text = row.Cells[12].Value.ToString(); // Cell_no
                textBox9.Text = row.Cells[9].Value.ToString(); // Complains
                textBox10.Text = row.Cells[10].Value.ToString(); // Prescribed_Medicine
                textBox11.Text = row.Cells[11].Value.ToString(); // Attended_by
            }
        }

        private void button2_Click_1(object sender, EventArgs e) //delete button
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-UIDARLS\\SQLEXPRESS;Initial Catalog=Database Project;Integrated Security=True");

            try
            {
                SqlCommand cm;
                con.Open();

                //Get the ID from textbox
                int id = Convert.ToInt32(textBox1.Text);
                DialogResult confirm = MessageBox.Show("Are you sure you want to delete this patient's record?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm == DialogResult.No)
                {
                    return;
                }
                //Delete from Treatment table
                string query1 = "Delete from treatment where ID =" + id;
                cm = new SqlCommand(query1, con);
                cm.ExecuteNonQuery();
                //Delete from UCMSD table
                string query2 = "Delete from UCMSD where ID=" + id;
                cm = new SqlCommand(query2, con);
                cm.ExecuteNonQuery();
                //Delete from Patient table
                string query3 = "Delete from Patient where ID=" + id;
                cm = new SqlCommand(query3, con);
                cm.ExecuteNonQuery();

                MessageBox.Show("Record deleted successfully.");
                this.uCMSDTableAdapter1.Fill(this.database_ProjectDataSet.UCMSD);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            con.Close();
        }

        private void button3_Click_2(object sender, EventArgs e) //update button
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-UIDARLS\\SQLEXPRESS;Initial Catalog=Database Project;Integrated Security=True");

            try
            {
                con.Open();
                SqlCommand cm;

                int id = Convert.ToInt32(textBox1.Text);
                string date = textBox6.Text;
                string time = textBox8.Text;
                DateTime parsedDate = DateTime.Parse(textBox6.Text);
                DateTime parsedTime = DateTime.Parse(textBox8.Text);
                TimeSpan timeOnly = parsedTime.TimeOfDay;
                string name = textBox3.Text;
                string dept = textBox4.Text;
                string desg = textBox5.Text;
                string bds = textBox13.Text;
                string cell_no = textBox12.Text;
                string day = textBox7.Text;
                string ser_no = textBox2.Text;
                string complains = textBox9.Text;
                string prescribed_medicine = textBox10.Text;
                string attended_by = textBox11.Text;

                //Update UCMSD table
                string query1 = "Update UCMSD set Ser_No='" + textBox2.Text + "',Date='" + textBox6.Text + "',Day='" + textBox7.Text + "',Time='" + textBox8.Text +
                        "',Name='" + textBox3.Text + "',Department='" + textBox4.Text + "',Designation='" + textBox5.Text + "',Boarder_Day_Scholar='" + textBox13.Text +
                        "',Complains='" + complains + "',Prescribed_Medicine='" + prescribed_medicine + "',Attended_by='" + attended_by +
                        "',Cell_no='" + textBox12.Text + "' where ID=" + textBox1.Text + "";
                cm = new SqlCommand(query1, con);
                cm.ExecuteNonQuery();
                //Update Patient table
                string query2 = "Update Patient set Name='" + textBox3.Text + "',Department='" + textBox4.Text + "',Designation='" + textBox5.Text +
                        "',Boarder_Day_Scholar='" + textBox13.Text + "',Cell_no='" + textBox12.Text + "' where ID=" + textBox1.Text + "";
                cm = new SqlCommand(query2, con);
                cm.ExecuteNonQuery();
                //Update into Treatment table
                string query3 = "Update Treatment set Ser_No='" + textBox2.Text + "',Date='" + textBox6.Text + "',Day='" + textBox7.Text + "',Time='" + textBox8.Text +
                        "',Complains='" + textBox9.Text + "',Prescribed_Medicine='" + textBox10.Text +
                        "',Attended_by='" + textBox11.Text + "' where ID=" + textBox1.Text + "";
                cm = new SqlCommand(query3, con);
                cm.ExecuteNonQuery();
                MessageBox.Show("Record Updated Successfully");
                this.uCMSDTableAdapter1.Fill(this.database_ProjectDataSet.UCMSD);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            con.Close();
        }
    }
}










