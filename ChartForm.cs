using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace KR_BD
{
    public partial class ChartForm : Form
    {
        public ChartForm()
        {
            InitializeComponent();
            LoadWardsWithCount();
        }
        public string sqlConnectionString = "Host=127.0.0.1;Port=5432;Database=recoverdb_1;Username=postgres;Password=02012006;";
        private void LoadWardsWithCount()
        {

            string query = @"
        SELECT wards.id AS Номер, 
               COUNT(people.last_name) AS Количество,
               wards.max_count AS Вместимость
        FROM wards 
        LEFT JOIN people ON people.ward_id = wards.id
        GROUP BY wards.id, wards.name, wards.max_count, wards.diagnosis_id;";

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(sqlConnectionString))
                {
                    try
                    {
                        connection.Open();
                        NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, connection);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Настроим Chart
                        chart1.Series.Clear();
                        chart1.ChartAreas.Clear();

                        // Добавляем область для диаграммы
                        var chartArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
                        chart1.ChartAreas.Add(chartArea);

                        // Настройка осей
                        chartArea.AxisX.Title = "Номер палаты"; // Название оси X
                        chartArea.AxisY.Title = "Процент загрузки (%)"; // Название оси Y
                        chartArea.AxisX.Interval = 1; // Интервал между метками на оси X
                        chartArea.AxisY.Interval = 10; // Интервал между метками на оси Y

                        // Добавляем заголовок диаграммы
                        chart1.Titles.Clear();
                        chart1.Titles.Add("Процент загрузки палат");

                        // Добавляем серию для данных
                        var series = new System.Windows.Forms.DataVisualization.Charting.Series
                        {
                            Name = "Загрузка",
                            IsVisibleInLegend = true,
                            ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column // Столбчатая диаграмма
                        };

                        // Добавляем данные в серию
                        foreach (DataRow row in dataTable.Rows) // Итерируемся по строкам, а не по индексам
                        {
                            int count = Convert.ToInt32(row["Количество"]);
                            int capacity = Convert.ToInt32(row["Вместимость"]);
                            double percentage = (capacity > 0) ? (count / (double)capacity) * 100 : 0; // Процент загрузки

                            // Добавляем данные в серию
                            series.Points.AddXY(row["Номер"], percentage);
                        }

                        // Настройка размеров диаграммы
                        chart1.Width = 612;
                        chart1.Height = 400;

                        // Добавляем серию в диаграмму
                        chart1.Series.Add(series);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка выполнения запроса: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
