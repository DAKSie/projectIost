namespace projectIost.Views
{
    partial class LoginView
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
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            btnLogin = new Button();
            lblInventory = new Label();
            lnkRegister = new LinkLabel();
            SuspendLayout();
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(65, 115);
            txtUsername.Margin = new Padding(3, 2, 3, 2);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "Username";
            txtUsername.Size = new Size(210, 23);
            txtUsername.TabIndex = 0;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(65, 167);
            txtPassword.Margin = new Padding(3, 2, 3, 2);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '1';
            txtPassword.PlaceholderText = "Password";
            txtPassword.Size = new Size(210, 23);
            txtPassword.TabIndex = 1;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(109, 215);
            btnLogin.Margin = new Padding(3, 2, 3, 2);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(116, 38);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "LOGIN";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // lblInventory
            // 
            lblInventory.AutoSize = true;
            lblInventory.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblInventory.Location = new Point(115, 47);
            lblInventory.Margin = new Padding(2, 0, 2, 0);
            lblInventory.Name = "lblInventory";
            lblInventory.Size = new Size(104, 45);
            lblInventory.TabIndex = 3;
            lblInventory.Text = "Login";
            lblInventory.TextAlign = ContentAlignment.TopCenter;
            // 
            // lnkRegister
            // 
            lnkRegister.AutoSize = true;
            lnkRegister.Location = new Point(65, 284);
            lnkRegister.Name = "lnkRegister";
            lnkRegister.Size = new Size(205, 15);
            lnkRegister.TabIndex = 4;
            lnkRegister.TabStop = true;
            lnkRegister.Text = "Don't have an account? Register now.";
            // 
            // LoginView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(340, 327);
            Controls.Add(lnkRegister);
            Controls.Add(lblInventory);
            Controls.Add(btnLogin);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            Margin = new Padding(2);
            Name = "LoginView";
            Text = "LoginView";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Label lblInventory;
        private LinkLabel lnkRegister;
    }
}