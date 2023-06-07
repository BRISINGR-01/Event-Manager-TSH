using Desktop.Forms;
using Domain.Managers;
using Infrastructure;
using Infrastructure.DatabaseManagers;
using Logic.Models;
using Shared;
using Shared.Errors;

namespace Desktop
{
    public partial class Form1 : Form
    {
        private Manager? manager;
        public Form1()
        {
            InitializeComponent();
            Guid? currentUserId = Manager.Local.GetLastLoggedAs();
            if (currentUserId == null)
            {
                SwitchPanel<LogIn>();
            }
            else
            {
                var result = Manager.UserGet((Guid)currentUserId);

                if (result.IsSuccessful && result.Value != null)
                {
                    manager = new(result.Value);
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
            UserControl page;
            try
            {
                page = typeof(T).Name switch
                {
                    "Accounts" => manager != null ? new Accounts(manager) : throw new Exception(),
                    "Branches" => manager != null ? new Branches(manager.Branch) : throw new Exception(),
                    "LogIn" => new LogIn(),
                    _ => throw new Exception()
                };
            } catch
            {
                page = new LogIn();
            }

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

        private bool CheckCredentials(string userName, string password)
        {
            var result = Manager.CheckCredentials(userName, password);
            
            if (result.IsSuccessful)
            {
                User? user = result.Value;

                if (user != null)
                {
                    Manager.Local.SetLastLoggedAs(user.Id);
                    manager = new(user);
                    userNameLabel.Text = user.UserName;
                    SwitchPanel<Branches>();
                }
                return user == null;
            } else
            {
                MessageBox.Show(result.Error);
                return false;
            }
        }
        private void SelectBranch(Branch branch)
        {
            SwitchPanel<Accounts>(branch);
        }

        private void LogOutBtn_Click(object sender, EventArgs e)
        {
            Manager.Local.SetLastLoggedAs(null);
            SwitchPanel<LogIn>();
        }
    }
}