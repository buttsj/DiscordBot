namespace DiscordBot
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.enterCred = new System.Windows.Forms.Button();
            this.usernameBox = new System.Windows.Forms.TextBox();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.github = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // enterCred
            // 
            this.enterCred.Location = new System.Drawing.Point(12, 64);
            this.enterCred.Name = "enterCred";
            this.enterCred.Size = new System.Drawing.Size(75, 23);
            this.enterCred.TabIndex = 0;
            this.enterCred.Text = "Log In";
            this.enterCred.UseVisualStyleBackColor = true;
            this.enterCred.Click += new System.EventHandler(this.button1_Click);
            // 
            // usernameBox
            // 
            this.usernameBox.Location = new System.Drawing.Point(12, 12);
            this.usernameBox.Name = "usernameBox";
            this.usernameBox.Size = new System.Drawing.Size(165, 20);
            this.usernameBox.TabIndex = 1;
            this.usernameBox.Text = "Username";
            // 
            // passwordBox
            // 
            this.passwordBox.Location = new System.Drawing.Point(12, 38);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.PasswordChar = '*';
            this.passwordBox.Size = new System.Drawing.Size(165, 20);
            this.passwordBox.TabIndex = 2;
            this.passwordBox.Text = "Password";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(183, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(239, 160);
            this.listBox1.TabIndex = 3;
            // 
            // github
            // 
            this.github.AutoSize = true;
            this.github.Location = new System.Drawing.Point(9, 159);
            this.github.Name = "github";
            this.github.Size = new System.Drawing.Size(119, 13);
            this.github.TabIndex = 4;
            this.github.TabStop = true;
            this.github.Text = "Open source on GitHub";
            this.github.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.github_LinkClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GrayText;
            this.ClientSize = new System.Drawing.Size(433, 178);
            this.Controls.Add(this.github);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.passwordBox);
            this.Controls.Add(this.usernameBox);
            this.Controls.Add(this.enterCred);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Discord Bot";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button enterCred;
        private System.Windows.Forms.TextBox usernameBox;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.LinkLabel github;
    }
}

