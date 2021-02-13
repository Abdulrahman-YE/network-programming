using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmailApplication
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode);
            this.Close();
        }

        private void minmizeBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            if(Regex.IsMatch(emailTextBox.Text, @"^([a-zA-Z0-9_\-\.]+)@([gmail]+)\.([a-zA-Z]{2,5})$") && passwordTextBox.Text.Length >8)
{
                Dashboard form = new Dashboard(emailTextBox.Text, passwordTextBox.Text);
                this.Hide();
                form.Show();
            }
            else
            {
                MessageBox.Show(this, "The email must have a pattern of email - example@example.com. The password must at least have 8 characters", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
