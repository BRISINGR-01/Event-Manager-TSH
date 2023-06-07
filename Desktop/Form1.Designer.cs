namespace Desktop
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            NavBar = new FlowLayoutPanel();
            BranchesBtn = new Button();
            AccountsBtn = new Button();
            LogOutBtn = new Button();
            panelContainer = new Panel();
            userNameLabel = new Label();
            NavBar.SuspendLayout();
            SuspendLayout();
            // 
            // NavBar
            // 
            NavBar.AutoSize = true;
            NavBar.BackColor = Color.FromArgb(46, 46, 46);
            NavBar.Controls.Add(userNameLabel);
            NavBar.Controls.Add(BranchesBtn);
            NavBar.Controls.Add(AccountsBtn);
            NavBar.Controls.Add(LogOutBtn);
            NavBar.Dock = DockStyle.Left;
            NavBar.Location = new Point(0, 0);
            NavBar.Margin = new Padding(3, 4, 3, 4);
            NavBar.Name = "NavBar";
            NavBar.Size = new Size(212, 570);
            NavBar.TabIndex = 0;
            // 
            // BranchesBtn
            // 
            BranchesBtn.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BranchesBtn.FlatStyle = FlatStyle.Flat;
            BranchesBtn.Font = new Font("Microsoft Sans Serif", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            BranchesBtn.ForeColor = Color.White;
            BranchesBtn.Location = new Point(0, 48);
            BranchesBtn.Margin = new Padding(0);
            BranchesBtn.Name = "BranchesBtn";
            BranchesBtn.Size = new Size(212, 79);
            BranchesBtn.TabIndex = 0;
            BranchesBtn.Text = "Branches";
            BranchesBtn.UseVisualStyleBackColor = true;
            BranchesBtn.Click += BranchesBtn_Click;
            // 
            // AccountsBtn
            // 
            AccountsBtn.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AccountsBtn.FlatStyle = FlatStyle.Flat;
            AccountsBtn.Font = new Font("Microsoft Sans Serif", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            AccountsBtn.ForeColor = Color.White;
            AccountsBtn.Location = new Point(0, 127);
            AccountsBtn.Margin = new Padding(0);
            AccountsBtn.Name = "AccountsBtn";
            AccountsBtn.Size = new Size(212, 79);
            AccountsBtn.TabIndex = 1;
            AccountsBtn.Text = "Accounts";
            AccountsBtn.UseVisualStyleBackColor = true;
            AccountsBtn.Click += AccuntsBtn_Click;
            // 
            // LogOutBtn
            // 
            LogOutBtn.FlatStyle = FlatStyle.Flat;
            LogOutBtn.Font = new Font("Microsoft Sans Serif", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            LogOutBtn.ForeColor = Color.White;
            LogOutBtn.Location = new Point(0, 206);
            LogOutBtn.Margin = new Padding(0);
            LogOutBtn.Name = "LogOutBtn";
            LogOutBtn.Size = new Size(212, 73);
            LogOutBtn.TabIndex = 2;
            LogOutBtn.Text = "Log Out";
            LogOutBtn.UseVisualStyleBackColor = true;
            LogOutBtn.Click += LogOutBtn_Click;
            // 
            // panelContainer
            // 
            panelContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelContainer.BackColor = Color.Transparent;
            panelContainer.Location = new Point(210, 0);
            panelContainer.Margin = new Padding(3, 4, 3, 4);
            panelContainer.Name = "panelContainer";
            panelContainer.Size = new Size(828, 570);
            panelContainer.TabIndex = 1;
            // 
            // userNameLabel
            // 
            userNameLabel.AutoSize = true;
            userNameLabel.BackColor = Color.FromArgb(46, 46, 46);
            userNameLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            userNameLabel.ForeColor = SystemColors.ButtonHighlight;
            userNameLabel.Location = new Point(10, 10);
            userNameLabel.Margin = new Padding(10);
            userNameLabel.Name = "userNameLabel";
            userNameLabel.Size = new Size(58, 28);
            userNameLabel.TabIndex = 3;
            userNameLabel.Text = "Hello";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(920, 570);
            Controls.Add(NavBar);
            Controls.Add(panelContainer);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "Administration Panel";
            NavBar.ResumeLayout(false);
            NavBar.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel NavBar;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Button BranchesBtn;
        private System.Windows.Forms.Button AccountsBtn;
        private Button LogOutBtn;
        private Label userNameLabel;
    }
}