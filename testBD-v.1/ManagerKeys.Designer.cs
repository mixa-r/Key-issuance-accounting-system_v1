namespace testBD_v._1
{
    partial class ManagerKeys
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
            this.cbSotr = new System.Windows.Forms.ComboBox();
            this.cbKey = new System.Windows.Forms.ComboBox();
            this.dgData = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Add = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Delete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            this.SuspendLayout();
            // 
            // cbSotr
            // 
            this.cbSotr.DisplayMember = "ФИО";
            this.cbSotr.FormattingEnabled = true;
            this.cbSotr.Location = new System.Drawing.Point(6, 30);
            this.cbSotr.Margin = new System.Windows.Forms.Padding(2);
            this.cbSotr.Name = "cbSotr";
            this.cbSotr.Size = new System.Drawing.Size(164, 21);
            this.cbSotr.TabIndex = 0;
            this.cbSotr.ValueMember = "Код_сотрудника";
            // 
            // cbKey
            // 
            this.cbKey.DisplayMember = "Адрес";
            this.cbKey.FormattingEnabled = true;
            this.cbKey.Location = new System.Drawing.Point(6, 69);
            this.cbKey.Margin = new System.Windows.Forms.Padding(2);
            this.cbKey.Name = "cbKey";
            this.cbKey.Size = new System.Drawing.Size(164, 21);
            this.cbKey.TabIndex = 1;
            this.cbKey.ValueMember = "Код_ключа";
            // 
            // dgData
            // 
            this.dgData.AllowUserToAddRows = false;
            this.dgData.AllowUserToDeleteRows = false;
            this.dgData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column1,
            this.Column4,
            this.Column2});
            this.dgData.Location = new System.Drawing.Point(174, 11);
            this.dgData.Margin = new System.Windows.Forms.Padding(2);
            this.dgData.Name = "dgData";
            this.dgData.ReadOnly = true;
            this.dgData.RowTemplate.Height = 24;
            this.dgData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgData.Size = new System.Drawing.Size(683, 559);
            this.dgData.TabIndex = 2;
            // 
            // Column3
            // 
            this.Column3.FillWeight = 65.28304F;
            this.Column3.HeaderText = "Код ключа";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.FillWeight = 106.599F;
            this.Column1.HeaderText = "Ключ";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Расположение АВ";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.FillWeight = 128.118F;
            this.Column2.HeaderText = "Сотрудник";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Add
            // 
            this.Add.Location = new System.Drawing.Point(6, 94);
            this.Add.Margin = new System.Windows.Forms.Padding(2);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(163, 29);
            this.Add.TabIndex = 3;
            this.Add.Text = "Добавить";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(7, 161);
            this.Save.Margin = new System.Windows.Forms.Padding(2);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(163, 28);
            this.Save.TabIndex = 4;
            this.Save.Text = "Сохранить";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Сотрудник";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 53);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Ключ";
            // 
            // Delete
            // 
            this.Delete.Location = new System.Drawing.Point(7, 128);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(162, 28);
            this.Delete.TabIndex = 7;
            this.Delete.Text = "Удалить";
            this.Delete.UseVisualStyleBackColor = true;
            this.Delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // ManagerKeys
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 581);
            this.Controls.Add(this.Delete);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.Add);
            this.Controls.Add(this.dgData);
            this.Controls.Add(this.cbKey);
            this.Controls.Add(this.cbSotr);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ManagerKeys";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выдача ключей";
            this.Load += new System.EventHandler(this.ManagerKeys_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbSotr;
        private System.Windows.Forms.ComboBox cbKey;
        private System.Windows.Forms.DataGridView dgData;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Delete;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}