namespace Desktop
{
    partial class Accounts
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            searchBox = new TextBox();
            branchCb = new ComboBox();
            UsersList = new ListBox();
            editBranch = new Button();
            removeBtn = new Button();
            addUserBtn = new Button();
            SuspendLayout();
            // 
            // branchCb
            // 
            branchCb.Dock = DockStyle.Top;
            branchCb.DropDownStyle = ComboBoxStyle.DropDownList;
            branchCb.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            branchCb.FormattingEnabled = true;
            branchCb.Items.AddRange(new object[] { "All" });
            branchCb.Location = new Point(0, 39);
            branchCb.Name = "branchCb";
            branchCb.Size = new Size(795, 39);
            branchCb.TabIndex = 1;
            branchCb.SelectedIndexChanged += branchCb_SelectedIndexChanged;
            // 
            // UsersList
            // 
            UsersList.Dock = DockStyle.Fill;
            UsersList.Font = new Font("Arial", 15F, FontStyle.Regular, GraphicsUnit.Point);
            UsersList.FormattingEnabled = true;
            UsersList.ItemHeight = 28;
            UsersList.Location = new Point(0, 78);
            UsersList.Margin = new Padding(0);
            UsersList.Name = "UsersList";
            UsersList.Size = new Size(795, 475);
            UsersList.TabIndex = 2;
            UsersList.SelectedIndexChanged += UsersList_SelectedIndexChanged;
            UsersList.DoubleClick += UsersList_DoubleClick;
            // 
            // editBranch
            // 
            editBranch.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            editBranch.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            editBranch.Location = new Point(181, 488);
            editBranch.Name = "editBranch";
            editBranch.Size = new Size(87, 51);
            editBranch.TabIndex = 7;
            editBranch.Text = "Edit";
            editBranch.UseVisualStyleBackColor = true;
            editBranch.Click += editBranch_Click;
            // 
            // removeBtn
            // 
            removeBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            removeBtn.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            removeBtn.Location = new Point(78, 488);
            removeBtn.Name = "removeBtn";
            removeBtn.Size = new Size(97, 51);
            removeBtn.TabIndex = 6;
            removeBtn.Text = "Remove";
            removeBtn.UseVisualStyleBackColor = true;
            removeBtn.Click += removeBtn_Click;
            // 
            // addUserBtn
            // 
            addUserBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            addUserBtn.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            addUserBtn.Location = new Point(13, 488);
            addUserBtn.Name = "addUserBtn";
            addUserBtn.Size = new Size(59, 51);
            addUserBtn.TabIndex = 5;
            addUserBtn.Text = "+";
            addUserBtn.TextAlign = ContentAlignment.TopCenter;
            addUserBtn.UseVisualStyleBackColor = true;
            addUserBtn.Click += addUserBtn_Click;
            // 
            // Accounts
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(editBranch);
            Controls.Add(removeBtn);
            Controls.Add(addUserBtn);
            Controls.Add(UsersList);
            Controls.Add(branchCb);
            Controls.Add(searchBox);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Accounts";
            Size = new Size(795, 553);
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private TextBox searchBox;
        private ComboBox branchCb;
        private ListBox UsersList;
        private Button editBranch;
        private Button removeBtn;
        private Button addUserBtn;
    }
}
