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
            lblSKU = new Label();
            txtSKU = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvInventory).BeginInit();
            SuspendLayout();
            // 
            // dgvInventory
            // 
            dgvInventory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvInventory.Location = new Point(382, 68);
            dgvInventory.Margin = new Padding(2, 2, 2, 2);
            dgvInventory.Name = "dgvInventory";
            dgvInventory.RowHeadersWidth = 82;
            dgvInventory.Size = new Size(693, 359);
            dgvInventory.TabIndex = 0;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(437, 41);
            txtSearch.Margin = new Padding(2, 2, 2, 2);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(316, 23);
            txtSearch.TabIndex = 1;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // lblInventory
            // 
            lblInventory.AutoSize = true;
            lblInventory.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblInventory.Location = new Point(57, 32);
            lblInventory.Margin = new Padding(2, 0, 2, 0);
            lblInventory.Name = "lblInventory";
            lblInventory.Size = new Size(165, 45);
            lblInventory.TabIndex = 2;
            lblInventory.Text = "Inventory";
            // 
            // txtID
            // 
            txtID.Location = new Point(57, 118);
            txtID.Margin = new Padding(2, 2, 2, 2);
            txtID.Name = "txtID";
            txtID.Size = new Size(262, 23);
            txtID.TabIndex = 3;
            // 
            // txtName
            // 
            txtName.Location = new Point(57, 234);
            txtName.Margin = new Padding(2, 2, 2, 2);
            txtName.Name = "txtName";
            txtName.Size = new Size(262, 23);
            txtName.TabIndex = 4;
            // 
            // txtQuantity
            // 
            txtQuantity.Location = new Point(57, 292);
            txtQuantity.Margin = new Padding(2, 2, 2, 2);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(262, 23);
            txtQuantity.TabIndex = 5;
            // 
            // txtCostPrice
            // 
            txtCostPrice.Location = new Point(57, 351);
            txtCostPrice.Margin = new Padding(2, 2, 2, 2);
            txtCostPrice.Name = "txtCostPrice";
            txtCostPrice.Size = new Size(262, 23);
            txtCostPrice.TabIndex = 6;
            // 
            // txtRetailPrice
            // 
            txtRetailPrice.Location = new Point(57, 409);
            txtRetailPrice.Margin = new Padding(2, 2, 2, 2);
            txtRetailPrice.Name = "txtRetailPrice";
            txtRetailPrice.Size = new Size(262, 23);
            txtRetailPrice.TabIndex = 7;
            // 
            // lblID
            // 
            lblID.AutoSize = true;
            lblID.Location = new Point(57, 101);
            lblID.Margin = new Padding(2, 0, 2, 0);
            lblID.Name = "lblID";
            lblID.Size = new Size(18, 15);
            lblID.TabIndex = 8;
            lblID.Text = "ID";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(57, 218);
            lblName.Margin = new Padding(2, 0, 2, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(39, 15);
            lblName.TabIndex = 9;
            lblName.Text = "Name";
            // 
            // lblQuantity
            // 
            lblQuantity.AutoSize = true;
            lblQuantity.Location = new Point(57, 276);
            lblQuantity.Margin = new Padding(2, 0, 2, 0);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(53, 15);
            lblQuantity.TabIndex = 10;
            lblQuantity.Text = "Quantity";
            // 
            // lblCostPrice
            // 
            lblCostPrice.AutoSize = true;
            lblCostPrice.Location = new Point(57, 334);
            lblCostPrice.Margin = new Padding(2, 0, 2, 0);
            lblCostPrice.Name = "lblCostPrice";
            lblCostPrice.Size = new Size(60, 15);
            lblCostPrice.TabIndex = 11;
            lblCostPrice.Text = "Cost Price";
            // 
            // lblRetailPrice
            // 
            lblRetailPrice.AutoSize = true;
            lblRetailPrice.Location = new Point(57, 392);
            lblRetailPrice.Margin = new Padding(2, 0, 2, 0);
            lblRetailPrice.Name = "lblRetailPrice";
            lblRetailPrice.Size = new Size(65, 15);
            lblRetailPrice.TabIndex = 12;
            lblRetailPrice.Text = "Retail Price";
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(383, 43);
            lblSearch.Margin = new Padding(2, 0, 2, 0);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(42, 15);
            lblSearch.TabIndex = 13;
            lblSearch.Text = "Search";
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(57, 470);
            btnAdd.Margin = new Padding(2, 2, 2, 2);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(220, 56);
            btnAdd.TabIndex = 14;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(315, 470);
            btnEdit.Margin = new Padding(2, 2, 2, 2);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(220, 56);
            btnEdit.TabIndex = 15;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(569, 470);
            btnClear.Margin = new Padding(2, 2, 2, 2);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(220, 56);
            btnClear.TabIndex = 16;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(827, 470);
            btnDelete.Margin = new Padding(2, 2, 2, 2);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(220, 56);
            btnDelete.TabIndex = 17;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // lblSKU
            // 
            lblSKU.AutoSize = true;
            lblSKU.Location = new Point(57, 159);
            lblSKU.Margin = new Padding(2, 0, 2, 0);
            lblSKU.Name = "lblSKU";
            lblSKU.Size = new Size(28, 15);
            lblSKU.TabIndex = 19;
            lblSKU.Text = "SKU";
            // 
            // txtSKU
            // 
            txtSKU.Location = new Point(57, 176);
            txtSKU.Margin = new Padding(2, 2, 2, 2);
            txtSKU.Name = "txtSKU";
            txtSKU.Size = new Size(262, 23);
            txtSKU.TabIndex = 18;
            // 
            // InventoryView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1127, 562);
            Controls.Add(lblSKU);
            Controls.Add(txtSKU);
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
            Margin = new Padding(2, 2, 2, 2);
            Name = "InventoryView";
            Text = "InventoryView";
            Load += InventoryView_Load;
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
        private Label lblSKU;
        private TextBox txtSKU;
    }
}