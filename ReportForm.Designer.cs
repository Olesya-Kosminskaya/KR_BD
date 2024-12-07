namespace KR_BD
{
    partial class ReportForm
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
            this.checkedListBoxTables = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkedListBoxTables
            // 
            this.checkedListBoxTables.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBoxTables.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkedListBoxTables.ForeColor = System.Drawing.Color.Black;
            this.checkedListBoxTables.FormattingEnabled = true;
            this.checkedListBoxTables.Items.AddRange(new object[] {
            "Пациенты",
            "Диагнозы",
            "Палаты"});
            this.checkedListBoxTables.Location = new System.Drawing.Point(39, 106);
            this.checkedListBoxTables.Name = "checkedListBoxTables";
            this.checkedListBoxTables.Size = new System.Drawing.Size(288, 116);
            this.checkedListBoxTables.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(34, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(544, 29);
            this.label2.TabIndex = 8;
            this.label2.Text = "Выберите таблицы для добавления в отчёт";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(585, 271);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(167, 46);
            this.button1.TabIndex = 9;
            this.button1.Text = "Скачать XLSX-отчёт";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnGenerateXLSX_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(585, 343);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(167, 46);
            this.button2.TabIndex = 10;
            this.button2.Text = "Скачать PDF-отчёт";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnGeneratePDF_Click);
            // 
            // ReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(790, 419);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkedListBoxTables);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ReportForm";
            this.Text = "Формирование отчётов";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBoxTables;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}