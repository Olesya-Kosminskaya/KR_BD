using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KR_BD
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            LoadWardsWithCount();
            LoadLabels();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet1.wards". При необходимости она может быть перемещена или удалена.
            this.wardsTableAdapter.Fill(this.dataSet1.wards);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet1.diagnosis". При необходимости она может быть перемещена или удалена.
            this.diagnosisTableAdapter.Fill(this.dataSet1.diagnosis);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet1.people". При необходимости она может быть перемещена или удалена.
            this.peopleTableAdapter.Fill(this.dataSet1.people);
            // Обработка события DataError 
            dataGridView1.DataError += DataGridView1_DataError;
            dataGridView2.DataError += DataGridView2_DataError;
            dataGridView3.DataError += DataGridView3_DataError;
        }

        private System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
        private void LoadLabels()
        {
            // Настроим подсказку для Label
            toolTip.SetToolTip(label4, "Нажмите, чтобы увидеть диаграмму");
            toolTip.SetToolTip(label6, "Нажмите, чтобы увидеть диаграмму");

            // Дополнительно можно настроить параметры ToolTip (по желанию)
            toolTip.ToolTipTitle = "Подсказка";  // Заголовок подсказки
            toolTip.IsBalloon = true;  // Сделать подсказку в виде "шарика"
            toolTip.AutoPopDelay = 5000;  // Время отображения подсказки (в миллисекундах)
            toolTip.InitialDelay = 500;  // Задержка перед появлением подсказки (в миллисекундах)
            toolTip.ReshowDelay = 100;  // Задержка между повторным отображением подсказки (в миллисекундах)
        }
        //===Обработка нажатия кнопки "Добавить" (попытка сохранения изменений в базу данных)
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.peopleBindingSource.EndEdit();
                this.peopleTableAdapter.Update(this.dataSet1.people);
                MessageBox.Show("Новый пациент добавлен в базу", "Операция прошла успешно", MessageBoxButtons.OK);
                LoadWardsWithCount();
                this.peopleTableAdapter.Fill(this.dataSet1.people);
                //LoadData();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Невозможно записать данные: \n{ex.Message}", "Сообщение об ошибке", MessageBoxButtons.OK);
            }
        }
        //===Обработка нажатия кнопки "Изменить" (попытка сохранения изменений в базу данных)
        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                this.Validate();
                this.peopleBindingSource.EndEdit();
                this.peopleTableAdapter.Update(this.dataSet1.people);
                MessageBox.Show("Данные пациента изменены", "Операция прошла успешно", MessageBoxButtons.OK);
                LoadWardsWithCount();
                this.peopleTableAdapter.Fill(this.dataSet1.people);
                //LoadData();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Невозможно изменить данные: \n{ex.Message}",
                "Сообщение об ошибке",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }
        //===Обработка нажатия кнопки "Удалить" (попытка сохранения изменений в базу данных)
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Проверяем, выбрана ли строка
                if (dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Выберите строку для удаления", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Подтверждение удаления
                var confirmResult = MessageBox.Show("Вы уверены, что хотите удалить данные о выбранном пациенте?",
                                                    "Подтверждение удаления",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question);

                if (confirmResult != DialogResult.Yes)
                    return;

                // Удаляем строку через BindingSource
                this.Validate();
                this.peopleBindingSource.RemoveCurrent(); 
                // Сохраняем изменения в базе данных
                this.peopleBindingSource.EndEdit();
                this.peopleTableAdapter.Update(this.dataSet1.people);
                MessageBox.Show("Запись успешно удалена", "Операция прошла успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadWardsWithCount();
                this.peopleTableAdapter.Fill(this.dataSet1.people);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления записи: \n{ex.Message}", "Сообщение об ошибке", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {           
            //if (MessageBox.Show("Вы действительно хотите закрыть программу?",
            //"Подтверждение закрытия", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    Application.Exit();
            //    Environment.Exit(1);
            //    //Process.GetCurrentProcess().Kill();
            //}
            //else
            //{
            //    e.Cancel = true;
            //    Close();
            //}
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
            else if (result == DialogResult.No)
            {
                e.Cancel = true;
            }          
        }
        //private void buttonExit_Click(object sender, EventArgs e)
        //{
        //    if (MessageBox.Show("Закрыть программу?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //    {
        //        Application.Exit();
        //        Environment.Exit(1);
        //        //Process.GetCurrentProcess().Kill();
        //    }
        //    else
        //    {
        //        Close();
        //    }
        //}

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.diagnosisBindingSource.EndEdit();
                this.diagnosisTableAdapter.Update(this.dataSet1.diagnosis);
                MessageBox.Show("Новый диагноз добавлен в базу", "Операция прошла успешно", MessageBoxButtons.OK);
                //LoadData();
                this.diagnosisTableAdapter.Fill(this.dataSet1.diagnosis);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Невозможно записать данные: \n{ex.Message}",
                "Сообщение об ошибке",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }

        // Обработчик DataError
        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Некорректные данные! Проверьте ввод и повторите снова",
                "Ошибка ввода",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            e.ThrowException = false; // Предотвращаем выброс исключения
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.diagnosisBindingSource.EndEdit();
                this.diagnosisTableAdapter.Update(this.dataSet1.diagnosis);
                MessageBox.Show("Данные о диагнозе изменены", "Операция прошла успешно", MessageBoxButtons.OK);
                //LoadData();
                this.diagnosisTableAdapter.Fill(this.dataSet1.diagnosis);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Невозможно изменить данные: \n{ex.Message}",
                "Сообщение об ошибке",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                // Проверяем, выбрана ли строка
                if (dataGridView2.CurrentRow == null)
                {
                    MessageBox.Show("Выберите строку для удаления", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Подтверждение удаления
                var confirmResult = MessageBox.Show("Вы уверены, что хотите удалить данные о выбранном диангнозе?",
                                                    "Подтверждение удаления",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question);

                if (confirmResult != DialogResult.Yes)
                    return;

                this.Validate();
                this.diagnosisBindingSource.RemoveCurrent(); 
                // Сохраняем изменения в базе данных
                this.diagnosisBindingSource.EndEdit();
                this.diagnosisTableAdapter.Update(this.dataSet1.diagnosis);

                MessageBox.Show("Запись успешно удалена", "Операция прошла успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.diagnosisTableAdapter.Fill(this.dataSet1.diagnosis);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления записи: \n{ex.Message}", "Сообщение об ошибке", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.wardsBindingSource.EndEdit();
                this.wardsTableAdapter.Update(this.dataSet1.wards);
                MessageBox.Show("Новая палата добавлена в базу", "Операция прошла успешно", MessageBoxButtons.OK);
                LoadWardsWithCount();
                this.wardsTableAdapter.Fill(this.dataSet1.wards);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Невозможно записать данные: \n{ex.Message}", "Сообщение об ошибке", MessageBoxButtons.OK);
            }
        }
        // Обработчик DataError
        private void DataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Некорректные данные! Проверьте ввод и повторите снова",
                "Ошибка ввода",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            e.ThrowException = false; // Предотвращаем выброс исключения
        }
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.wardsBindingSource.EndEdit();
                this.wardsTableAdapter.Update(this.dataSet1.wards);
                MessageBox.Show("Данные о палате изменены", "Операция прошла успешно", MessageBoxButtons.OK);
                LoadWardsWithCount();
                this.wardsTableAdapter.Fill(this.dataSet1.wards);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Невозможно изменить данные: \n{ex.Message}",
                "Сообщение об ошибке",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                // Проверяем, выбрана ли строка
                if (dataGridView3.CurrentRow == null)
                {
                    MessageBox.Show("Выберите строку для удаления", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Подтверждение удаления
                var confirmResult = MessageBox.Show("Вы уверены, что хотите удалить данные о выбранной палате?",
                                                    "Подтверждение удаления",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question);

                if (confirmResult != DialogResult.Yes)
                    return;

                this.Validate();
                this.wardsBindingSource.RemoveCurrent();
                // Сохраняем изменения в базе данных
                this.wardsBindingSource.EndEdit();
                this.wardsTableAdapter.Update(this.dataSet1.wards);
                MessageBox.Show("Запись успешно удалена", "Операция прошла успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadWardsWithCount();
                this.wardsTableAdapter.Fill(this.dataSet1.wards);
                this.peopleTableAdapter.Fill(this.dataSet1.people);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления записи: \n{ex.Message}", "Сообщение об ошибке", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Обработчик DataError
        private void DataGridView3_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Некорректные данные! Проверьте ввод и повторите снова",
                "Ошибка ввода",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            e.ThrowException = false; // Предотвращаем выброс исключения
        }

        public string sqlConnectionString = "Host=127.0.0.1;Port=5432;Database=recoverdb_1;Username=postgres;Password=02012006;"/*ReadConfigFromFile("db_config.txt")*/;
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
                        dataGridView4.Columns[0].Width = 50;
                        dataGridView4.Columns[1].Width = 75;
                        dataGridView4.Columns[2].Width = 80;
                        dataGridView5.DataSource = dtData;
                       // dtData.Load(cmd.ExecuteReader());
                        dataGridView5.Columns[0].Width = 50;
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

        private void btnReportForm_Click(object sender, EventArgs e)
        {
            DataTable[] dataTableArray = { this.dataSet1.people, this.dataSet1.diagnosis, this.dataSet1.wards };
            ReportForm fm2 = new ReportForm(dataTableArray);
            fm2.ShowDialog();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            ChartForm chartForm = new ChartForm();
            chartForm.ShowDialog();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            ChartForm chartForm = new ChartForm();
            chartForm.ShowDialog();
        }
    }
}
