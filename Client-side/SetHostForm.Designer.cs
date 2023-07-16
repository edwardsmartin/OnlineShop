namespace Client_side
{
    partial class SetHostForm
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
            this.lblHostName = new System.Windows.Forms.Label();
            this.txtHostName = new System.Windows.Forms.TextBox();
            this.btnSetHost = new System.Windows.Forms.Button();
            this.lblAccountNo = new System.Windows.Forms.Label();
            this.txtAccountNo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblHostName
            // 
            this.lblHostName.AutoSize = true;
            this.lblHostName.Location = new System.Drawing.Point(9, 15);
            this.lblHostName.Name = "lblHostName";
            this.lblHostName.Size = new System.Drawing.Size(60, 13);
            this.lblHostName.TabIndex = 2;
            this.lblHostName.Text = "&Host Name";
            // 
            // txtHostName
            // 
            this.txtHostName.Location = new System.Drawing.Point(91, 12);
            this.txtHostName.Name = "txtHostName";
            this.txtHostName.Size = new System.Drawing.Size(167, 20);
            this.txtHostName.TabIndex = 3;
            this.txtHostName.Text = "localhost";
            this.txtHostName.TextChanged += new System.EventHandler(this.txtHostName_TextChanged);
            // 
            // btnSetHost
            // 
            this.btnSetHost.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSetHost.Enabled = false;
            this.btnSetHost.Location = new System.Drawing.Point(12, 86);
            this.btnSetHost.Name = "btnSetHost";
            this.btnSetHost.Size = new System.Drawing.Size(75, 23);
            this.btnSetHost.TabIndex = 6;
            this.btnSetHost.Text = "Set Host";
            this.btnSetHost.UseVisualStyleBackColor = true;
            this.btnSetHost.Click += new System.EventHandler(this.btnSetHost_Click);
            // 
            // lblAccountNo
            // 
            this.lblAccountNo.AutoSize = true;
            this.lblAccountNo.Location = new System.Drawing.Point(9, 50);
            this.lblAccountNo.Name = "lblAccountNo";
            this.lblAccountNo.Size = new System.Drawing.Size(67, 13);
            this.lblAccountNo.TabIndex = 4;
            this.lblAccountNo.Text = "&Account No.";
            // 
            // txtAccountNo
            // 
            this.txtAccountNo.Location = new System.Drawing.Point(91, 50);
            this.txtAccountNo.Name = "txtAccountNo";
            this.txtAccountNo.Size = new System.Drawing.Size(167, 20);
            this.txtAccountNo.TabIndex = 5;
            this.txtAccountNo.TextChanged += new System.EventHandler(this.txtAccountNo_TextChanged);
            // 
            // SetHostForm
            // 
            this.AcceptButton = this.btnSetHost;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(271, 121);
            this.Controls.Add(this.txtAccountNo);
            this.Controls.Add(this.lblAccountNo);
            this.Controls.Add(this.btnSetHost);
            this.Controls.Add(this.txtHostName);
            this.Controls.Add(this.lblHostName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SetHostForm";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHostName;
        private System.Windows.Forms.TextBox txtHostName;
        private System.Windows.Forms.Button btnSetHost;
        private System.Windows.Forms.Label lblAccountNo;
        private System.Windows.Forms.TextBox txtAccountNo;
    }
}

