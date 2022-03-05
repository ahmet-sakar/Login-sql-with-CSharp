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

namespace ITEnzim.Forms
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private SqlConnection connection = new SqlConnection(@"Data Source=MACHINE_NAME;Initial Catalog=DBNAME;User ID=USERNAME;Password=PASSWORD");

        private void login_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Forms.register frmRegister = new register();
            frmRegister.Show();
            this.Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM users WHERE username=@username AND password=@password", connection);
            command.Parameters.AddWithValue("@username", txtUsername.Text);
            command.Parameters.AddWithValue("@password", txtPassword.Text);
            SqlDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
		if (dr["auth"].toString() == "1") 
		{
		    // Admin Panel redirect
		} 
		else if (dr["auth"].toString() == "0")  
		{
		    // User Panel redirect
		}

                dr.Close();
            }
            else
            {
                MessageBox.Show("Username or password wrong!", "Error");
            }

            connection.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
