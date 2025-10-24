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
            dgvInventory.Location = new Point(709, 145);
            dgvInventory.Margin = new Padding(4);
            dgvInventory.Name = "dgvInventory";
            dgvInventory.RowHeadersWidth = 82;
            dgvInventory.Size = new Size(1287, 766);
            dgvInventory.TabIndex = 0;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(812, 88);
            txtSearch.Margin = new Padding(4);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(583, 39);
            txtSearch.TabIndex = 1;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // lblInventory
            // 
            lblInventory.AutoSize = true;
            lblInventory.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblInventory.Location = new Point(106, 68);
            lblInventory.Margin = new Padding(4, 0, 4, 0);
            lblInventory.Name = "lblInventory";
            lblInventory.Size = new Size(330, 86);
            lblInventory.TabIndex = 2;
            lblInventory.Text = "Inventory";
            // 
            // txtID
            // 
            txtID.Location = new Point(106, 252);
            txtID.Margin = new Padding(4);
            txtID.Name = "txtID";
            txtID.Size = new Size(483, 39);
            txtID.TabIndex = 3;
            // 
            // txtName
            // 
            txtName.Location = new Point(106, 500);
            txtName.Margin = new Padding(4);
            txtName.Name = "txtName";
            txtName.Size = new Size(483, 39);
            txtName.TabIndex = 4;
            // 
            // txtQuantity
            // 
            txtQuantity.Location = new Point(106, 624);
            txtQuantity.Margin = new Padding(4);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(483, 39);
            txtQuantity.TabIndex = 5;
            // 
            // txtCostPrice
            // 
            txtCostPrice.Location = new Point(106, 748);
            txtCostPrice.Margin = new Padding(4);
            txtCostPrice.Name = "txtCostPrice";
            txtCostPrice.Size = new Size(483, 39);
            txtCostPrice.TabIndex = 6;
            // 
            // txtRetailPrice
            // 
            txtRetailPrice.Location = new Point(106, 872);
            txtRetailPrice.Margin = new Padding(4);
            txtRetailPrice.Name = "txtRetailPrice";
            txtRetailPrice.Size = new Size(483, 39);
            txtRetailPrice.TabIndex = 7;
            // 
            // lblID
            // 
            lblID.AutoSize = true;
            lblID.Location = new Point(106, 216);
            lblID.Margin = new Padding(4, 0, 4, 0);
            lblID.Name = "lblID";
            lblID.Size = new Size(37, 32);
            lblID.TabIndex = 8;
            lblID.Text = "ID";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(106, 464);
            lblName.Margin = new Padding(4, 0, 4, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(78, 32);
            lblName.TabIndex = 9;
            lblName.Text = "Name";
            // 
            // lblQuantity
            // 
            lblQuantity.AutoSize = true;
            lblQuantity.Location = new Point(106, 588);
            lblQuantity.Margin = new Padding(4, 0, 4, 0);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(106, 32);
            lblQuantity.TabIndex = 10;
            lblQuantity.Text = "Quantity";
            // 
            // lblCostPrice
            // 
            lblCostPrice.AutoSize = true;
            lblCostPrice.Location = new Point(106, 712);
            lblCostPrice.Margin = new Padding(4, 0, 4, 0);
            lblCostPrice.Name = "lblCostPrice";
            lblCostPrice.Size = new Size(119, 32);
            lblCostPrice.TabIndex = 11;
            lblCostPrice.Text = "Cost Price";
            // 
            // lblRetailPrice
            // 
            lblRetailPrice.AutoSize = true;
            lblRetailPrice.Location = new Point(106, 836);
            lblRetailPrice.Margin = new Padding(4, 0, 4, 0);
            lblRetailPrice.Name = "lblRetailPrice";
            lblRetailPrice.Size = new Size(130, 32);
            lblRetailPrice.TabIndex = 12;
            lblRetailPrice.Text = "Retail Price";
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(712, 92);
            lblSearch.Margin = new Padding(4, 0, 4, 0);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(85, 32);
            lblSearch.TabIndex = 13;
            lblSearch.Text = "Search";
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(106, 1003);
            btnAdd.Margin = new Padding(4);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(409, 119);
            btnAdd.TabIndex = 14;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(585, 1003);
            btnEdit.Margin = new Padding(4);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(409, 119);
            btnEdit.TabIndex = 15;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(1057, 1003);
            btnClear.Margin = new Padding(4);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(409, 119);
            btnClear.TabIndex = 16;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(1536, 1003);
            btnDelete.Margin = new Padding(4);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(409, 119);
            btnDelete.TabIndex = 17;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // lblSKU
            // 
            lblSKU.AutoSize = true;
            lblSKU.Location = new Point(106, 340);
            lblSKU.Margin = new Padding(4, 0, 4, 0);
            lblSKU.Name = "lblSKU";
            lblSKU.Size = new Size(57, 32);
            lblSKU.TabIndex = 19;
            lblSKU.Text = "SKU";
            // 
            // txtSKU
            // 
            txtSKU.Location = new Point(106, 376);
            txtSKU.Margin = new Padding(4);
            txtSKU.Name = "txtSKU";
            txtSKU.Size = new Size(483, 39);
            txtSKU.TabIndex = 18;
            // 
            // InventoryView
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2151, 1199);
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
            Margin = new Padding(4);
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