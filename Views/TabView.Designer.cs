namespace projectIost.Views
{
    partial class TabView
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
            tabControl1 = new TabControl();
            InventoryTab = new TabPage();
            OrderTab = new TabPage();
            SupplyTab = new TabPage();
            AnalyticsTab = new TabPage();
            tabControl1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(InventoryTab);
            tabControl1.Controls.Add(OrderTab);
            tabControl1.Controls.Add(SupplyTab);
            tabControl1.Controls.Add(AnalyticsTab);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1967, 1150);
            tabControl1.TabIndex = 0;
            // 
            // InventoryTab
            // 
            InventoryTab.Location = new Point(8, 46);
            InventoryTab.Name = "InventoryTab";
            InventoryTab.Padding = new Padding(3);
            InventoryTab.Size = new Size(1951, 1096);
            InventoryTab.TabIndex = 0;
            InventoryTab.Text = "Inventory";
            // 
            // OrderTab
            // 
            OrderTab.Location = new Point(8, 46);
            OrderTab.Name = "OrderTab";
            OrderTab.Padding = new Padding(3);
            OrderTab.Size = new Size(1951, 1096);
            OrderTab.TabIndex = 1;
            OrderTab.Text = "Order";
            OrderTab.UseVisualStyleBackColor = true;
            // 
            // SupplyTab
            // 
            SupplyTab.Location = new Point(8, 46);
            SupplyTab.Name = "SupplyTab";
            SupplyTab.Size = new Size(1951, 1096);
            SupplyTab.TabIndex = 2;
            SupplyTab.Text = "Supply";
            SupplyTab.UseVisualStyleBackColor = true;
            // 
            // AnalyticsTab
            // 
            AnalyticsTab.Location = new Point(8, 46);
            AnalyticsTab.Name = "AnalyticsTab";
            AnalyticsTab.Size = new Size(1951, 1096);
            AnalyticsTab.TabIndex = 3;
            AnalyticsTab.Text = "Analytics";
            AnalyticsTab.UseVisualStyleBackColor = true;
            // 
            // TabView
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2011, 1170);
            Controls.Add(tabControl1);
            Name = "TabView";
            Text = "I LOVE IOST";
            //Load += this.TabView_Load;
            tabControl1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage InventoryTab;
        private TabPage OrderTab;
        private TabPage SupplyTab;
        private TabPage AnalyticsTab;
    }
}