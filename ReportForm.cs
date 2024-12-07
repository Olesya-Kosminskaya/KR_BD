using iText.IO.Font;
using iText.Kernel.Font;
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

namespace KR_BD
{
    public partial class ReportForm : Form
    {
        ReportGenerator reportGenerator = new ReportGenerator();
        DataTable[] tablesToInclude = null;
        public ReportForm(DataTable[] tablesToInclude)
        {
            InitializeComponent();
            this.tablesToInclude = tablesToInclude;
        }

        public string sqlConnectionString = "Host=127.0.0.1;Port=5432;Database=recoverdb_1;Username=postgres;Password=02012006;";
        
        private void btnGenerateXLSX_Click(object sender, EventArgs e)
        {
            var reportGenerator = new ReportGenerator();
            List<DataTable> selectedTables = new List<DataTable>();

            // Получение выбранных элементов из CheckedListBox
            foreach (var item in checkedListBoxTables.CheckedItems)
            {
                string tableName = item.ToString();

                // В зависимости от выбора добавляем соответствующую таблицу
                switch (tableName)
                {
                    case "Пациенты":
                        selectedTables.Add(tablesToInclude[0]);
                        break;
                    case "Диагнозы":
                        selectedTables.Add(tablesToInclude[1]);
                        break;
                    case "Палаты":
                        selectedTables.Add(tablesToInclude[2]);
                        break;
                }
            }

            if (selectedTables.Count > 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    Title = "Сохранить отчёт",
                    FileName = "Report.xlsx"
                };
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    // Пользовательские названия столбцов
                    Dictionary<string, string> columnNames = new Dictionary<string, string>
            {
                { "ID", "Идентификатор" },
                { "Name", "Имя" },
                { "Diagnosis", "Диагноз" },
                { "MaxCount", "Макс. вместимость" }
            };
                    // Генерация отчёта
                    //string filePath = @"C:\Work\Lesya\3\5 семестр\БД\KR_BD\bin\x64\Debug\Report.xlsx";
                    reportGenerator.GenerateXLSXReport(selectedTables.ToArray(), filePath);
                }
            }
            else
            {
                MessageBox.Show("Не выбрано ни одной таблицы для отчёта", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnGeneratePDF_Click(object sender, EventArgs e)
        {
            List<DataTable> selectedTables = new List<DataTable>();

            foreach (var item in checkedListBoxTables.CheckedItems)
            {
                string tableName = item.ToString();

                switch (tableName)
                {
                    case "Пациенты":
                        selectedTables.Add(tablesToInclude[0]);
                        break;
                    case "Диагнозы":
                        selectedTables.Add(tablesToInclude[1]);
                        break;
                    case "Палаты":
                        selectedTables.Add(tablesToInclude[2]);
                        break;
                }
            }

            if (selectedTables.Count > 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF Files|*.pdf",
                    Title = "Сохранить отчёт",
                    FileName = "Report.pdf"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    reportGenerator.GeneratePDFReport(selectedTables.ToArray(), filePath);
                }
            }
            else
            {
                MessageBox.Show("Не выбрано ни одной таблицы для отчёта", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        //private void ReportForm_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    var result = MessageBox.Show(
        //     "Вы действительно хотите закрыть окно формирования отчётов?",
        //     "Подтверждение закрытия",
        //     MessageBoxButtons.YesNo,
        //     MessageBoxIcon.Question);

        //    if (result == DialogResult.No)
        //    {
        //        e.Cancel = true;
        //    }
        //}

        
    }
}
