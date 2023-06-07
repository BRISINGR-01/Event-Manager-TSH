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
    public partial class RenameBranch : Form
    {
        new public string? Name { get; private set; }
        public RenameBranch(string prevName)
        {
            InitializeComponent();
            renameTb.Text = prevName;
            Name = null;
        }

        private void addBtn_Click(object sender, EventArgs e)
        {

            Name = renameTb.Text;
            if (Name == string.Empty)
            {
                MessageBox.Show("Please provide a name");
            }
            else
            {
                this.Close();
            }
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
