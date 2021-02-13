using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmailApplication
{
    public partial class Dashboard : Form
    {
        private string email;
        private string password;
        public Dashboard()
        {
            InitializeComponent();
            successLbl.Hide();
        }

        public Dashboard(string email, string password)
        {
            InitializeComponent();
            this.email = email;
            this.password = password;
            successLbl.Hide();
            emailLbl.Text = email;

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

        private void homeBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (homeBtn.Checked == true)
            {
                this.homePanel.BringToFront();
            }
        }

        private void guna2Button1_CheckedChanged(object sender, EventArgs e)
        {
            if (emailBtn.Checked == true)
            {
                this.emailPanel.BringToFront();
            }
        }

        private void closeBtn2_Click(object sender, EventArgs e)
        {
            this.closeBtn_Click(null, null);
        }

        private void minmizeBtn2_Click(object sender, EventArgs e)
        {
            this.minmizeBtn_Click(null, null);

        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            if(validateInput() == true)
            {

                MailMessage message = new MailMessage(email, recipientTextBox.Text);
                message.Subject = subjectTextBox.Text;
                message.Body = contentTextBox.Text;

                SmtpClient mailer = new SmtpClient("smtp.gmail.com", 587);
                mailer.Credentials = new NetworkCredential(email, password);
                mailer.EnableSsl = true;
                mailer.Send(message);

                mailer = null;

                subjectTextBox.Text = null;
                recipientTextBox.Text = null;
                contentTextBox.Text = null;
                successLbl.Show();


            }
        }

        private bool validateInput()
        {
            if(subjectTextBox.Text.Length < 1)
            {
                MessageBox.Show(this, "Subject field is required.", "Field is required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if(recipientTextBox.Text.Length < 5)
            {
                MessageBox.Show(this, "Please specify the recipient of the email..", "Field is required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (contentTextBox.Text.Length < 1)
            {
                MessageBox.Show(this, "Content body is empty.", "Field is required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Login form = new Login();
            this.Hide();
            form.Show();
        }

        private void subjectTextBox_TextChanged(object sender, EventArgs e)
        {
            successLbl.Hide();
        }

        private void sendTileBtn_Click(object sender, EventArgs e)
        {
            this.emailPanel.BringToFront();

        }

        private void emailLbl_Click(object sender, EventArgs e)
        {

        }
    }
}
