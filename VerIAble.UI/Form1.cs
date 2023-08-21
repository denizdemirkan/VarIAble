using FluentValidation;
using FluentValidation.Results;

using System.Runtime.InteropServices;

using VerIAble.UI.Classes;

namespace VerIAble.UI
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        private static extern bool FreeConsole();

        List<string> Errors = new List<string>();

        List<Field> Headers = new List<Field>();

        List<Data> CellDatas = new List<Data>();

        List<string> Types = new List<string>();

        List<string> headerCellValues = new List<string>();

        List<CustomType> CustomTypes = new List<CustomType>();

        List<ValidationResult> results = new List<ValidationResult>();

        string csvFilePath = "C:\\Users\\deniz\\OneDrive\\Masaüstü\\example_set.csv";
        //string csvFilePath = "C:\\Users\\deniz\\OneDrive\\Masaüstü\\example.csv";

        public Form1()
        {
            InitializeComponent();
            AllocConsole();

            Types.Add("Integer");
            Types.Add("Decimal");
            Types.Add("String");
            Types.Add("Email");
            Types.Add("Date");
            Types.Add("UID");
            Types.Add("Phone Number");
            Types.Add("Name");

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
            // Start Reading
            using (StreamReader reader = new StreamReader(csvFilePath))
            {
                int lineCount = 1;
                int indexCount = 0;
                while (!reader.EndOfStream)
                {

                    string line = reader.ReadLine();
                    string[] cells = line.Split(',');

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
            
            // Type Column for Default Settings
            //DataGridViewComboBoxColumn typeColumn = new DataGridViewComboBoxColumn();
            //typeColumn.HeaderText = "Type";
            //typeColumn.DataSource = Types;
            //dataGridView1.Columns[1].DataPropertyName = "Type";
            //dataGridView1.Columns.Insert(1, typeColumn);

            // Custom Types Column
            DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn();
            comboBoxColumn.HeaderText = "CustomTypes";
            comboBoxColumn.DataPropertyName = "Type"; // Veri baðlantýsý için CustomType sýnýfýnýn özelliði
            comboBoxColumn.DataSource = CustomTypes; // ComboBox içeriði
            comboBoxColumn.DisplayMember = "Type"; // Görüntülenecek metin özelliði
            comboBoxColumn.ValueMember = "Type"; // Seçilen deðeri temsil eden özellik

            dataGridView1.Columns[1].DataPropertyName = "Type";
            dataGridView1.Columns.Insert(1, comboBoxColumn);


            // SameWith Column
            foreach (Field header in Headers)
            {
                headerCellValues.Add(header.Value);
            }
            DataGridViewComboBoxColumn sameWithColumn = new DataGridViewComboBoxColumn();
            sameWithColumn.HeaderText = "SameWith";
            sameWithColumn.DataSource = headerCellValues;

            dataGridView1.Columns[2].DataPropertyName = "MustSameWith";
            dataGridView1.Columns.Insert(2, sameWithColumn);


            //// DataGridView'e verileri yükleme
            //dataGridView1.DataSource = CustomTypes;
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // On Type Change
            //if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            //{
            //    DataGridViewComboBoxCell comboBoxCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewComboBoxCell;
            //    if (comboBoxCell != null)
            //    {
            //        int rowNumber = e.RowIndex;

            //        string selectedValue = comboBoxCell.Value.ToString();

            //        Headers.ElementAt(rowNumber).Type = selectedValue;

            //        TypeDefaults(Headers.ElementAt(rowNumber));

            //        dataGridView1.Invalidate();
            //        dataGridView1.Update();
            //    }
            //}

            // On CustomType Change
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                DataGridViewComboBoxCell comboBoxCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewComboBoxCell;
                if (comboBoxCell != null)
                {
                    int rowNumber = e.RowIndex;

                    string selectedValue = comboBoxCell.Value.ToString();

                    Headers.ElementAt(rowNumber).Type = selectedValue;

                    //TypeDefaults(Headers.ElementAt(rowNumber));
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

        private void calculateViolationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Data data in CellDatas)
            {
                ApplyRules(Headers.ElementAt(data.CsvIndex % Headers.Count), data);
                // Data rules Applied
            }


            foreach (Data data in CellDatas)
            {
                DataValidator validator = new DataValidator(data, this.Headers, this.CellDatas, this.headerCellValues);
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

            data.Type = header.Type;
        }

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

            field.Type = customType.Type;
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //private void TypeDefaults(Field cell)
        //{
        //    if (cell.Type.Equals("Integer"))
        //    {
        //        cell.OnlyNumerics = true;
        //        cell.OnlyLetters = false;

        //        cell.AllMustLower = false;
        //        cell.AllMustUpper = false;

        //        cell.AllowNumerics = true;
        //        cell.AllowSpace = false;
        //        cell.AllowSpecialCharacters = false;

        //    }
        //    if (cell.Type.Equals("String"))
        //    {
        //        cell.OnlyNumerics = false;
        //        cell.OnlyLetters = false;

        //        cell.AllMustLower = false;
        //        cell.AllMustUpper = false;

        //        cell.AllowNumerics = true;
        //        cell.AllowSpace = true;
        //        cell.AllowSpecialCharacters = true;
        //    }
        //    if (cell.Type.Equals("Email"))
        //    {
        //        cell.OnlyNumerics = false;
        //        cell.OnlyLetters = false;

        //        cell.AllMustLower = false;
        //        cell.AllMustUpper = false;

        //        cell.MustBeUnique = true;
        //        cell.AllowNumerics = true;
        //        cell.AllowSpace = false;
        //        cell.AllowSpecialCharacters = true;
        //    }
        //    if (cell.Type.Equals("UID"))
        //    {
        //        cell.OnlyNumerics = true;
        //        cell.OnlyLetters = false;

        //        cell.AllMustLower = false;
        //        cell.AllMustUpper = false;

        //        cell.AllowNumerics = true;
        //        cell.AllowSpace = false;
        //        cell.AllowSpecialCharacters = false;

        //        cell.MustBeUnique = true;
        //        cell.AllowNull = false;
        //        cell.MustBeInteger = true;
        //    }
        //    if (cell.Type.Equals("Phone Number"))
        //    {
        //        cell.TotalLenght = 10;

        //        cell.OnlyNumerics = true;
        //        cell.OnlyLetters = false;

        //        cell.AllMustLower = false;
        //        cell.AllMustUpper = false;

        //        cell.AllowNumerics = true;
        //        cell.AllowSpace = false;
        //        cell.AllowSpecialCharacters = false;

        //        cell.MustBeUnique = true;
        //        cell.AllowNull = false;
        //        cell.MustBeInteger = true;
        //    }
        //    if (cell.Type.Equals("Name"))
        //    {
        //        cell.OnlyNumerics = false;
        //        cell.OnlyLetters = true;

        //        cell.AllMustLower = false;
        //        cell.AllMustUpper = false;

        //        cell.AllowNumerics = false;
        //        cell.AllowSpace = true;
        //        cell.AllowSpecialCharacters = false;

        //        cell.MustBeUnique = false;
        //        cell.AllowNull = false;
        //        cell.MustBeInteger = false;
        //    }

        //}

        private void typesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomTypesForm customTypesForm = new CustomTypesForm(CustomTypes);
            customTypesForm.Show();
        }
    }

    public class DataValidator : AbstractValidator<Data>
    {
        private List<Field> headers;
        private List<Data> datas;
        private List<string> headerValues;
        public DataValidator(Data data, List<Field> headers, List<Data> datas, List<string> headerValues)
        {
            this.headers = headers;
            this.datas = datas;
            this.headerValues = headerValues;
            int rawNumberOfData = data.CsvIndex / headers.Count + 2;
            int columnNumberOfData = data.CsvIndex % headers.Count + 1;

            if (data.Type.Equals("Email")) // Develop This for default settings
            {
                RuleFor(x => x.Value).EmailAddress().WithMessage(ViolationMessage(rawNumberOfData, columnNumberOfData, "Invalid EMAIL Format!" + "// Violation with: " + data.Value));
            }
            if (!data.AllowNull)
            {
                RuleFor(x => x.Value).NotEmpty().WithMessage(ViolationMessage(rawNumberOfData, columnNumberOfData, "Can not be EMPTY!"));
            }
            if (!data.AllowNumerics)
            {
                RuleFor(x => x.Value).Must(AllowNumerics).WithMessage(ViolationMessage(rawNumberOfData, columnNumberOfData, "Numerics not ALLOWED!" + "// Violation with: " + data.Value));
            }
            if (data.OnlyNumerics)
            {
                RuleFor(x => x.Value).Must(OnlyNumeric).WithMessage(ViolationMessage(rawNumberOfData, columnNumberOfData, "ONLY Numerics ALLOWED!" + "// Violation with: " + data.Value));
            }
            if (data.OnlyLetters)
            {
                RuleFor(x => x.Value).Must(OnlyLetter).WithMessage(ViolationMessage(rawNumberOfData, columnNumberOfData, "ONLY Letters ALLOWED! " + "// Violation with: " + data.Value));
            }
            if (data.MustBeUnique)
            {
                RuleFor(x => x).Must(IsUnique).WithMessage(ViolationMessage(rawNumberOfData, columnNumberOfData, "Must be Unique" + "// Violation with: " + data.Value));
            }
            if (data.TotalLenght != 0)
            {
                RuleFor(x => x.Value).Length(data.TotalLenght).WithMessage(ViolationMessage(rawNumberOfData, columnNumberOfData, "Length must be EXACTLY: " + data.TotalLenght + " // Violation with: " + data.Value));
            }
            if (data.TotalLenght == 0)
            {
                RuleFor(x => x.Value).MinimumLength(data.MinLenght).WithMessage(ViolationMessage(rawNumberOfData, columnNumberOfData, "Length must be GREATER: " + data.MinLenght + " // Violation with: " + data.Value));
                RuleFor(x => x.Value).MaximumLength(data.MaxLenght).WithMessage(ViolationMessage(rawNumberOfData, columnNumberOfData, "Length must be LESS: " + data.MaxLenght + " // Violation with: " + data.Value));
            }
            if (data.MustSameWith != null)
            {
                RuleFor(x => x).Must(IsSameWith).WithMessage(ViolationMessage(rawNumberOfData, columnNumberOfData, "Mus same with: " + data.MustSameWith));
            }
            if (data.AllMustLower == true)
            {
                RuleFor(x => x.Value).Must(AllLower).WithMessage(ViolationMessage(rawNumberOfData, columnNumberOfData, "Must be LOWER CASE: " + data.Value));
            }
            if (data.AllMustUpper == true)
            {
                RuleFor(x => x.Value).Must(AllUpper).WithMessage(ViolationMessage(rawNumberOfData, columnNumberOfData, "Must be UPPER CASE: " + data.Value));
            }
        }

        private string ViolationMessage(int row, int column, string Message)
        {
            return "There is violation in ROW: " + row.ToString() + " & COLUMN: " + column.ToString() + " " + Message;
        }
        private bool AllowNumerics(string value)
        {
            foreach (char c in value)
            {
                if (c == '0' || c == '1' || c == '2' || c == '3' || c == '4' || c == '5' || c == '6' || c == '7' || c == '8' || c == '9')
                {
                    return false;
                }
            }
            return true;
        }
        private bool OnlyNumeric(string value)
        {
            return value.All(char.IsNumber);
        }
        private bool OnlyLetter(string value)
        {
            return value.All(char.IsLetter);
        }
        private bool IsUnique(Data data)
        {
            int counter = 0;
            foreach (Data tempData in datas)
            {
                if (tempData.CsvIndex % headers.Count == data.CsvIndex % headers.Count && data.Value.Equals(tempData.Value))
                {
                    counter++;
                }
                if (counter > 1)
                {
                    return false;
                }
            }
            return true;

        }
        private bool AllLower(string value)
        {
            return value.All(char.IsLower);
        }
        private bool AllUpper(string value)
        {
            return value.All(char.IsUpper);
        }
        private bool IsSameWith(Data data)
        {
            int rowIndex = (data.CsvIndex / headers.Count);
            int columnIndex = data.CsvIndex + 1 % headers.Count;
            int sameIndex = headerValues.IndexOf(data.MustSameWith); // Same Column Index

            string sameValue = datas.ElementAt(headers.Count * rowIndex + sameIndex % headers.Count).Value;

            return data.Value.Equals(sameValue);
        }

    }
}