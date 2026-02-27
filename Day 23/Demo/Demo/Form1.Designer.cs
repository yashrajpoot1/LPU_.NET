namespace Demo
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
            this.prodId = new System.Windows.Forms.Label();
            this.TxtProdId = new System.Windows.Forms.TextBox();
            this.TxtProdPrice = new System.Windows.Forms.TextBox();
            this.prodName = new System.Windows.Forms.Label();
            this.TxtProdName = new System.Windows.Forms.TextBox();
            this.prodPrice = new System.Windows.Forms.Label();
            this.TxtProdDesc = new System.Windows.Forms.TextBox();
            this.prodDesc = new System.Windows.Forms.Label();
            this.BtnAddProd = new System.Windows.Forms.Button();
            this.BtnUpdateProd = new System.Windows.Forms.Button();
            this.BtnDeleteProd = new System.Windows.Forms.Button();
            this.BtnShowAllProds = new System.Windows.Forms.Button();
            this.BtnSearchProdById = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // prodId
            // 
            this.prodId.AutoSize = true;
            this.prodId.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prodId.Location = new System.Drawing.Point(92, 66);
            this.prodId.Name = "prodId";
            this.prodId.Size = new System.Drawing.Size(124, 29);
            this.prodId.TabIndex = 0;
            this.prodId.Text = "Product-Id";
            // 
            // TxtProdId
            // 
            this.TxtProdId.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtProdId.Location = new System.Drawing.Point(240, 61);
            this.TxtProdId.Name = "TxtProdId";
            this.TxtProdId.Size = new System.Drawing.Size(157, 34);
            this.TxtProdId.TabIndex = 1;
            // 
            // TxtProdPrice
            // 
            this.TxtProdPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtProdPrice.Location = new System.Drawing.Point(240, 151);
            this.TxtProdPrice.Name = "TxtProdPrice";
            this.TxtProdPrice.Size = new System.Drawing.Size(157, 34);
            this.TxtProdPrice.TabIndex = 3;
            // 
            // prodName
            // 
            this.prodName.AutoSize = true;
            this.prodName.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prodName.Location = new System.Drawing.Point(47, 106);
            this.prodName.Name = "prodName";
            this.prodName.Size = new System.Drawing.Size(169, 29);
            this.prodName.TabIndex = 2;
            this.prodName.Text = "Product-Name";
            // 
            // TxtProdName
            // 
            this.TxtProdName.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtProdName.Location = new System.Drawing.Point(240, 103);
            this.TxtProdName.Name = "TxtProdName";
            this.TxtProdName.Size = new System.Drawing.Size(157, 34);
            this.TxtProdName.TabIndex = 5;
            // 
            // prodPrice
            // 
            this.prodPrice.AutoSize = true;
            this.prodPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prodPrice.Location = new System.Drawing.Point(147, 151);
            this.prodPrice.Name = "prodPrice";
            this.prodPrice.Size = new System.Drawing.Size(69, 29);
            this.prodPrice.TabIndex = 4;
            this.prodPrice.Text = "Price";
            // 
            // TxtProdDesc
            // 
            this.TxtProdDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtProdDesc.Location = new System.Drawing.Point(240, 196);
            this.TxtProdDesc.Multiline = true;
            this.TxtProdDesc.Name = "TxtProdDesc";
            this.TxtProdDesc.Size = new System.Drawing.Size(157, 85);
            this.TxtProdDesc.TabIndex = 7;
            // 
            // prodDesc
            // 
            this.prodDesc.AutoSize = true;
            this.prodDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prodDesc.Location = new System.Drawing.Point(81, 196);
            this.prodDesc.Name = "prodDesc";
            this.prodDesc.Size = new System.Drawing.Size(135, 29);
            this.prodDesc.TabIndex = 6;
            this.prodDesc.Text = "Description";
            // 
            // BtnAddProd
            // 
            this.BtnAddProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAddProd.Location = new System.Drawing.Point(53, 325);
            this.BtnAddProd.Name = "BtnAddProd";
            this.BtnAddProd.Size = new System.Drawing.Size(92, 34);
            this.BtnAddProd.TabIndex = 8;
            this.BtnAddProd.Text = "Add";
            this.BtnAddProd.UseVisualStyleBackColor = true;
            // 
            // BtnUpdateProd
            // 
            this.BtnUpdateProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnUpdateProd.Location = new System.Drawing.Point(179, 325);
            this.BtnUpdateProd.Name = "BtnUpdateProd";
            this.BtnUpdateProd.Size = new System.Drawing.Size(99, 34);
            this.BtnUpdateProd.TabIndex = 9;
            this.BtnUpdateProd.Text = "Update";
            this.BtnUpdateProd.UseVisualStyleBackColor = true;
            // 
            // BtnDeleteProd
            // 
            this.BtnDeleteProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDeleteProd.Location = new System.Drawing.Point(306, 325);
            this.BtnDeleteProd.Name = "BtnDeleteProd";
            this.BtnDeleteProd.Size = new System.Drawing.Size(91, 34);
            this.BtnDeleteProd.TabIndex = 10;
            this.BtnDeleteProd.Text = "Delete";
            this.BtnDeleteProd.UseVisualStyleBackColor = true;
            // 
            // BtnShowAllProds
            // 
            this.BtnShowAllProds.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnShowAllProds.Location = new System.Drawing.Point(53, 386);
            this.BtnShowAllProds.Name = "BtnShowAllProds";
            this.BtnShowAllProds.Size = new System.Drawing.Size(92, 31);
            this.BtnShowAllProds.TabIndex = 11;
            this.BtnShowAllProds.Text = "Show";
            this.BtnShowAllProds.UseVisualStyleBackColor = true;
            this.BtnShowAllProds.Click += new System.EventHandler(this.BtnShowAllProds_Click);
            // 
            // BtnSearchProdById
            // 
            this.BtnSearchProdById.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSearchProdById.Location = new System.Drawing.Point(179, 386);
            this.BtnSearchProdById.Name = "BtnSearchProdById";
            this.BtnSearchProdById.Size = new System.Drawing.Size(99, 31);
            this.BtnSearchProdById.TabIndex = 12;
            this.BtnSearchProdById.Text = "Search";
            this.BtnSearchProdById.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(530, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(436, 309);
            this.dataGridView1.TabIndex = 13;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 633);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.BtnSearchProdById);
            this.Controls.Add(this.BtnShowAllProds);
            this.Controls.Add(this.BtnDeleteProd);
            this.Controls.Add(this.BtnUpdateProd);
            this.Controls.Add(this.BtnAddProd);
            this.Controls.Add(this.TxtProdDesc);
            this.Controls.Add(this.prodDesc);
            this.Controls.Add(this.TxtProdName);
            this.Controls.Add(this.prodPrice);
            this.Controls.Add(this.TxtProdPrice);
            this.Controls.Add(this.prodName);
            this.Controls.Add(this.TxtProdId);
            this.Controls.Add(this.prodId);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label prodId;
        private System.Windows.Forms.TextBox TxtProdId;
        private System.Windows.Forms.TextBox TxtProdPrice;
        private System.Windows.Forms.Label prodName;
        private System.Windows.Forms.TextBox TxtProdName;
        private System.Windows.Forms.Label prodPrice;
        private System.Windows.Forms.TextBox TxtProdDesc;
        private System.Windows.Forms.Label prodDesc;
        private System.Windows.Forms.Button BtnAddProd;
        private System.Windows.Forms.Button BtnUpdateProd;
        private System.Windows.Forms.Button BtnDeleteProd;
        private System.Windows.Forms.Button BtnShowAllProds;
        private System.Windows.Forms.Button BtnSearchProdById;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}

