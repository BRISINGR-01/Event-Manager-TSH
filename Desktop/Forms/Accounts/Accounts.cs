using Desktop.Forms.Accounts;
using Infrastructure;
using Logic.Configuration;
using Logic.Models;

namespace Desktop
{
    public partial class Accounts : UserControl
    {
        private List<User> users;
        private readonly List<Branch> branches;
        private Manager manager;
        private HashingConfig config;
        private Guid? BranchId => branchCb.SelectedIndex < 1 ? null : ((Branch)branchCb.SelectedItem).Id;
        public Accounts(Manager manager, HashingConfig config)
        {
            InitializeComponent();
            editBranch.Visible = false;
            removeBtn.Visible = false;

            this.manager = manager;
            this.config = config;

            var res = manager.Branch.GetAll();
            if (res.IsSuccessful)
            {
                branches = res.Value;
            }
            else
            {
                MessageBox.Show(res.ErrorMessage);
                branches = new();
            }

            foreach (var branch in branches)
            {
                branchCb.Items.Add(branch);
            }
            if (branches.Count > 0) branchCb.SelectedIndex = 0;

            var result = manager.User.GetAllFromBranch();
            if (result.IsSuccessful)
            {
                users = result.Value;
            }
            else
            {
                MessageBox.Show(result.ErrorMessage);
                users = new();
            }
            PopulateUsers();
        }
        public void SetDefaultBranch(Branch? branch)
        {
            if (branch == null) branchCb.SelectedIndex = 0;

            branchCb.SelectedIndex = branches.FindIndex(b => b.Id == branch!.Id) + 1;
        }

        private void PopulateUsers()
        {
            UsersList.Items.Clear();
            foreach (var user in users.ToList())
            {
                UsersList.Items.Add(user);
            }
        }

        private void branchCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            var result = manager.User.GetAllFromBranch(BranchId);

            if (result.IsSuccessful)
            {
                users = result.Value;
                PopulateUsers();
            }
            else
            {
                MessageBox.Show(result.ErrorMessage);
            }
        }

        private void addUserBtn_Click(object sender, EventArgs e)
        {
            AddEditAccount form = BranchId != null ? new(branches, ((Branch)branchCb.SelectedItem)) : new(branches);
            form.ShowDialog();

            if (form.User == null) return;
            var res = manager.User.Create(form.User);

            if (res.IsUnSuccessful)
            {
                MessageBox.Show(res.ErrorMessage);
                return;
            }

            Credentials credentials = new(form.Email, form.Password, config);
            res = manager.Credentials.Create(credentials);

            if (res.IsUnSuccessful)
            {
                MessageBox.Show(res.ErrorMessage);
                return;
            }

            var result = manager.User.GetAllFromBranch(BranchId);

            if (result.IsSuccessful)
            {
                users = result.Value;
            }
            else
            {
                MessageBox.Show(result.ErrorMessage);
                users = new();
            }

            PopulateUsers();
        }

        private void removeBtn_Click(object sender, EventArgs e)
        {
            int index = UsersList.SelectedIndex;
            if (index == -1)
            {
                removeBtn.Enabled = false;
                editBranch.Enabled = false;
            }
            else
            {
                var res = manager.User.Delete(users[index].Id);
                if (res.IsSuccessful)
                {
                    users.RemoveAt(index);
                    UsersList.Items.RemoveAt(index);
                }
                else
                {
                    MessageBox.Show(res.Exception.Message);
                }
                res = manager.Credentials.Delete(users[index].Id);
                if (res.IsUnSuccessful)
                {
                    MessageBox.Show(res.Exception.Message);
                }
            }
        }

        private void editBranch_Click(object sender, EventArgs e)
        {
            int index = UsersList.SelectedIndex;
            if (index == -1 || users.Count < index)
            {
                removeBtn.Enabled = false;
                editBranch.Enabled = false;
                return;
            }

            User user = users[index];

            AddEditAccount form = new(user, branches, BranchId != null ? (Branch)branchCb.SelectedItem : branches[0]);

            if (form.ShowDialog() == DialogResult.Cancel || form.User == null) return;

            user = form.User;

            var res = manager.User.Update(user);
            if (res.IsUnSuccessful)
            {
                MessageBox.Show(res.Exception.Message);
                return;
            }

            res = manager.Credentials.Update(new Credentials(form.Email, form.Password, config, form.User.Id));
            if (res.IsUnSuccessful)
            {
                MessageBox.Show(res.Exception.Message);
                return;
            }

            var result = manager.User.GetAllFromBranch(BranchId);

            if (result.IsSuccessful)
            {
                users = result.Value;
            }
            else
            {
                MessageBox.Show(result.ErrorMessage);
                users = new();
            }
            PopulateUsers();
        }

        private void UsersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            editBranch.Visible = true;
            removeBtn.Visible = true;
        }

        private void UsersList_DoubleClick(object sender, EventArgs e)
        {
            editBranch_Click(sender, e);
        }
    }
}
