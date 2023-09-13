using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerIAble.UI.Classes;
using VerIAble.UI.Forms;

namespace VerIAble.UI.HelperClasses
{
    public static class ImportExportHelper
    {

        public static void ExportCSV<T>(IEnumerable<T> items)
        {
            InputDialog inputDialog = new InputDialog();
            DialogResult csvFileName = inputDialog.ShowDialog();

            Console.WriteLine(inputDialog.FileName);
            if (csvFileName == DialogResult.OK)
            {
                string settingsDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "Settings");

                string csvContent = ToCsv(items);

                File.WriteAllText(Environment.CurrentDirectory + "\\" + inputDialog.FileName + ".csv", csvContent);

                Console.WriteLine("Export File: " + Environment.CurrentDirectory + "\\" + inputDialog.FileName + ".csv");
            }
        }

        public static IEnumerable<T> ImportCSV<T>()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV file (*.csv)|*.csv";
                openFileDialog.Title = "Select CSV File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    using (var reader = new StreamReader(filePath))
                    {
                        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                        {
                            MessageBox.Show("Settings Imported!", "Import Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            return csv.GetRecords<T>().ToList();
                        }
                    }
                }
                else
                {
                    return null;
                }
            }

        }

        private static string ToCsv<T>(IEnumerable<T> items)
        {
            var properties = typeof(T).GetProperties();
            var field = string.Join(",", properties.Select(p => p.Name));

            var csvLines = items.Select(item => string.Join(",", properties.Select(p => FormatCsvValue(p.GetValue(item)))));

            return field + Environment.NewLine + string.Join(Environment.NewLine, csvLines);
        }
        private static string FormatCsvValue(object value)
        {
            if (value == null)
                return "";

            string stringValue = value.ToString();
            if (stringValue.Contains(',') || stringValue.Contains('"'))
                return $"\"{stringValue.Replace("\"", "\"\"")}\"";
            return stringValue;
        }
    }
}
