using CsvHelper;
using CsvHelper.Configuration;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using VerIAble.UI.Classes;
using VerIAble.UI.Forms;
using VerIAble.UI.HelperClasses;

namespace VerIAble.UI
{
    public partial class MainForm : Form
    {
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        private static extern bool FreeConsole();

        List<string> Errors = new List<string>();

        List<Field> Headers = new List<Field>();

        List<Data> CellDatas = new List<Data>();

        List<string> headerCellValues = new List<string>();

        List<CustomType> CustomTypes = new List<CustomType>();

        List<ValidationResult> results = new List<ValidationResult>();

        // Optimize
        private DataGridView dataGridView;
        DataGridViewComboBoxColumn comboBoxColumnList = new DataGridViewComboBoxColumn();
        // Optimize

        public MainForm()
        {
            InitializeComponent();
            AllocConsole();

            CustomType integerType = new CustomType("Integer")
            {
                OnlyNumerics = true,
                OnlyLetters = false,

                AllMustLower = false,
                AllMustUpper = false,

                AllowNumerics = true,
                AllowSpace = false,
                AllowSpecialCharacters = false
            };
            CustomType stringType = new CustomType("String")
            {
                OnlyNumerics = false,
                OnlyLetters = false,

                AllMustLower = false,
                AllMustUpper = false,

                AllowNumerics = true,
                AllowSpace = true,
                AllowSpecialCharacters = true
            };
            CustomType emailType = new CustomType("Email")
            {
                OnlyNumerics = false,
                OnlyLetters = false,

                AllMustLower = false,
                AllMustUpper = false,

                MustBeUnique = true,

                AllowNumerics = true,
                AllowSpace = false,
                AllowSpecialCharacters = true
            };
            CustomType uidType = new CustomType("UID")
            {
                OnlyNumerics = true,
                OnlyLetters = false,

                AllMustLower = false,
                AllMustUpper = false,

                MustBeUnique = true,

                AllowNumerics = true,
                AllowSpace = false,
                AllowSpecialCharacters = false,
                MustBeInteger = true
            };
            CustomType phoneNumberType = new CustomType("Phone Number")
            {
                TotalLenght = 10,

                OnlyNumerics = true,
                OnlyLetters = false,

                AllMustLower = false,
                AllMustUpper = false,

                MustBeInteger = true,
                MustBeUnique = true,

                AllowNumerics = true,
                AllowSpace = false,
                AllowSpecialCharacters = false
            };
            CustomType nameType = new CustomType("Name")
            {
                OnlyNumerics = false,
                OnlyLetters = true,

                AllMustLower = false,
                AllMustUpper = false,

                AllowNumerics = false,
                AllowSpace = true,
                AllowSpecialCharacters = false,

                MustBeUnique = false,
                MustBeInteger = false,
                MustBeDecimal = false,
            };

            CustomTypes.Add(integerType);
            CustomTypes.Add(phoneNumberType);
            CustomTypes.Add(nameType);
            CustomTypes.Add(stringType);
            CustomTypes.Add(emailType);
            CustomTypes.Add(uidType);
        }

        private void loadDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to load CSV file? Your unsaved changes will be LOST!", "Load File", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (CsvSeperatorQuestionForm seperatorQuestion = new CsvSeperatorQuestionForm())
                {
                    if (seperatorQuestion.ShowDialog() == DialogResult.OK)
                    {
                        {
                            using (OpenFileDialog openFileDialog = new OpenFileDialog())
                            {
                                openFileDialog.Filter = "CSV file (*.csv)|*.csv";
                                openFileDialog.Title = "Select CSV File";

                                if (openFileDialog.ShowDialog() == DialogResult.OK)
                                {
                                    using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                                    {
                                        int lineCount = 1;
                                        int indexCount = 0;
                                        while (!reader.EndOfStream)
                                        {

                                            string line = reader.ReadLine();
                                            string[] cells = line.Split(seperatorQuestion.SelectedOption);

                                            foreach (string cell in cells)
                                            {
                                                if (lineCount == 1)
                                                {
                                                    Field newHeader = new Field();
                                                    newHeader.Value = cell;
                                                    Headers.Add(newHeader);
                                                   // Headers loaded
                                                }
                                                else
                                                {
                                                    Data newData = new Data();
                                                    newData.Value = cell;
                                                    newData.CsvIndex = indexCount;
                                                    CellDatas.Add(newData);
                                                    indexCount++;
                                                    // Data cells loaded
                                                }
                                            }
                                            lineCount++;
                                        }

                                    }
                                    dataGridView1.DataSource = Headers;

                                    // Custom Types Column
                                    comboBoxColumnList.HeaderText = "CustomTypes";
                                    comboBoxColumnList.DataPropertyName = "Type";
                                    comboBoxColumnList.DataSource = CustomTypes;
                                    comboBoxColumnList.DisplayMember = "Type";
                                    comboBoxColumnList.ValueMember = "Type";

                                    dataGridView1.Columns.Add(comboBoxColumnList);
                                    //dataGridView1.Columns[1].DataPropertyName = "Type";
                                    //dataGridView1.Columns.Insert(1, comboBoxColumn);


                                    // SameWith Column
                                    foreach (Field header in Headers)
                                    {
                                        headerCellValues.Add(header.Value);
                                    }
                                    DataGridViewComboBoxColumn sameWithColumn = new DataGridViewComboBoxColumn();
                                    sameWithColumn.HeaderText = "SameWith";
                                    sameWithColumn.DataSource = headerCellValues;

                                    dataGridView1.Columns.Add(sameWithColumn);

                                    //dataGridView1.Columns[2].DataPropertyName = "MustSameWith";
                                    //dataGridView1.Columns.Insert(2, sameWithColumn);

                                }
                            }
                        }
                    }
                }
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // On CustomType Change
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                DataGridViewComboBoxCell comboBoxCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewComboBoxCell;
                if (comboBoxCell != null)
                {
                    int rowNumber = e.RowIndex;

                    string selectedValue = comboBoxCell.Value.ToString();

                    Headers.ElementAt(rowNumber).Type = selectedValue;


                    foreach (CustomType ct in CustomTypes)
                    {
                        if (ct.Type.Equals(selectedValue))
                        {
                            ApplyRulesToField(Headers.ElementAt(rowNumber), ct);
                        }
                    }

                    dataGridView1.Invalidate();
                    dataGridView1.Update();
                }
            }

            // On SameWith Change
            if (e.ColumnIndex == 2 && e.RowIndex >= 0)
            {
                DataGridViewComboBoxCell comboBoxCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewComboBoxCell;
                if (comboBoxCell != null)
                {
                    int rowNumber = e.RowIndex;

                    string selectedValue = comboBoxCell.Value.ToString();

                    Headers.ElementAt(rowNumber).MustSameWith = selectedValue;

                    dataGridView1.Invalidate();
                    dataGridView1.Update();
                }
            }
        }

        private void ApplyRules(Field header, Data data)
        {
            data.AllMustLower = header.AllMustLower;
            data.AllMustUpper = header.AllMustUpper;

            data.AllowSpace = header.AllowSpace;
            data.AllowNull = header.AllowNull;
            data.AllowNumerics = header.AllowNumerics;
            data.AllowSpecialCharacters = header.AllowSpecialCharacters;

            data.MaxLenght = header.MaxLenght;
            data.MinLenght = header.MinLenght;
            data.TotalLenght = header.TotalLenght;

            data.MustBeUnique = header.MustBeUnique;
            data.MustBeDecimal = header.MustBeDecimal;
            data.MustBeInteger = header.MustBeInteger;

            data.OnlyLetters = header.OnlyLetters;
            data.OnlyNumerics = header.OnlyNumerics;

            data.MustSameWith = header.MustSameWith;

            data.MustStartsWith = header.MustStartsWith;
            data.MustEndsWith = header.MustEndsWith;
            data.MustContains = header.MustContains;

            data.Pattern = header.Pattern;

            data.AllowedValues = header.AllowedValues;

            data.Type = header.Type;
        }

        // Called After Calcultion
        private void ApplyRulesToField(Field field, CustomType customType)
        {
            field.AllMustLower = customType.AllMustLower;
            field.AllMustUpper = customType.AllMustUpper;

            field.AllowSpace = customType.AllowSpace;
            field.AllowNull = customType.AllowNull;
            field.AllowNumerics = customType.AllowNumerics;
            field.AllowSpecialCharacters = customType.AllowSpecialCharacters;

            field.MaxLenght = customType.MaxLenght;
            field.MinLenght = customType.MinLenght;
            field.TotalLenght = customType.TotalLenght;

            field.MustBeUnique = customType.MustBeUnique;
            field.MustBeDecimal = customType.MustBeDecimal;
            field.MustBeInteger = customType.MustBeInteger;

            field.OnlyLetters = customType.OnlyLetters;
            field.OnlyNumerics = customType.OnlyNumerics;

            field.MustSameWith = customType.MustSameWith;

            field.MustStartsWith = customType.MustStartsWith;
            field.MustEndsWith = customType.MustEndsWith;
            field.MustContains = customType.MustContains;

            field.AllowedValues = customType.AllowedValues;

            field.Pattern = customType.Pattern;

            field.Type = customType.Type;
        }

        private void typesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomTypesForm customTypesForm = new CustomTypesForm(CustomTypes, this);
            customTypesForm.Show();
        }

        private void consoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            results.Clear();
            Console.WriteLine("--------------------------------------------------------------------------");

            foreach (Data data in CellDatas)
            {
                ApplyRules(Headers.ElementAt(data.CsvIndex % Headers.Count), data);

                // Data rules Applied
            }

            foreach (Data data in CellDatas)
            {
                CustomDataValidator validator = new CustomDataValidator(data, this.Headers, this.CellDatas, this.headerCellValues);
                ValidationResult result = validator.Validate(data);
                if (!result.IsValid)
                {
                    results.Add(result);
                }
            }

            // Loop over all failures
            foreach (ValidationResult validationResult in results)
            {
                foreach (ValidationFailure failure in validationResult.Errors)
                {
                    Console.WriteLine(failure.ErrorMessage);
                }
            }
            Console.WriteLine("--------------------------------------------------------------------------");
        }

        private void exportSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputDialog inputDialog = new InputDialog();
            DialogResult csvFileName = inputDialog.ShowDialog();

            Console.WriteLine(inputDialog.FileName);
            if (csvFileName == DialogResult.OK)
            {
                string settingsDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
    "Settings");

                string csvContent = ToCsv(CustomTypes);

                File.WriteAllText(Environment.CurrentDirectory + "\\" + inputDialog.FileName + ".csv", csvContent);

                Console.WriteLine("Export File: " + Environment.CurrentDirectory + "\\" + inputDialog.FileName + ".csv");
            }


        }
        private void importSettingsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV file (*.csv)|*.csv";
                openFileDialog.Title = "Select CSV File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    CustomTypes = ReadCsvToCustomList(filePath);

                    MessageBox.Show("Settings Imported!", "Import Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Console.WriteLine("Import File: " + filePath);

                    comboBoxColumnList.DataSource = CustomTypes;

                    //dataGridView1.Columns.Clear();

                    //foreach()

                }
            }
        }

        // Together
        static string ToCsv<T>(IEnumerable<T> items)
        {
            var properties = typeof(T).GetProperties();
            var header = string.Join(",", properties.Select(p => p.Name));

            var csvLines = items.Select(item => string.Join(",", properties.Select(p => FormatCsvValue(p.GetValue(item)))));

            return header + Environment.NewLine + string.Join(Environment.NewLine, csvLines);
        }
        static string FormatCsvValue(object value)
        {
            if (value == null)
                return "";

            string stringValue = value.ToString();
            if (stringValue.Contains(',') || stringValue.Contains('"'))
                return $"\"{stringValue.Replace("\"", "\"\"")}\"";
            return stringValue;
        }
        // Together

        // Make Here Optimization
        static List<CustomType> ReadCsvToCustomList(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                return csv.GetRecords<CustomType>().ToList();
            }
        }

        private void statisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        // Saves Changes on CustomTypes to Fields when clicked Save Changes Button in CustomTypeForm
        public void saveChanges()
        {
            foreach (CustomType customType in CustomTypes)
            {
                foreach (Field field in Headers)
                {
                    if (field.Type.Equals(customType.Type))
                    {
                        ApplyRulesToField(field, customType);
                    }
                }
            }
        }
    }
}