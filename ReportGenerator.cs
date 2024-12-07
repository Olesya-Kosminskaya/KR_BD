using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Input;
using System.Diagnostics;
using iText.IO.Font;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace KR_BD
{
    public class ReportGenerator
    {
        // Словарь для пользовательских названий столбцов
        Dictionary<string, Dictionary<string, string>> tableColumnNames = new Dictionary<string, Dictionary<string, string>>()
        {
            { "people", new Dictionary<string, string>
                {
                    { "id", "Код пациента" },
                    { "first_name", "Фамилия" },
                    { "last_name", "Имя" },
                    { "father_name", "Отчество" },
                    { "diagnosis_id", "Код диагноза" },
                    { "ward_id", "Номер палаты" }
                }
            },
            { "diagnosis", new Dictionary<string, string>
                {
                    { "id", "Код диагноза" },
                    { "name", "Название" }
                }
            },
            { "wards", new Dictionary<string, string>
                {
                    { "id", "Номер палаты" },
                    { "name", "Название" },
                    { "max_count", "Вместимость" },
                    { "diagnosis_id", "Код диагноза" }
                }
            }
        };

        // Метод для генерации отчёта
        public void GenerateXLSXReport(System.Data.DataTable[] tablesToInclude, string filePath)
        {
            Excel.Application excelApp = null;
            Excel.Workbook workbook = null;
            try
            {
                // Создание нового Excel-приложения
                excelApp = new Excel.Application();
                workbook = excelApp.Workbooks.Add();

                for (int i = 0; i < tablesToInclude.Length; i++)
                {
                    DataTable table = tablesToInclude[i];
                    string tableName = table.TableName;

                    // Получаем названия столбцов для этой таблицы
                    var customColumnNames = tableColumnNames.ContainsKey(tableName) ? tableColumnNames[tableName] : new Dictionary<string, string>();
                    string tName = "";
                    if (table.TableName == "people") { tName = "Пациенты"; }
                    if (table.TableName == "diagnosis") { tName = "Диагнозы"; }
                    if (table.TableName == "wards") { tName = "Палаты"; }

                    // Добавление нового листа в Excel
                    Excel.Worksheet worksheet = i == 0
                        ? (Excel.Worksheet)workbook.Worksheets[1]
                        : (Excel.Worksheet)workbook.Worksheets.Add();
                    worksheet.Name = tName;

                    // Заполнение заголовков столбцов с учётом пользовательских названий
                    for (int col = 0; col < table.Columns.Count; col++)
                    {
                        string columnName = table.Columns[col].ColumnName;
                        worksheet.Cells[1, col + 1] = customColumnNames.ContainsKey(columnName) ? customColumnNames[columnName] : columnName;
                    }

                    // Заполнение строк таблицы
                    for (int row = 0; row < table.Rows.Count; row++)
                    {
                        for (int col = 0; col < table.Columns.Count; col++)
                        {
                            worksheet.Cells[row + 2, col + 1] = table.Rows[row][col];
                        }
                    }

                    // Автоматическая настройка ширины столбцов
                    worksheet.Columns.AutoFit();
                }

                // Сохранение файла
                workbook.SaveAs(filePath);
                MessageBox.Show("Отчёт успешно создан!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании отчёта: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Освобождение ресурсов
                if (workbook != null)
                {
                    workbook.Close();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                }

                if (excelApp != null)
                {
                    excelApp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                }
            }
        }

        public void GeneratePDFReport(System.Data.DataTable[] tablesToInclude, string filePath)
        {
            try
            {
                string fontPath = "DejaVuSerif.ttf";
                // Подключение шрифта
                PdfFont font = PdfFontFactory.CreateFont(fontPath, PdfEncodings.IDENTITY_H);
                string fileName = Path.GetFileName(filePath);
                using (PdfWriter writer = new PdfWriter(fileName))
                using (PdfDocument pdf = new PdfDocument(writer))
                {

                    Document document = new Document(pdf);

                    foreach (DataTable table in tablesToInclude)
                    {
                        string tableName = table.TableName;

                        // Получаем названия столбцов для этой таблицы
                        var customColumnNames = tableColumnNames.ContainsKey(tableName) ? tableColumnNames[tableName] : new Dictionary<string, string>();
                        string tName = "";
                        if (table.TableName == "people") { tName = "Пациенты"; }
                        if (table.TableName == "diagnosis")  { tName = "Диагнозы"; }
                        if (table.TableName == "wards") { tName = "Палаты"; }

                        // Заголовок таблицы
                        Color colLightCoral = new DeviceRgb(System.Drawing.Color.LightCoral.R, System.Drawing.Color.LightCoral.G, System.Drawing.Color.LightCoral.B);
                        document.Add(new Paragraph(tName).SetFont(font)
                            .SetFontSize(16).SetBackgroundColor(colLightCoral)
                            .SetTextAlignment(TextAlignment.CENTER));

                        // Создаём PDF-таблицу с количеством столбцов равным количеству столбцов DataTable
                        iText.Layout.Element.Table pdfTable = new iText.Layout.Element.Table(table.Columns.Count);
                        pdfTable.SetWidth(UnitValue.CreatePercentValue(100)); // Растянуть таблицу на 100% ширины страницы

                        // Добавляем заголовки столбцов
                        foreach (DataColumn column in table.Columns)
                        {
                            string columnName = column.ColumnName;
                            string displayName = customColumnNames.ContainsKey(columnName) ? customColumnNames[columnName] : columnName;
                            pdfTable.AddHeaderCell(new Cell()
                                .Add(new Paragraph(displayName).SetFont(font))
                                .SetBackgroundColor(iText.Kernel.Colors.ColorConstants.CYAN));
                        }

                        // Добавляем строки данных
                        foreach (DataRow row in table.Rows)
                        {
                            foreach (var cell in row.ItemArray)
                            {
                                pdfTable.AddCell(new Cell().Add(new Paragraph(cell?.ToString() ?? string.Empty).SetFont(font)));
                            }
                        }

                        // Добавляем таблицу в документ
                        document.Add(pdfTable);
                        document.Add(new Paragraph("\n")); // Разделитель между таблицами
                    }
                    document.Close();
                    MessageBox.Show("Отчёт успешно создан!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании отчёта: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




    }
}
