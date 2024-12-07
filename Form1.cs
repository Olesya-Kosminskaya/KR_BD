using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Npgsql;
using System.Data.Common;


namespace KR_BD
{
    public partial class Form1 : Form
    {
        public string sqlConnectionString = "Host=127.0.0.1;Port=5432;Database=recoverdb_1;Username=postgres;Password=02012006;"/*ReadConfigFromFile("db_config.txt")*/;
        
        public Form1()
        {
            InitializeComponent();
            LoadWardsWithCount();
            LoadLabels();
           // LoadData();
        }
        private void SqlConnectionReader(string query, DataGridView targetGridView)
        {
            using (NpgsqlConnection npgsqlConnection = new NpgsqlConnection(sqlConnectionString))
            {
                try
                {
                    npgsqlConnection.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand(query, npgsqlConnection))
                    {
                        //command.Connection = npgsqlConnection;
                        command.CommandType = CommandType.Text;

                        using (NpgsqlDataReader dataReader = command.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                DataTable data = new DataTable();
                                data.Load(dataReader);
                                targetGridView.DataSource = data;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
                }
                finally
                {
                    //command.Dispose();
                    npgsqlConnection.Close();
                }
            }
        }
        private void LoadData()
        {
            SqlConnectionReader(ShowTables.ShowPeopleTable, dataGridView1);
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            SqlConnectionReader(ShowTables.ShowDiagnosisTable, dataGridView2);
            dataGridView2.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dataGridView2.Height = dataGridView2.Rows.GetRowsHeight(DataGridViewElementStates.Visible) +
                       dataGridView2.ColumnHeadersHeight;
            SqlConnectionReader(ShowTables.ShowWardsTable, dataGridView3);
            dataGridView3.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dataGridView3.Height = dataGridView3.Rows.GetRowsHeight(DataGridViewElementStates.Visible) +
                       dataGridView3.ColumnHeadersHeight;
        }

        void AdjustDataGridViewSize(DataGridView grid)
        {
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            int totalWidth = grid.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
            int totalHeight = grid.Rows.GetRowsHeight(DataGridViewElementStates.Visible);

            // Устанавливаем новый размер
            grid.ClientSize = new Size(totalWidth, totalHeight);
        }


        //private void sqlConnectionReader()
        //{
        //    NpgsqlConnection npgsqlConnection = new NpgsqlConnection(sqlConnectionString);
        //    npgsqlConnection.Open();
        //    NpgsqlCommand command = new NpgsqlCommand();
        //    command.Connection = npgsqlConnection;
        //    command.CommandType = CommandType.Text;
        //    command.CommandText = ShowTables.ShowPeopleTable;
        //    try
        //    {
        //        NpgsqlDataReader dataReader = command.ExecuteReader();
        //        if (dataReader.HasRows)
        //        {
        //            DataTable data = new DataTable();
        //            data.Load(dataReader);
        //            dataGridView1.DataSource = data;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
        //    }
        //    finally
        //    {
        //        command.Dispose();
        //        npgsqlConnection.Close();
        //    }
        //}
        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show(
            "Вы действительно хотите закрыть программу?",
            "Подтверждение закрытия",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
               //Application.Exit();
                Environment.Exit(1);
            }
            else if(result == DialogResult.No)
            {
                e.Cancel = true;
            }
             
            //if (MessageBox.Show("Вы действительно хотите закрыть программу?",
            // "Подтверждение закрытия", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    Application.Exit();
            //    //Environment.Exit(1);
            //    //Process.GetCurrentProcess().Kill();
            //}
            //else
            //{
            //    e.Cancel = true;
            //}
        }

        static string ReadConfigFromFile(string configFile)
        {
            string[] lines = File.ReadAllLines(configFile);
            var parameters = new Dictionary<string, string>();

            foreach (var line in lines)
            {
                string[] keyValue = line.Split('=', (char)StringSplitOptions.RemoveEmptyEntries);
                if (keyValue.Length == 2)
                {
                    parameters[keyValue[0].Trim()] = keyValue[1].Trim();
                }
            }

            return $"Host={parameters["Host"]};Port={parameters["Port"]};Database={parameters["Database"]};Username={parameters["Username"]};Password={parameters["Password"]}";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet1.people". При необходимости она может быть перемещена или удалена.
            this.peopleTableAdapter.Fill(this.dataSet1.people);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet1.wards". При необходимости она может быть перемещена или удалена.
            this.wardsTableAdapter.Fill(this.dataSet1.wards);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet1.diagnosis". При необходимости она может быть перемещена или удалена.
            this.diagnosisTableAdapter.Fill(this.dataSet1.diagnosis);

        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.peopleTableAdapter.FillBy(this.dataSet1.people);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void fillBy1ToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.peopleTableAdapter.FillBy1(this.dataSet1.people);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void LoadWardsWithCount()
        {
            string query = @"
            SELECT wards.id AS Номер, 
            COUNT(people.last_name) AS Количество,
            wards.max_count AS Вместимость
            FROM wards 
            LEFT JOIN people ON people.ward_id = wards.id
            GROUP BY wards.id, wards.name, wards.max_count, wards.diagnosis_id
            ORDER BY wards.id ASC; ";
            try
            {
                using (var connection = new NpgsqlConnection(this.sqlConnectionString))
                {
                    DataTable dtData = new DataTable();
                    connection.Open();


                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
                    {
                        dataGridView4.DataSource = dtData;
                        dtData.Load(cmd.ExecuteReader());
                        dataGridView4.Columns[0].Width = 65;
                        dataGridView4.Columns[1].Width = 75;
                        dataGridView4.Columns[2].Width = 80;
                        dataGridView5.DataSource = dtData;
                        dataGridView5.Columns[0].Width = 65;
                        dataGridView5.Columns[1].Width = 75;
                        dataGridView5.Columns[2].Width = 80;
                        //connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка выполнения запроса: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        private ToolTip toolTip = new ToolTip();
        private void LoadLabels()
        {
            // Настроим подсказку для Label
            toolTip.SetToolTip(label5, "Нажмите, чтобы увидеть диаграмму");
            toolTip.SetToolTip(label6, "Нажмите, чтобы увидеть диаграмму");

            // Дополнительно можно настроить параметры ToolTip (по желанию)
            toolTip.ToolTipTitle = "Подсказка";  // Заголовок подсказки
            toolTip.IsBalloon = true;  // Сделать подсказку в виде "шарика"
            toolTip.AutoPopDelay = 5000;  // Время отображения подсказки (в миллисекундах)
            toolTip.InitialDelay = 500;  // Задержка перед появлением подсказки (в миллисекундах)
            toolTip.ReshowDelay = 100;  // Задержка между повторным отображением подсказки (в миллисекундах)
        }

        private void fillByToolStripButton_Click_1(object sender, EventArgs e)
        {
            try
            {
                this.wardsTableAdapter.FillBy(this.dataSet1.wards);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            DataTable[] dataTableArray = { this.dataSet1.people, this.dataSet1.diagnosis, this.dataSet1.wards };
            ReportForm fm2 = new ReportForm(dataTableArray);
            fm2.ShowDialog();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            ChartForm chartForm = new ChartForm();
            chartForm.ShowDialog();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            ChartForm chartForm = new ChartForm();
            chartForm.ShowDialog();
        }
    }

    public static class ShowTables
    {
        public static string ShowPeopleTable => "SELECT * FROM show_people_table();";
        public static string ShowDiagnosisTable => "SELECT * FROM show_diagnosis_table();";
        public static string ShowWardsTable => "SELECT * FROM show_wards_table();";
    }
}
