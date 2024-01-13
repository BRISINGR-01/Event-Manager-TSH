using Logic.Models;
using Shared.Enums;

namespace Desktop.Forms.Accounts
{
    public partial class AddEditAccount : Form
    {
        public User? User { get; private set; }
        public string Password { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public AddEditAccount(List<Branch> branches, Branch? selectedBranch = null)
        {
            InitializeComponent();
            User = null;

            foreach (var branch in branches)
            {
                BranchCb.Items.Add(branch);
            }
            if (selectedBranch == null && branches.Count > 0) selectedBranch = branches[0];
            BranchCb.SelectedItem = selectedBranch;
            foreach (var role in Enum.GetValues(typeof(UserRole)))
            {
                roleCb.Items.Add(role);
            }
            roleCb.SelectedIndex = roleCb.Items.IndexOf(UserRole.Student);

            this.Text = "Create student";
        }
        public AddEditAccount(User user, List<Branch> branches, Branch selectedBranch)
        {
            // edit
            InitializeComponent();
            User = user;

            foreach (var branch in branches)
            {
                BranchCb.Items.Add(branch);
            }
            foreach (var role in Enum.GetValues(typeof(UserRole)))
            {
                roleCb.Items.Add(role);
            }
            BranchCb.SelectedItem = selectedBranch;
            nameBox.Text = User.UserName;
            roleCb.SelectedIndex = roleCb.Items.IndexOf(user.Role);

            this.Text = "Edit student";
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            User = null;
            this.Close();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            if (roleCb.SelectedItem == null)
            {
                MessageBox.Show("Please select a valid role");
                return;
            }

            User = new(
                User?.Id ?? Guid.Empty,
                ((Branch)BranchCb.SelectedItem).Id,
                nameBox.Text,
                (UserRole)roleCb.SelectedItem
            );
            Password = PasswordBox.Text;
            Email = emailBox.Text;

            this.Close();
        }
    }
}
