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

        public List<Data> Fields = new List<Data>();

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
                DataLoader.LoadFromCSV(Fields, CellDatas);

                dataGridView1.DataSource = Fields;

                // Custom Types Column
                comboBoxColumnList.HeaderText = "CustomTypes";
                comboBoxColumnList.DataPropertyName = "Name";
                comboBoxColumnList.DataSource = CustomTypes;
                comboBoxColumnList.DisplayMember = "Name";
                comboBoxColumnList.ValueMember = "Name";

                //dataGridView1.Columns.Add(comboBoxColumnList);
                dataGridView1.Columns.Insert(1, comboBoxColumnList);

                // SameWith Column
                foreach (Data field in Fields)
                {
                    fieldNames.Add(field.Value);
                }
                DataGridViewComboBoxColumn sameWithColumn = new DataGridViewComboBoxColumn();
                sameWithColumn.HeaderText = "SameWith";
                sameWithColumn.DataSource = fieldNames;

                //dataGridView1.Columns.Add(sameWithColumn);
                dataGridView1.Columns.Insert(2, sameWithColumn);
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

                            FieldCopyHelper.ApplyRulesToField(currentField, selectedCustomType);
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
                FieldCopyHelper.ApplyRulesToData(Fields.ElementAt(data.CsvIndex % Fields.Count), data);
                // Data rules Applied
            }

            ViolationCalculator.SeeViolationsOnConlose(CellDatas, Fields, fieldNames, results);

            Console.WriteLine("--------------------------------------------------------------------------");
        }

        private void exportSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportExportHelper.ExportCSV(CustomTypes);
        }
        private void importSettingsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CustomTypes = ImportExportHelper.ImportCSV<CustomType>().ToList();

            comboBoxColumnList.DataSource = CustomTypes;
        }
        private void statisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        // Saves Changes on CustomTypes to Fields when clicked Save Changes Button in CustomTypeForm. Not Implemented yet.
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