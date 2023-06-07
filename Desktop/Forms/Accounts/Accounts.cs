using Desktop.Forms;
using Desktop.Forms.Accounts;
using Domain.Managers;
using Infrastructure;
using Logic.Models;
using Shared.Errors;

namespace Desktop
{
    public partial class Accounts : UserControl
    {
        private List<User> users;
        private readonly List<Branch> branches;
        private Manager manager;
        private Guid? BranchId
        {
            get => branchCb.SelectedIndex < 1 ? null : ((Branch)branchCb.SelectedItem).Id;
        }
        public Accounts(Manager manager)
        {
            InitializeComponent();
            editBranch.Visible = false;
            removeBtn.Visible = false;

            this.manager = manager;

            var res = manager.Branch.GetAll();
            if (res.IsSuccessful)
            {
                branches = res.Value!;
            } else
            {
                MessageBox.Show(res.Error);
                branches = new();
            }
            
            foreach (var branch in branches)
            {
                branchCb.Items.Add(branch);
            }
            if (branches.Count > 0) branchCb.SelectedIndex = 0;

            var result = manager.User.GetAll();
            if (result.IsSuccessful)
            {
                users = result.Value!;
            } else
            {
                MessageBox.Show(result.Error);
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
            var result = manager.User.GetAll(BranchId);
            
            if (result.IsSuccessful)
            {
                users = result.Value!;
                PopulateUsers();
            }
            else
            {
                MessageBox.Show(result.Error);
            }
        }

        private void addUserBtn_Click(object sender, EventArgs e)
        {
            AddEditAccount form;
            if (BranchId != null)
            {
                form = new(branches, ((Branch)branchCb.SelectedItem));
            }
            else
            {
                form = new(branches);
            }
            form.ShowDialog();
            if (form.User != null)
            {
                var res = manager.User.Create(form.User);
                if (!res.IsSuccessful)
                {
                    MessageBox.Show(res.Error);
                } else
                {
                    var result = manager.User.GetAll(BranchId);
                    
                    if (result.IsSuccessful)
                    {
                        users = result.Value!;
                    }
                    else
                    {
                        MessageBox.Show(result.Error);
                        users = new();
                    }
                    PopulateUsers();
                }
            }
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
                } else
                {
                    MessageBox.Show(res.Error);
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

            AddEditAccount form;
            if (BranchId != null)
            {
                form = new(user, branches, (Branch)branchCb.SelectedItem);
            }
            else
            {
                form = new(user, branches, branches[0]);
            }
            form.ShowDialog();
            if (form.User == null) return;

            user = form.User;

            var res = manager.User.Update(user);
            if (res.IsSuccessful)
            {
                var result = manager.User.GetAll(BranchId);

                if (result.IsSuccessful)
                {
                    users = result.Value!;
                }
                else
                {
                    MessageBox.Show(result.Error);
                    users = new();
                }
                PopulateUsers();
            } else
            {
                MessageBox.Show(res.Error);
            }
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
