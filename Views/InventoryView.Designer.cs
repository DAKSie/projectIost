namespace projectIost.Views
{
    partial class InventoryView
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
            dgvInventory = new DataGridView();
            txtSearch = new TextBox();
            lblInventory = new Label();
            txtID = new TextBox();
            txtName = new TextBox();
            txtQuantity = new TextBox();
            txtCostPrice = new TextBox();
            txtRetailPrice = new TextBox();
            lblID = new Label();
            lblName = new Label();
            lblQuantity = new Label();
            lblCostPrice = new Label();
            lblRetailPrice = new Label();
            lblSearch = new Label();
            btnAdd = new Button();
            btnEdit = new Button();
            btnClear = new Button();
            btnDelete = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvInventory).BeginInit();
            SuspendLayout();
            // 
            // dgvInventory
            // 
            dgvInventory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvInventory.Location = new Point(804, 146);
            dgvInventory.Name = "dgvInventory";
            dgvInventory.RowHeadersWidth = 82;
            dgvInventory.Size = new Size(1140, 767);
            dgvInventory.TabIndex = 0;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(905, 82);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(584, 39);
            txtSearch.TabIndex = 1;
            // 
            // lblInventory
            // 
            lblInventory.AutoSize = true;
            lblInventory.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblInventory.Location = new Point(106, 67);
            lblInventory.Name = "lblInventory";
            lblInventory.Size = new Size(330, 86);
            lblInventory.TabIndex = 2;
            lblInventory.Text = "Inventory";
            lblInventory.Click += label1_Click;
            // 
            // txtID
            // 
            txtID.Location = new Point(106, 253);
            txtID.Name = "txtID";
            txtID.Size = new Size(483, 39);
            txtID.TabIndex = 3;
            // 
            // txtName
            // 
            txtName.Location = new Point(106, 401);
            txtName.Name = "txtName";
            txtName.Size = new Size(483, 39);
            txtName.TabIndex = 4;
            // 
            // txtQuantity
            // 
            txtQuantity.Location = new Point(106, 549);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(483, 39);
            txtQuantity.TabIndex = 5;
            // 
            // txtCostPrice
            // 
            txtCostPrice.Location = new Point(106, 697);
            txtCostPrice.Name = "txtCostPrice";
            txtCostPrice.Size = new Size(483, 39);
            txtCostPrice.TabIndex = 6;
            // 
            // txtRetailPrice
            // 
            txtRetailPrice.Location = new Point(106, 845);
            txtRetailPrice.Name = "txtRetailPrice";
            txtRetailPrice.Size = new Size(483, 39);
            txtRetailPrice.TabIndex = 7;
            // 
            // lblID
            // 
            lblID.AutoSize = true;
            lblID.Location = new Point(106, 218);
            lblID.Name = "lblID";
            lblID.Size = new Size(37, 32);
            lblID.TabIndex = 8;
            lblID.Text = "ID";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(106, 366);
            lblName.Name = "lblName";
            lblName.Size = new Size(78, 32);
            lblName.TabIndex = 9;
            lblName.Text = "Name";
            // 
            // lblQuantity
            // 
            lblQuantity.AutoSize = true;
            lblQuantity.Location = new Point(106, 514);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(106, 32);
            lblQuantity.TabIndex = 10;
            lblQuantity.Text = "Quantity";
            // 
            // lblCostPrice
            // 
            lblCostPrice.AutoSize = true;
            lblCostPrice.Location = new Point(106, 662);
            lblCostPrice.Name = "lblCostPrice";
            lblCostPrice.Size = new Size(119, 32);
            lblCostPrice.TabIndex = 11;
            lblCostPrice.Text = "Cost Price";
            // 
            // lblRetailPrice
            // 
            lblRetailPrice.AutoSize = true;
            lblRetailPrice.Location = new Point(106, 810);
            lblRetailPrice.Name = "lblRetailPrice";
            lblRetailPrice.Size = new Size(130, 32);
            lblRetailPrice.TabIndex = 12;
            lblRetailPrice.Text = "Retail Price";
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(804, 85);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(85, 32);
            lblSearch.TabIndex = 13;
            lblSearch.Text = "Search";
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(106, 1003);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(408, 120);
            btnAdd.TabIndex = 14;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(585, 1003);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(408, 120);
            btnEdit.TabIndex = 15;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(1056, 1003);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(408, 120);
            btnClear.TabIndex = 16;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(1536, 1003);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(408, 120);
            btnDelete.TabIndex = 17;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // InventoryView
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2081, 1201);
            Controls.Add(btnDelete);
            Controls.Add(btnClear);
            Controls.Add(btnEdit);
            Controls.Add(btnAdd);
            Controls.Add(lblSearch);
            Controls.Add(lblRetailPrice);
            Controls.Add(lblCostPrice);
            Controls.Add(lblQuantity);
            Controls.Add(lblName);
            Controls.Add(lblID);
            Controls.Add(txtRetailPrice);
            Controls.Add(txtCostPrice);
            Controls.Add(txtQuantity);
            Controls.Add(txtName);
            Controls.Add(txtID);
            Controls.Add(lblInventory);
            Controls.Add(txtSearch);
            Controls.Add(dgvInventory);
            Name = "InventoryView";
            Text = "InventoryView";
            ((System.ComponentModel.ISupportInitialize)dgvInventory).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvInventory;
        private TextBox txtSearch;
        private Label lblInventory;
        private TextBox txtID;
        private TextBox txtName;
        private TextBox txtQuantity;
        private TextBox txtCostPrice;
        private TextBox txtRetailPrice;
        private Label lblID;
        private Label lblName;
        private Label lblQuantity;
        private Label lblCostPrice;
        private Label lblRetailPrice;
        private Label lblSearch;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnClear;
        private Button btnDelete;
    }
}