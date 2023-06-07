namespace Desktop
{
    partial class Branches
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
            branchesListBox = new ListBox();
            addBranchBtn = new Button();
            removeBtn = new Button();
            searchBox = new TextBox();
            editBranch = new Button();
            SuspendLayout();
            // 
            // branchesListBox
            // 
            branchesListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            branchesListBox.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            branchesListBox.FormattingEnabled = true;
            branchesListBox.ItemHeight = 31;
            branchesListBox.Location = new Point(0, 60);
            branchesListBox.Name = "branchesListBox";
            branchesListBox.Size = new Size(686, 376);
            branchesListBox.TabIndex = 0;
            branchesListBox.SelectedIndexChanged += branchesListBox_SelectedIndexChanged;
            branchesListBox.DoubleClick += branchesListBox_DoubleClick;
            // 
            // addBranchBtn
            // 
            addBranchBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            addBranchBtn.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            addBranchBtn.Location = new Point(13, 371);
            addBranchBtn.Name = "addBranchBtn";
            addBranchBtn.Size = new Size(59, 51);
            addBranchBtn.TabIndex = 1;
            addBranchBtn.Text = "+";
            addBranchBtn.TextAlign = ContentAlignment.TopCenter;
            addBranchBtn.UseVisualStyleBackColor = true;
            addBranchBtn.Click += addBranchBtn_Click;
            // 
            // removeBtn
            // 
            removeBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            removeBtn.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            removeBtn.Location = new Point(78, 371);
            removeBtn.Name = "removeBtn";
            removeBtn.Size = new Size(97, 51);
            removeBtn.TabIndex = 2;
            removeBtn.Text = "Remove";
            removeBtn.UseVisualStyleBackColor = true;
            removeBtn.Click += removeBtn_Click;
            // 
            // searchBox
            // 
            searchBox.Dock = DockStyle.Top;
            searchBox.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            searchBox.Location = new Point(0, 0);
            searchBox.Name = "searchBox";
            searchBox.PlaceholderText = "Enter name";
            searchBox.Size = new Size(686, 39);
            searchBox.TabIndex = 3;
            searchBox.TextChanged += textBox1_TextChanged;
            // 
            // editBranch
            // 
            editBranch.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            editBranch.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            editBranch.Location = new Point(181, 371);
            editBranch.Name = "editBranch";
            editBranch.Size = new Size(87, 51);
            editBranch.TabIndex = 4;
            editBranch.Text = "Edit";
            editBranch.UseVisualStyleBackColor = true;
            editBranch.Click += editBranch_Click;
            // 
            // Branches
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(editBranch);
            Controls.Add(searchBox);
            Controls.Add(removeBtn);
            Controls.Add(addBranchBtn);
            Controls.Add(branchesListBox);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Branches";
            Size = new Size(686, 436);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox branchesListBox;
        private Button addBranchBtn;
        private Button removeBtn;
        private TextBox searchBox;
        private Button editBranch;
    }
}
