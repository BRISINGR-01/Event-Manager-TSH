namespace Desktop.Forms
{
    partial class RenameBranch
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
            addBtn = new Button();
            renameTb = new TextBox();
            closeBtn = new Button();
            SuspendLayout();
            // 
            // addBtn
            // 
            addBtn.Location = new Point(234, 131);
            addBtn.Name = "addBtn";
            addBtn.Size = new Size(94, 29);
            addBtn.TabIndex = 0;
            addBtn.Text = "Done";
            addBtn.UseVisualStyleBackColor = true;
            addBtn.Click += addBtn_Click;
            // 
            // renameTb
            // 
            renameTb.Anchor = AnchorStyles.Right;
            renameTb.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            renameTb.Location = new Point(44, 12);
            renameTb.Name = "renameTb";
            renameTb.PlaceholderText = "Enter branch name";
            renameTb.Size = new Size(254, 41);
            renameTb.TabIndex = 1;
            // 
            // closeBtn
            // 
            closeBtn.Location = new Point(12, 131);
            closeBtn.Name = "closeBtn";
            closeBtn.Size = new Size(94, 29);
            closeBtn.TabIndex = 2;
            closeBtn.Text = "Cancel";
            closeBtn.UseVisualStyleBackColor = true;
            closeBtn.Click += closeBtn_Click;
            // 
            // RenameBranch
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(340, 172);
            Controls.Add(closeBtn);
            Controls.Add(renameTb);
            Controls.Add(addBtn);
            Name = "RenameBranch";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button addBtn;
        private TextBox renameTb;
        private Button closeBtn;
    }
}
