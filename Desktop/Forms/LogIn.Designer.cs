namespace Desktop.Forms
{
    partial class LogIn
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
            LogInBtn = new Button();
            label1 = new Label();
            userNameTextBox = new TextBox();
            label2 = new Label();
            label3 = new Label();
            PasswordTextBox = new TextBox();
            errorLabel = new Label();
            SuspendLayout();
            // 
            // LogInBtn
            // 
            LogInBtn.BackColor = Color.FromArgb(255, 221, 0);
            LogInBtn.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            LogInBtn.ForeColor = Color.Black;
            LogInBtn.Location = new Point(318, 409);
            LogInBtn.Margin = new Padding(3, 4, 3, 4);
            LogInBtn.Name = "LogInBtn";
            LogInBtn.Size = new Size(180, 55);
            LogInBtn.TabIndex = 0;
            LogInBtn.Text = "Log In";
            LogInBtn.UseVisualStyleBackColor = false;
            LogInBtn.Click += LogInBtn_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.White;
            label1.Location = new Point(262, 183);
            label1.Name = "label1";
            label1.Size = new Size(66, 25);
            label1.TabIndex = 1;
            label1.Text = "Email:";
            // 
            // userNameTextBox
            // 
            userNameTextBox.BackColor = Color.FromArgb(255, 221, 0);
            userNameTextBox.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            userNameTextBox.ForeColor = Color.Black;
            userNameTextBox.Location = new Point(267, 212);
            userNameTextBox.Margin = new Padding(3, 4, 3, 4);
            userNameTextBox.Name = "userNameTextBox";
            userNameTextBox.Size = new Size(295, 30);
            userNameTextBox.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Microsoft Sans Serif", 19.8F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.White;
            label2.Location = new Point(367, 73);
            label2.Name = "label2";
            label2.Size = new Size(106, 38);
            label2.TabIndex = 3;
            label2.Text = "Log In";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = Color.White;
            label3.Location = new Point(262, 285);
            label3.Name = "label3";
            label3.Size = new Size(104, 25);
            label3.TabIndex = 4;
            label3.Text = "Password:";
            // 
            // PasswordTextBox
            // 
            PasswordTextBox.BackColor = Color.FromArgb(255, 221, 0);
            PasswordTextBox.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            PasswordTextBox.ForeColor = Color.Black;
            PasswordTextBox.Location = new Point(267, 320);
            PasswordTextBox.Margin = new Padding(3, 4, 3, 4);
            PasswordTextBox.Name = "PasswordTextBox";
            PasswordTextBox.Size = new Size(295, 30);
            PasswordTextBox.TabIndex = 5;
            // 
            // errorLabel
            // 
            errorLabel.AutoSize = true;
            errorLabel.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            errorLabel.ForeColor = Color.White;
            errorLabel.Location = new Point(267, 354);
            errorLabel.Name = "errorLabel";
            errorLabel.Size = new Size(234, 20);
            errorLabel.TabIndex = 6;
            errorLabel.Text = "Wrong username or password";
            // 
            // LogIn
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(46, 46, 46);
            Controls.Add(errorLabel);
            Controls.Add(PasswordTextBox);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(userNameTextBox);
            Controls.Add(label1);
            Controls.Add(LogInBtn);
            Margin = new Padding(3, 4, 3, 4);
            Name = "LogIn";
            Size = new Size(759, 576);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button LogInBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox userNameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private Label errorLabel;
    }
}
