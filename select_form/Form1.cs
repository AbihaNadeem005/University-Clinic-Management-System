//new form

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Select_Form
{
    public partial class Form1 : Form
    {
        private Form login;
        private string selectedform = "";
        public Form1()
            : this(null)
        { }
        public Form1(Form loginForm)
        {
            InitializeComponent();
            this.login = loginForm;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            selectedform = "Patient";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            selectedform = "Stock";
        }

        private void button3_Click(object sender, EventArgs e) //back button
        {
            login.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (selectedform == "Patient")
            {

                Patientt_Form.Form1 patientForm = new Patientt_Form.Form1(this);
                patientForm.Show();
                this.Hide();
            }
            else if (selectedform == "Stock")
            {

                medicine_form.Form1 stockForm = new medicine_form.Form1(this);
                stockForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please select a form before clicking Next.");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
        "Are you sure you want to logout?",       // message
        "Logout Confirmation",                    // title
        MessageBoxButtons.YesNo,                  // buttons
        MessageBoxIcon.Question                   // icon
    );

            if (result == DialogResult.Yes)
            {
                // Close the current form
                this.Close();

                // OR if you want to exit the entire application:
                // Application.Exit();
            }
        }

    }
}


