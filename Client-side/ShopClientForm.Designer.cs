namespace Client_side
{
    partial class ShopClientForm
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
            this.txtOrders = new System.Windows.Forms.TextBox();
            this.cmbProducts = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnPurchase = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtOrders
            // 
            this.txtOrders.Location = new System.Drawing.Point(13, 13);
            this.txtOrders.Multiline = true;
            this.txtOrders.Name = "txtOrders";
            this.txtOrders.ReadOnly = true;
            this.txtOrders.Size = new System.Drawing.Size(209, 251);
            this.txtOrders.TabIndex = 0;
            // 
            // cmbProducts
            // 
            this.cmbProducts.FormattingEnabled = true;
            this.cmbProducts.Items.AddRange(new object[] {
            "Apple",
            "Banana",
            "Cucumber",
            "Tomato",
            "Pear"});
            this.cmbProducts.Location = new System.Drawing.Point(228, 13);
            this.cmbProducts.Name = "cmbProducts";
            this.cmbProducts.Size = new System.Drawing.Size(157, 21);
            this.cmbProducts.TabIndex = 1;
            this.cmbProducts.Text = "Apple";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(229, 241);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnPurchase
            // 
            this.btnPurchase.Location = new System.Drawing.Point(310, 241);
            this.btnPurchase.Name = "btnPurchase";
            this.btnPurchase.Size = new System.Drawing.Size(75, 23);
            this.btnPurchase.TabIndex = 3;
            this.btnPurchase.Text = "Purchase";
            this.btnPurchase.UseVisualStyleBackColor = true;
            this.btnPurchase.Click += new System.EventHandler(this.btnPurchase_Click);
            // 
            // ShopClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 276);
            this.Controls.Add(this.btnPurchase);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.cmbProducts);
            this.Controls.Add(this.txtOrders);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimizeBox = false;
            this.Name = "ShopClientForm";
            this.Text = "ShopClient";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ShopClientForm_FormClosed);
            this.Load += new System.EventHandler(this.ShopClientForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOrders;
        private System.Windows.Forms.ComboBox cmbProducts;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnPurchase;
    }
}

