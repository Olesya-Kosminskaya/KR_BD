namespace KR_BD
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView5 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.firstnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fathernameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diagnosisidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wardidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.peopleBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet1 = new KR_BD.DataSet1();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diagnosisBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maxcountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diagnosisidDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wardsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.peopleBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.peopleTableAdapter = new KR_BD.DataSet1TableAdapters.peopleTableAdapter();
            this.diagnosisTableAdapter = new KR_BD.DataSet1TableAdapters.diagnosisTableAdapter();
            this.wardsTableAdapter = new KR_BD.DataSet1TableAdapters.wardsTableAdapter();
            this.wardsBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peopleBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.diagnosisBindingSource)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wardsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peopleBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wardsBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(-1, -1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1535, 667);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.dataGridView5);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1527, 638);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Пациенты";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1211, 537);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(154, 43);
            this.button1.TabIndex = 13;
            this.button1.Text = "Создать отчёт";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.ForeColor = System.Drawing.Color.Navy;
            this.label5.Location = new System.Drawing.Point(1106, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(351, 29);
            this.label5.TabIndex = 10;
            this.label5.Text = "Распределение по палатам";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // dataGridView5
            // 
            this.dataGridView5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView5.Location = new System.Drawing.Point(1111, 152);
            this.dataGridView5.Name = "dataGridView5";
            this.dataGridView5.RowHeadersWidth = 51;
            this.dataGridView5.RowTemplate.Height = 24;
            this.dataGridView5.Size = new System.Drawing.Size(363, 327);
            this.dataGridView5.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(44, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 29);
            this.label2.TabIndex = 7;
            this.label2.Text = "Пациенты";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.firstnameDataGridViewTextBoxColumn,
            this.lastnameDataGridViewTextBoxColumn,
            this.fathernameDataGridViewTextBoxColumn,
            this.diagnosisidDataGridViewTextBoxColumn,
            this.wardidDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.peopleBindingSource1;
            this.dataGridView1.Location = new System.Drawing.Point(49, 61);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1036, 519);
            this.dataGridView1.TabIndex = 0;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn.HeaderText = "    Код пациента";
            this.idDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            this.idDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.idDataGridViewTextBoxColumn.Width = 140;
            // 
            // firstnameDataGridViewTextBoxColumn
            // 
            this.firstnameDataGridViewTextBoxColumn.DataPropertyName = "first_name";
            this.firstnameDataGridViewTextBoxColumn.HeaderText = "         Фамилия";
            this.firstnameDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.firstnameDataGridViewTextBoxColumn.Name = "firstnameDataGridViewTextBoxColumn";
            this.firstnameDataGridViewTextBoxColumn.ReadOnly = true;
            this.firstnameDataGridViewTextBoxColumn.Width = 125;
            // 
            // lastnameDataGridViewTextBoxColumn
            // 
            this.lastnameDataGridViewTextBoxColumn.DataPropertyName = "last_name";
            this.lastnameDataGridViewTextBoxColumn.HeaderText = "             Имя";
            this.lastnameDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.lastnameDataGridViewTextBoxColumn.Name = "lastnameDataGridViewTextBoxColumn";
            this.lastnameDataGridViewTextBoxColumn.ReadOnly = true;
            this.lastnameDataGridViewTextBoxColumn.Width = 125;
            // 
            // fathernameDataGridViewTextBoxColumn
            // 
            this.fathernameDataGridViewTextBoxColumn.DataPropertyName = "father_name";
            this.fathernameDataGridViewTextBoxColumn.HeaderText = "         Отчество";
            this.fathernameDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.fathernameDataGridViewTextBoxColumn.Name = "fathernameDataGridViewTextBoxColumn";
            this.fathernameDataGridViewTextBoxColumn.ReadOnly = true;
            this.fathernameDataGridViewTextBoxColumn.Width = 125;
            // 
            // diagnosisidDataGridViewTextBoxColumn
            // 
            this.diagnosisidDataGridViewTextBoxColumn.DataPropertyName = "diagnosis_id";
            this.diagnosisidDataGridViewTextBoxColumn.HeaderText = "Код диагноза";
            this.diagnosisidDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.diagnosisidDataGridViewTextBoxColumn.Name = "diagnosisidDataGridViewTextBoxColumn";
            this.diagnosisidDataGridViewTextBoxColumn.ReadOnly = true;
            this.diagnosisidDataGridViewTextBoxColumn.Width = 125;
            // 
            // wardidDataGridViewTextBoxColumn
            // 
            this.wardidDataGridViewTextBoxColumn.DataPropertyName = "ward_id";
            this.wardidDataGridViewTextBoxColumn.HeaderText = "Номер палаты";
            this.wardidDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.wardidDataGridViewTextBoxColumn.Name = "wardidDataGridViewTextBoxColumn";
            this.wardidDataGridViewTextBoxColumn.ReadOnly = true;
            this.wardidDataGridViewTextBoxColumn.Width = 130;
            // 
            // peopleBindingSource1
            // 
            this.peopleBindingSource1.DataMember = "people";
            this.peopleBindingSource1.DataSource = this.dataSet1;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.dataGridView2);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1526, 634);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Диагнозы";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1211, 537);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(154, 43);
            this.button2.TabIndex = 13;
            this.button2.Text = "Создать отчёт";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(237, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 29);
            this.label3.TabIndex = 6;
            this.label3.Text = "Диагнозы";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn1,
            this.nameDataGridViewTextBoxColumn});
            this.dataGridView2.DataSource = this.diagnosisBindingSource;
            this.dataGridView2.Location = new System.Drawing.Point(242, 81);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(650, 408);
            this.dataGridView2.TabIndex = 0;
            // 
            // idDataGridViewTextBoxColumn1
            // 
            this.idDataGridViewTextBoxColumn1.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn1.FillWeight = 106.9519F;
            this.idDataGridViewTextBoxColumn1.HeaderText = "                    Код диагноза";
            this.idDataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.idDataGridViewTextBoxColumn1.Name = "idDataGridViewTextBoxColumn1";
            this.idDataGridViewTextBoxColumn1.ReadOnly = true;
            this.idDataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.idDataGridViewTextBoxColumn1.Width = 265;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "name";
            this.nameDataGridViewTextBoxColumn.FillWeight = 93.04813F;
            this.nameDataGridViewTextBoxColumn.HeaderText = "                       Название";
            this.nameDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            this.nameDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.nameDataGridViewTextBoxColumn.Width = 315;
            // 
            // diagnosisBindingSource
            // 
            this.diagnosisBindingSource.DataMember = "diagnosis";
            this.diagnosisBindingSource.DataSource = this.dataSet1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button3);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.dataGridView4);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.dataGridView3);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1526, 634);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Палаты";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1150, 461);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(154, 43);
            this.button3.TabIndex = 14;
            this.button3.Text = "Создать отчёт";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.ForeColor = System.Drawing.Color.Navy;
            this.label6.Location = new System.Drawing.Point(1042, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(351, 29);
            this.label6.TabIndex = 11;
            this.label6.Text = "Распределение по палатам";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // dataGridView4
            // 
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Location = new System.Drawing.Point(1047, 96);
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.RowHeadersWidth = 51;
            this.dataGridView4.RowTemplate.Height = 24;
            this.dataGridView4.Size = new System.Drawing.Size(363, 327);
            this.dataGridView4.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(95, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 29);
            this.label4.TabIndex = 7;
            this.label4.Text = "Палаты";
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.AutoGenerateColumns = false;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn2,
            this.nameDataGridViewTextBoxColumn1,
            this.maxcountDataGridViewTextBoxColumn,
            this.diagnosisidDataGridViewTextBoxColumn1});
            this.dataGridView3.DataSource = this.wardsBindingSource;
            this.dataGridView3.Location = new System.Drawing.Point(100, 96);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            this.dataGridView3.RowHeadersWidth = 51;
            this.dataGridView3.RowTemplate.Height = 24;
            this.dataGridView3.Size = new System.Drawing.Size(827, 327);
            this.dataGridView3.TabIndex = 0;
            // 
            // idDataGridViewTextBoxColumn2
            // 
            this.idDataGridViewTextBoxColumn2.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn2.HeaderText = "Номер палаты";
            this.idDataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.idDataGridViewTextBoxColumn2.Name = "idDataGridViewTextBoxColumn2";
            this.idDataGridViewTextBoxColumn2.ReadOnly = true;
            this.idDataGridViewTextBoxColumn2.Width = 130;
            // 
            // nameDataGridViewTextBoxColumn1
            // 
            this.nameDataGridViewTextBoxColumn1.DataPropertyName = "name";
            this.nameDataGridViewTextBoxColumn1.HeaderText = "             Название";
            this.nameDataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.nameDataGridViewTextBoxColumn1.Name = "nameDataGridViewTextBoxColumn1";
            this.nameDataGridViewTextBoxColumn1.ReadOnly = true;
            this.nameDataGridViewTextBoxColumn1.Width = 160;
            // 
            // maxcountDataGridViewTextBoxColumn
            // 
            this.maxcountDataGridViewTextBoxColumn.DataPropertyName = "max_count";
            this.maxcountDataGridViewTextBoxColumn.HeaderText = "    Вместимость";
            this.maxcountDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.maxcountDataGridViewTextBoxColumn.Name = "maxcountDataGridViewTextBoxColumn";
            this.maxcountDataGridViewTextBoxColumn.ReadOnly = true;
            this.maxcountDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.maxcountDataGridViewTextBoxColumn.Width = 140;
            // 
            // diagnosisidDataGridViewTextBoxColumn1
            // 
            this.diagnosisidDataGridViewTextBoxColumn1.DataPropertyName = "diagnosis_id";
            this.diagnosisidDataGridViewTextBoxColumn1.HeaderText = "    Код диагноза";
            this.diagnosisidDataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.diagnosisidDataGridViewTextBoxColumn1.Name = "diagnosisidDataGridViewTextBoxColumn1";
            this.diagnosisidDataGridViewTextBoxColumn1.ReadOnly = true;
            this.diagnosisidDataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.diagnosisidDataGridViewTextBoxColumn1.Width = 140;
            // 
            // wardsBindingSource
            // 
            this.wardsBindingSource.DataMember = "wards";
            this.wardsBindingSource.DataSource = this.dataSet1;
            // 
            // peopleBindingSource
            // 
            this.peopleBindingSource.DataMember = "people";
            this.peopleBindingSource.DataSource = this.dataSet1;
            // 
            // peopleTableAdapter
            // 
            this.peopleTableAdapter.ClearBeforeFill = true;
            // 
            // diagnosisTableAdapter
            // 
            this.diagnosisTableAdapter.ClearBeforeFill = true;
            // 
            // wardsTableAdapter
            // 
            this.wardsTableAdapter.ClearBeforeFill = true;
            // 
            // wardsBindingSource1
            // 
            this.wardsBindingSource1.DataMember = "wards";
            this.wardsBindingSource1.DataSource = this.dataSet1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1529, 659);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormLogin_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peopleBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.diagnosisBindingSource)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wardsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peopleBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wardsBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView3;
        private DataSet1 dataSet1;
        private System.Windows.Forms.BindingSource peopleBindingSource;
        private DataSet1TableAdapters.peopleTableAdapter peopleTableAdapter;
        private System.Windows.Forms.BindingSource diagnosisBindingSource;
        private DataSet1TableAdapters.diagnosisTableAdapter diagnosisTableAdapter;
        private System.Windows.Forms.BindingSource wardsBindingSource;
        private DataSet1TableAdapters.wardsTableAdapter wardsTableAdapter;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource peopleBindingSource1;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn firstnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fathernameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn diagnosisidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn wardidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.BindingSource wardsBindingSource1;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn maxcountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn diagnosisidDataGridViewTextBoxColumn1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}

