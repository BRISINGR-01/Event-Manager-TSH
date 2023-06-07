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
    public partial class AddBranch : Form
    {
        new public string? Name { get; private set; }
        public AddBranch()
        {
            InitializeComponent();
            this.ActiveControl = searchBox;
            Name = null;
        }

        private void addBtn_Click(object sender, EventArgs e)
        {

            Name = searchBox.Text;
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
