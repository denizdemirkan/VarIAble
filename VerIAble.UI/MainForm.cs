using CsvHelper;
using CsvHelper.Configuration;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
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

        List<Data> Fields = new List<Data>();

        List<Data> CellDatas = new List<Data>();

        List<string> fieldNames = new List<string>();

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
                                                    Data newField = new Data();
                                                    newField.Value = cell;
                                                    Fields.Add(newField);
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
                                    dataGridView1.DataSource = Fields;

                                    // Custom Types Column
                                    comboBoxColumnList.HeaderText = "CustomTypes";
                                    comboBoxColumnList.DataPropertyName = "Name";
                                    comboBoxColumnList.DataSource = CustomTypes;
                                    comboBoxColumnList.DisplayMember = "Name";
                                    comboBoxColumnList.ValueMember = "Name";

                                    dataGridView1.Columns.Add(comboBoxColumnList);
                                    //dataGridView1.Columns[1].DataPropertyName = "Type";
                                    //dataGridView1.Columns.Insert(1, comboBoxColumn);


                                    // SameWith Column
                                    foreach (Data field in Fields)
                                    {
                                        fieldNames.Add(field.Value);
                                    }
                                    DataGridViewComboBoxColumn sameWithColumn = new DataGridViewComboBoxColumn();
                                    sameWithColumn.HeaderText = "SameWith";
                                    sameWithColumn.DataSource = fieldNames;

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
                if (e.ColumnIndex == comboBoxColumnList.Index && e.RowIndex >= 0)
                {
                    DataGridViewComboBoxCell comboBoxCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewComboBoxCell;

                    if (comboBoxCell != null && comboBoxCell.Value != null)
                    {
                        CustomType selectedCustomType = CustomTypes.FirstOrDefault(x => x.Name == comboBoxCell.Value);

                        if (selectedCustomType != null)
                        {
                            Data currentField = Fields.ElementAt(e.RowIndex);
                            //currentField.Type = selectedCustomType;

                            ApplyRulesToField(currentField, selectedCustomType);
                        }
                    }
                }

                dataGridView1.Invalidate();
                dataGridView1.Update();
                
            }

            // On SameWith Change
            if (e.ColumnIndex == 2 && e.RowIndex >= 0)
            {
                DataGridViewComboBoxCell comboBoxCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewComboBoxCell;
                if (comboBoxCell != null)
                {
                    int rowNumber = e.RowIndex;

                    string selectedValue = comboBoxCell.Value.ToString();

                    Fields.ElementAt(rowNumber).MustSameWith = selectedValue;

                    dataGridView1.Invalidate();
                    dataGridView1.Update();
                }
            }
        }

        private void ApplyRules(Data field, Data data)
        {
            data.AllMustLower = field.AllMustLower;
            data.AllMustUpper = field.AllMustUpper;

            data.AllowSpace = field.AllowSpace;
            data.AllowNull = field.AllowNull;
            data.AllowNumerics = field.AllowNumerics;
            data.AllowSpecialCharacters = field.AllowSpecialCharacters;

            data.MaxLenght = field.MaxLenght;
            data.MinLenght = field.MinLenght;
            data.TotalLenght = field.TotalLenght;

            data.MustBeUnique = field.MustBeUnique;
            data.MustBeDecimal = field.MustBeDecimal;
            data.MustBeInteger = field.MustBeInteger;

            data.OnlyLetters = field.OnlyLetters;
            data.OnlyNumerics = field.OnlyNumerics;

            data.MustSameWith = field.MustSameWith;

            data.MustStartsWith = field.MustStartsWith;
            data.MustEndsWith = field.MustEndsWith;
            data.MustContains = field.MustContains;

            data.Pattern = field.Pattern;

            data.AllowedValues = field.AllowedValues;

            data.Type = field.Type;
        }

        // Called After Calcultion
        private void ApplyRulesToField(Data data, CustomType customType)
        {
            data.Type = customType;

            // actually no need for this check. But once application runs with async functions, we may need this.
            if(data.Type == customType)
            {
                data.AllMustLower = data.Type.AllMustLower;
                data.AllMustUpper = data.Type.AllMustUpper;

                data.AllowSpace = data.Type.AllowSpace;
                data.AllowNull = data.Type.AllowNull;
                data.AllowNumerics = data.Type.AllowNumerics;
                data.AllowSpecialCharacters = data.Type.AllowSpecialCharacters;

                data.MaxLenght = data.Type.MaxLenght;
                data.MinLenght = data.Type.MinLenght;
                data.TotalLenght = data.Type.TotalLenght;

                data.MustBeUnique = data.Type.MustBeUnique;
                data.MustBeDecimal = data.Type.MustBeDecimal;
                data.MustBeInteger = data.Type.MustBeInteger;

                data.OnlyLetters = data.Type.OnlyLetters;
                data.OnlyNumerics = data.Type.OnlyNumerics;

                data.MustSameWith = data.Type.MustSameWith;

                data.MustStartsWith = data.Type.MustStartsWith;
                data.MustEndsWith = data.Type.MustEndsWith;
                data.MustContains = data.Type.MustContains;

                data.AllowedValues = data.Type.AllowedValues;

                data.Pattern = data.Type.Pattern;
            }
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
                ApplyRules(Fields.ElementAt(data.CsvIndex % Fields.Count), data);

                // Data rules Applied
            }

            foreach (Data data in CellDatas)
            {
                CustomDataValidator validator = new CustomDataValidator(data, this.Fields, this.CellDatas, this.fieldNames);
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
            var field = string.Join(",", properties.Select(p => p.Name));

            var csvLines = items.Select(item => string.Join(",", properties.Select(p => FormatCsvValue(p.GetValue(item)))));

            return field + Environment.NewLine + string.Join(Environment.NewLine, csvLines);
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
                foreach (Data data in Fields)
                {
                    //if (data.Type.Equals(customType.Type))
                    //{
                    //    ApplyRulesToField(data, customType);
                    //}
                }
            }
        }
    }
}