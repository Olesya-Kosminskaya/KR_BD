namespace KR_BD
{
    partial class Form2
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
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.firstnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fathernameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diagnosisidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wardidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.peopleBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet1 = new KR_BD.DataSet1();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button10 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button11 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button8 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diagnosisBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.button12 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView5 = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.button9 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maxcountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diagnosisidDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wardsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.peopleTableAdapter = new KR_BD.DataSet1TableAdapters.peopleTableAdapter();
            this.diagnosisTableAdapter = new KR_BD.DataSet1TableAdapters.diagnosisTableAdapter();
            this.wardsTableAdapter = new KR_BD.DataSet1TableAdapters.wardsTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peopleBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.diagnosisBindingSource)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wardsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1BindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.firstnameDataGridViewTextBoxColumn,
            this.lastnameDataGridViewTextBoxColumn,
            this.fathernameDataGridViewTextBoxColumn,
            this.diagnosisidDataGridViewTextBoxColumn,
            this.wardidDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.peopleBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(43, 56);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(817, 467);
            this.dataGridView1.TabIndex = 0;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn.HeaderText = "Код пациента";
            this.idDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.Width = 130;
            // 
            // firstnameDataGridViewTextBoxColumn
            // 
            this.firstnameDataGridViewTextBoxColumn.DataPropertyName = "first_name";
            this.firstnameDataGridViewTextBoxColumn.HeaderText = "        Фамилия";
            this.firstnameDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.firstnameDataGridViewTextBoxColumn.Name = "firstnameDataGridViewTextBoxColumn";
            this.firstnameDataGridViewTextBoxColumn.Width = 125;
            // 
            // lastnameDataGridViewTextBoxColumn
            // 
            this.lastnameDataGridViewTextBoxColumn.DataPropertyName = "last_name";
            this.lastnameDataGridViewTextBoxColumn.HeaderText = "             Имя";
            this.lastnameDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.lastnameDataGridViewTextBoxColumn.Name = "lastnameDataGridViewTextBoxColumn";
            this.lastnameDataGridViewTextBoxColumn.Width = 125;
            // 
            // fathernameDataGridViewTextBoxColumn
            // 
            this.fathernameDataGridViewTextBoxColumn.DataPropertyName = "father_name";
            this.fathernameDataGridViewTextBoxColumn.HeaderText = "      Отчество";
            this.fathernameDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.fathernameDataGridViewTextBoxColumn.Name = "fathernameDataGridViewTextBoxColumn";
            this.fathernameDataGridViewTextBoxColumn.Width = 125;
            // 
            // diagnosisidDataGridViewTextBoxColumn
            // 
            this.diagnosisidDataGridViewTextBoxColumn.DataPropertyName = "diagnosis_id";
            this.diagnosisidDataGridViewTextBoxColumn.HeaderText = "  Код диагноза";
            this.diagnosisidDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.diagnosisidDataGridViewTextBoxColumn.Name = "diagnosisidDataGridViewTextBoxColumn";
            this.diagnosisidDataGridViewTextBoxColumn.Width = 130;
            // 
            // wardidDataGridViewTextBoxColumn
            // 
            this.wardidDataGridViewTextBoxColumn.DataPropertyName = "ward_id";
            this.wardidDataGridViewTextBoxColumn.HeaderText = "Номер палаты";
            this.wardidDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.wardidDataGridViewTextBoxColumn.Name = "wardidDataGridViewTextBoxColumn";
            this.wardidDataGridViewTextBoxColumn.Width = 130;
            // 
            // peopleBindingSource
            // 
            this.peopleBindingSource.DataMember = "people";
            this.peopleBindingSource.DataSource = this.dataSet1;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(-6, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1540, 668);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button10);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.dataGridView4);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1532, 639);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Пациенты";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(1201, 480);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(154, 43);
            this.button10.TabIndex = 14;
            this.button10.Text = "Создать отчёт";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.btnReportForm_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.ForeColor = System.Drawing.Color.Navy;
            this.label6.Location = new System.Drawing.Point(1103, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(351, 29);
            this.label6.TabIndex = 13;
            this.label6.Text = "Распределение по палатам";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // dataGridView4
            // 
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Location = new System.Drawing.Point(1108, 130);
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.RowHeadersWidth = 51;
            this.dataGridView4.RowTemplate.Height = 24;
            this.dataGridView4.Size = new System.Drawing.Size(346, 324);
            this.dataGridView4.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(38, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 29);
            this.label2.TabIndex = 6;
            this.label2.Text = "Пациенты";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(905, 395);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(154, 43);
            this.button3.TabIndex = 3;
            this.button3.Text = "Удалить";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(905, 271);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(154, 43);
            this.button2.TabIndex = 2;
            this.button2.Text = "Изменить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(905, 151);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(154, 43);
            this.button1.TabIndex = 1;
            this.button1.Text = "Добавить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button11);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.button8);
            this.tabPage2.Controls.Add(this.button6);
            this.tabPage2.Controls.Add(this.button4);
            this.tabPage2.Controls.Add(this.dataGridView2);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1524, 606);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Диагнозы";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(793, 448);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(154, 43);
            this.button11.TabIndex = 14;
            this.button11.Text = "Создать отчёт";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.btnReportForm_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(52, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 29);
            this.label1.TabIndex = 5;
            this.label1.Text = "Диагнозы";
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(793, 293);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(154, 43);
            this.button8.TabIndex = 4;
            this.button8.Text = "Удалить";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(793, 194);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(154, 43);
            this.button6.TabIndex = 3;
            this.button6.Text = "Изменить";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(793, 99);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(154, 43);
            this.button4.TabIndex = 2;
            this.button4.Text = "Добавить";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn1,
            this.nameDataGridViewTextBoxColumn});
            this.dataGridView2.DataSource = this.diagnosisBindingSource;
            this.dataGridView2.Location = new System.Drawing.Point(57, 83);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(650, 408);
            this.dataGridView2.TabIndex = 0;
            // 
            // idDataGridViewTextBoxColumn1
            // 
            this.idDataGridViewTextBoxColumn1.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn1.HeaderText = "        Код диагноза";
            this.idDataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.idDataGridViewTextBoxColumn1.Name = "idDataGridViewTextBoxColumn1";
            this.idDataGridViewTextBoxColumn1.Width = 265;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "       Название";
            this.nameDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.Width = 315;
            // 
            // diagnosisBindingSource
            // 
            this.diagnosisBindingSource.DataMember = "diagnosis";
            this.diagnosisBindingSource.DataSource = this.dataSet1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button12);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.dataGridView5);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.button9);
            this.tabPage3.Controls.Add(this.button7);
            this.tabPage3.Controls.Add(this.button5);
            this.tabPage3.Controls.Add(this.dataGridView3);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1524, 606);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Палаты";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(1170, 467);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(154, 43);
            this.button12.TabIndex = 16;
            this.button12.Text = "Создать отчёт";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.btnReportForm_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(1063, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(351, 29);
            this.label4.TabIndex = 15;
            this.label4.Text = "Распределение по палатам";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // dataGridView5
            // 
            this.dataGridView5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView5.Location = new System.Drawing.Point(1068, 100);
            this.dataGridView5.Name = "dataGridView5";
            this.dataGridView5.RowHeadersWidth = 51;
            this.dataGridView5.RowTemplate.Height = 24;
            this.dataGridView5.Size = new System.Drawing.Size(346, 324);
            this.dataGridView5.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(68, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 29);
            this.label3.TabIndex = 6;
            this.label3.Text = "Палаты";
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(823, 340);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(154, 43);
            this.button9.TabIndex = 4;
            this.button9.Text = "Удалить";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(823, 237);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(154, 43);
            this.button7.TabIndex = 3;
            this.button7.Text = "Изменить";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(823, 132);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(154, 43);
            this.button5.TabIndex = 2;
            this.button5.Text = "Добавить";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // dataGridView3
            // 
            this.dataGridView3.AutoGenerateColumns = false;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn2,
            this.nameDataGridViewTextBoxColumn1,
            this.maxcountDataGridViewTextBoxColumn,
            this.diagnosisidDataGridViewTextBoxColumn1});
            this.dataGridView3.DataSource = this.wardsBindingSource;
            this.dataGridView3.Location = new System.Drawing.Point(73, 100);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowHeadersWidth = 51;
            this.dataGridView3.RowTemplate.Height = 24;
            this.dataGridView3.Size = new System.Drawing.Size(658, 327);
            this.dataGridView3.TabIndex = 0;
            // 
            // idDataGridViewTextBoxColumn2
            // 
            this.idDataGridViewTextBoxColumn2.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn2.HeaderText = "     Номер палаты";
            this.idDataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.idDataGridViewTextBoxColumn2.Name = "idDataGridViewTextBoxColumn2";
            this.idDataGridViewTextBoxColumn2.Width = 151;
            // 
            // nameDataGridViewTextBoxColumn1
            // 
            this.nameDataGridViewTextBoxColumn1.DataPropertyName = "name";
            this.nameDataGridViewTextBoxColumn1.HeaderText = "           Название";
            this.nameDataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.nameDataGridViewTextBoxColumn1.Name = "nameDataGridViewTextBoxColumn1";
            this.nameDataGridViewTextBoxColumn1.Width = 152;
            // 
            // maxcountDataGridViewTextBoxColumn
            // 
            this.maxcountDataGridViewTextBoxColumn.DataPropertyName = "max_count";
            this.maxcountDataGridViewTextBoxColumn.HeaderText = "        Вместимость";
            this.maxcountDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.maxcountDataGridViewTextBoxColumn.Name = "maxcountDataGridViewTextBoxColumn";
            this.maxcountDataGridViewTextBoxColumn.Width = 151;
            // 
            // diagnosisidDataGridViewTextBoxColumn1
            // 
            this.diagnosisidDataGridViewTextBoxColumn1.DataPropertyName = "diagnosis_id";
            this.diagnosisidDataGridViewTextBoxColumn1.HeaderText = "        Код диагноза";
            this.diagnosisidDataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.diagnosisidDataGridViewTextBoxColumn1.Name = "diagnosisidDataGridViewTextBoxColumn1";
            this.diagnosisidDataGridViewTextBoxColumn1.Width = 151;
            // 
            // wardsBindingSource
            // 
            this.wardsBindingSource.DataMember = "wards";
            this.wardsBindingSource.DataSource = this.dataSet1;
            // 
            // dataSet1BindingSource
            // 
            this.dataSet1BindingSource.DataSource = this.dataSet1;
            this.dataSet1BindingSource.Position = 0;
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
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1529, 659);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Form2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormLogin_FormClosing);
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peopleBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.diagnosisBindingSource)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wardsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1BindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource dataSet1BindingSource;
        private DataSet1 dataSet1;
        private System.Windows.Forms.BindingSource peopleBindingSource;
        private DataSet1TableAdapters.peopleTableAdapter peopleTableAdapter;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn firstnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fathernameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn diagnosisidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn wardidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.BindingSource diagnosisBindingSource;
        private DataSet1TableAdapters.diagnosisTableAdapter diagnosisTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.BindingSource wardsBindingSource;
        private DataSet1TableAdapters.wardsTableAdapter wardsTableAdapter;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView5;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn maxcountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn diagnosisidDataGridViewTextBoxColumn1;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
    }
}