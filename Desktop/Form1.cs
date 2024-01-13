using Desktop.Forms;
using Infrastructure;
using Logic.Configuration;
using Logic.Models;
using Logic.Utilities;

namespace Desktop
{
    public partial class App : Form
    {
        private Manager manager;
        private HashingConfig hashingConfig;
        public App(Manager manager, HashingConfig hashingConfig)
        {
            this.hashingConfig = hashingConfig;
            this.manager = manager;
            InitializeComponent();
            Result<Guid> getLastLoggedAs = manager.Local.GetLastLoggedAs();
            //Result<Guid> getLastLoggedAs = Result<Guid>.From(() => manager.User.GetAll().Value.Find(w => w.Role == Shared.Enums.UserRole.Administrator)!.Id);
            if (getLastLoggedAs.IsUnSuccessful)
            {
                SwitchPanel<LogIn>();
            }
            else
            {
                var result = manager.User.GetById(getLastLoggedAs.Value);

                if (result.IsSuccessful)
                {
                    userNameLabel.Text = result.Value.UserName;
                    SwitchPanel<Branches>();
                }
                else
                {
                    SwitchPanel<LogIn>();
                }
            }
        }

        private void SwitchPanel<T>(Branch? branch = null) where T : UserControl
        {
            UserControl page = typeof(T).Name switch
            {
                "Accounts" => new Accounts(manager, hashingConfig),
                "Branches" => new Branches(manager.Branch),
                _ => new LogIn()
            };

            if (page is LogIn logIn)
            {
                panelContainer.Dock = DockStyle.Fill;
                logIn.CheckCredentials += CheckCredentials;
                NavBar.Visible = false;
            }
            else
            {
                panelContainer.Dock = DockStyle.None;
                NavBar.Visible = true;
            }
            if (page is Branches branches)
            {
                branches.SelectBranch += SelectBranch;
            }
            else if (page is Accounts accounts && branch != null)
            {
                accounts.SetDefaultBranch(branch);
            }

            var buttons = new List<Button>
            {
                AccountsBtn,
                BranchesBtn,
                LogOutBtn
            };

            foreach (var btn in buttons)
            {
                btn.BackColor = Color.FromArgb(46, 46, 46);
            }

            switch (typeof(T).Name)
            {
                case "LogIn":
                    LogOutBtn.BackColor = Color.FromArgb(80, 80, 80);
                    break;
                case "Accounts":
                    AccountsBtn.BackColor = Color.FromArgb(80, 80, 80);
                    break;
                case "Branches":
                    BranchesBtn.BackColor = Color.FromArgb(80, 80, 80);
                    break;
            }

            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(page);
            page.Dock = DockStyle.Fill;
            page.BringToFront();
        }

        private void BranchesBtn_Click(object sender, EventArgs e)
        {
            SwitchPanel<Branches>();
        }

        private void AccuntsBtn_Click(object sender, EventArgs e)
        {
            SwitchPanel<Accounts>();
        }

        private bool CheckCredentials(string email, string password)
        {
            var result = manager.Credentials.GetByEmail(email);

            if (result.IsUnSuccessful)
            {
                MessageBox.Show(result.ErrorMessage);
                return false;
            }

            var credentials = result.Value;
            credentials.Configure(hashingConfig);
            credentials.VerifyPassword(password);

            manager.Local.SetLastLoggedAs(credentials.Id);

            var userRes = manager.User.GetById(credentials.Id);
            if (userRes.IsUnSuccessful) return false;

            SwitchPanel<Branches>();

            return true;
        }

        private void SelectBranch(Branch branch)
        {
            SwitchPanel<Accounts>(branch);
        }

        private void LogOutBtn_Click(object sender, EventArgs e)
        {
            manager.Local.SetLastLoggedAs(null);
            SwitchPanel<LogIn>();
        }
    }
}