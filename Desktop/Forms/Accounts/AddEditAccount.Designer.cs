namespace Desktop.Forms.Accounts
{
    partial class AddEditAccount
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
            label1 = new Label();
            nameBox = new TextBox();
            label2 = new Label();
            BranchCb = new ComboBox();
            PasswordBox = new TextBox();
            label3 = new Label();
            roleCb = new ComboBox();
            label4 = new Label();
            emailBox = new TextBox();
            label5 = new Label();
            closeBtn = new Button();
            addBtn = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(18, 83);
            label1.Name = "label1";
            label1.Size = new Size(87, 35);
            label1.TabIndex = 0;
            label1.Text = "Name:";
            // 
            // nameBox
            // 
            nameBox.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            nameBox.Location = new Point(169, 80);
            nameBox.Name = "nameBox";
            nameBox.Size = new Size(360, 41);
            nameBox.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(18, 27);
            label2.Name = "label2";
            label2.Size = new Size(96, 35);
            label2.TabIndex = 2;
            label2.Text = "Branch:";
            // 
            // BranchCb
            // 
            BranchCb.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            BranchCb.FormattingEnabled = true;
            BranchCb.Location = new Point(169, 27);
            BranchCb.Name = "BranchCb";
            BranchCb.Size = new Size(360, 43);
            BranchCb.TabIndex = 2;
            // 
            // PasswordBox
            // 
            PasswordBox.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            PasswordBox.Location = new Point(169, 138);
            PasswordBox.Name = "PasswordBox";
            PasswordBox.Size = new Size(360, 41);
            PasswordBox.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(18, 141);
            label3.Name = "label3";
            label3.Size = new Size(125, 35);
            label3.TabIndex = 4;
            label3.Text = "Password:";
            // 
            // roleCb
            // 
            roleCb.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            roleCb.FormattingEnabled = true;
            roleCb.Location = new Point(169, 200);
            roleCb.Name = "roleCb";
            roleCb.Size = new Size(360, 43);
            roleCb.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(18, 200);
            label4.Name = "label4";
            label4.Size = new Size(68, 35);
            label4.TabIndex = 6;
            label4.Text = "Role:";
            // 
            // emailBox
            // 
            emailBox.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            emailBox.Location = new Point(169, 261);
            emailBox.Name = "emailBox";
            emailBox.Size = new Size(360, 41);
            emailBox.TabIndex = 9;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(18, 264);
            label5.Name = "label5";
            label5.Size = new Size(80, 35);
            label5.TabIndex = 8;
            label5.Text = "Email:";
            // 
            // closeBtn
            // 
            closeBtn.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            closeBtn.Location = new Point(12, 343);
            closeBtn.Name = "closeBtn";
            closeBtn.Size = new Size(102, 49);
            closeBtn.TabIndex = 11;
            closeBtn.Text = "Cancel";
            closeBtn.UseVisualStyleBackColor = true;
            closeBtn.Click += closeBtn_Click;
            // 
            // addBtn
            // 
            addBtn.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            addBtn.Location = new Point(439, 343);
            addBtn.Name = "addBtn";
            addBtn.Size = new Size(106, 49);
            addBtn.TabIndex = 10;
            addBtn.Text = "Done";
            addBtn.UseVisualStyleBackColor = true;
            addBtn.Click += addBtn_Click;
            // 
            // AddEditAccount
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(557, 404);
            Controls.Add(closeBtn);
            Controls.Add(addBtn);
            Controls.Add(emailBox);
            Controls.Add(label5);
            Controls.Add(roleCb);
            Controls.Add(label4);
            Controls.Add(PasswordBox);
            Controls.Add(label3);
            Controls.Add(BranchCb);
            Controls.Add(label2);
            Controls.Add(nameBox);
            Controls.Add(label1);
            Name = "AddEditAccount";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox nameBox;
        private Label label2;
        private ComboBox BranchCb;
        private TextBox PasswordBox;
        private Label label3;
        private ComboBox roleCb;
        private Label label4;
        private TextBox emailBox;
        private Label label5;
        private Button closeBtn;
        private Button addBtn;
    }
}
