using Desktop.Forms;
using Domain.Managers;
using Logic.Models;

namespace Desktop
{
    public partial class Branches : UserControl
    {
        private List<Branch> branches;
        private BranchManager branchManager;
        public event SelectBranchArgs? SelectBranch;
        public delegate void SelectBranchArgs(Branch branch);

        public Branches(BranchManager branchManager)
        {
            this.branchManager = branchManager;
            InitializeComponent();
            removeBtn.Visible = false;
            editBranch.Visible = false;
            var res = branchManager.GetAll();
            if (res.IsSuccessful)
            {
                branches = res.Value;
            }
            else
            {
                MessageBox.Show(res.ErrorMessage);
                branches = new();
            }
            PopulateBranchesList();
        }
        private void PopulateBranchesList()
        {
            branchesListBox.Items.Clear();
            foreach (var branch in branches)
            {
                branchesListBox.Items.Add(branch);
            }
        }

        private void addBranchBtn_Click(object sender, EventArgs e)
        {
            AddBranch form = new();
            form.ShowDialog();
            if (string.IsNullOrEmpty(form.Name)) return;

            Branch newBranch = new(form.Name);
            var res = branchManager.Create(newBranch);
            if (res.IsUnSuccessful)
            {
                MessageBox.Show(res.Exception.Message);
                return;
            }

            int index = branches.FindIndex(branch => newBranch.Name[0].CompareTo(branch.Name[0]) <= 0);
            if (index == -1)
            {
                branches.Add(newBranch);
                branchesListBox.Items.Add(newBranch);
                return;
            }

            branchesListBox.Items.Insert(index, newBranch);
        }

        private void removeBtn_Click(object sender, EventArgs e)
        {
            int index = branchesListBox.SelectedIndex;
            if (index != -1)
            {
                var res = branchManager.Delete(branches[index].Id);
                if (res.IsSuccessful)
                {
                    branches.RemoveAt(index);
                    branchesListBox.Items.RemoveAt(index);
                }
                else
                {
                    MessageBox.Show(res.Exception.Message);
                }
            }
            else
            {
                removeBtn.Enabled = false;
                editBranch.Enabled = false;
            }
        }

        private void branchesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            removeBtn.Visible = true;
            editBranch.Visible = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            removeBtn.Visible = false;
            editBranch.Visible = false;
            var res = branchManager.GetAll();
            if (res.IsSuccessful)
            {
                branches = res.Value;
                PopulateBranchesList();
            }
            else
            {
                MessageBox.Show(res.ErrorMessage);
            }
        }

        private void editBranch_Click(object sender, EventArgs e)
        {
            int index = branchesListBox.SelectedIndex;
            if (index == -1 || branches.Count < index)
            {
                removeBtn.Enabled = false;
                editBranch.Enabled = false;
                return;
            }

            Branch branch = branches[index];

            RenameBranch form = new(branch.Name);
            form.ShowDialog();
            if (string.IsNullOrEmpty(form.Name)) return;

            branches[index] = new Branch(branch.Id, form.Name);
            var res = branchManager.Update(branches[index]);
            if (res.IsSuccessful)
            {
                branchesListBox.Items[index] = branches[index];
            }
            else
            {
                MessageBox.Show(res.Exception.Message);
            }
        }

        private void branchesListBox_DoubleClick(object sender, EventArgs e)
        {
            int index = branchesListBox.SelectedIndex;
            if (index == -1 || branches.Count < index)
            {
                removeBtn.Enabled = false;
                editBranch.Enabled = false;
                return;
            }

            Branch branch = branches[index];

            SelectBranch?.Invoke(branch);
        }
    }
}
