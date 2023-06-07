using Domain.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop.Forms
{
    public partial class LogIn : UserControl
    {
        public delegate bool CredentialsArgs(string userName, string password);
        public event CredentialsArgs? CheckCredentials;
        public LogIn()
        {
            InitializeComponent();
            errorLabel.Visible = false;
        }


        private void LogInBtn_Click(object sender, EventArgs e)
        {
            if (CheckCredentials?.Invoke(userNameTextBox.Text, PasswordTextBox.Text) ?? false)
            {
                errorLabel.Visible = true;
            }
        }
    }
}
