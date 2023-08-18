using FluentValidation;
using FluentValidation.Results;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using VerIAble.UI.Classes;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static VerIAble.UI.Program;

namespace VerIAble.UI
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        private static extern bool FreeConsole();

        List<string> Errors = new List<string>();

        List<Cell> Headers = new List<Cell>();

        List<Data> CellDatas = new List<Data>();

        List<string> Types = new List<string>();

        List<string> headerCellValues = new List<string>();

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
                            Cell newHeader = new Cell();
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
            // Type Column for Default Settings
            dataGridView1.DataSource = Headers;
            DataGridViewComboBoxColumn typeColumn = new DataGridViewComboBoxColumn();
            typeColumn.HeaderText = "Type";
            typeColumn.DataSource = Types;
            dataGridView1.Columns[1].DataPropertyName = "Type";
            dataGridView1.Columns.Insert(1, typeColumn);

            // SameWith Column
            foreach(Cell header in Headers)
            {
                headerCellValues.Add(header.Value);
            }
            DataGridViewComboBoxColumn sameWithColumn = new DataGridViewComboBoxColumn();
            sameWithColumn.HeaderText = "SameWith";
            sameWithColumn.DataSource = headerCellValues;
            dataGridView1.Columns[2].DataPropertyName = "MustSameWith";
            dataGridView1.Columns.Insert(2, sameWithColumn);

        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // On Type Change
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                DataGridViewComboBoxCell comboBoxCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewComboBoxCell;
                if (comboBoxCell != null)
                {
                    int rowNumber = e.RowIndex;

                    string selectedValue = comboBoxCell.Value.ToString();

                    Headers.ElementAt(rowNumber).Type = selectedValue;

                    TypeDefaults(Headers.ElementAt(rowNumber));

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
                //Console.WriteLine(Headers.ElementAt(data.CsvIndex % Headers.Count).Value);
                //Console.WriteLine(Headers.ElementAt(data.CsvIndex % Headers.Count).MustBeUnique);
                //Console.WriteLine(Headers.ElementAt(data.CsvIndex % Headers.Count).OnlyLetters);
                //Console.WriteLine(Headers.ElementAt(data.CsvIndex % Headers.Count).MaxLenght);
                //Console.WriteLine(data.Value);
            }


            foreach (Data data in CellDatas)
            {
                DataValidator validator = new DataValidator(data, this.Headers, this.CellDatas);
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

        private void ApplyRules(Cell header, Data data)
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

            data.Type = header.Type;
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(Cell c in Headers)
            {
               Console.WriteLine(c.MustBeUnique);
            }
        }

        private void TypeDefaults(Cell cell)
        {
            if (cell.Type.Equals("Integer"))
            {
                cell.OnlyNumerics = true;
                cell.OnlyLetters = false;

                cell.AllMustLower = false;
                cell.AllMustUpper = false;

                cell.AllowNumerics = true;
                cell.AllowSpace = false;
                cell.AllowSpecialCharacters = false;

            }
            if (cell.Type.Equals("String"))
            {
                cell.OnlyNumerics = false;
                cell.OnlyLetters = false;

                cell.AllMustLower = false;
                cell.AllMustUpper = false;

                cell.AllowNumerics = true;
                cell.AllowSpace = true;
                cell.AllowSpecialCharacters = true;
            }
            if (cell.Type.Equals("Email"))
            {
                cell.OnlyNumerics = false;
                cell.OnlyLetters = false;

                cell.AllMustLower = false;
                cell.AllMustUpper = false;

                cell.MustBeUnique = true;
                cell.AllowNumerics = true;
                cell.AllowSpace = false;
                cell.AllowSpecialCharacters = true;
            }
            if (cell.Type.Equals("UID"))
            {
                cell.OnlyNumerics = true;
                cell.OnlyLetters = false;

                cell.AllMustLower = false;
                cell.AllMustUpper = false;

                cell.AllowNumerics = true;
                cell.AllowSpace = false;
                cell.AllowSpecialCharacters = false;

                cell.MustBeUnique = true;
                cell.AllowNull = false;
                cell.MustBeInteger = true;
            }
            if (cell.Type.Equals("Phone Number"))
            {
                cell.TotalLenght = 10;

                cell.OnlyNumerics = true;
                cell.OnlyLetters = false;

                cell.AllMustLower = false;
                cell.AllMustUpper = false;

                cell.AllowNumerics = true;
                cell.AllowSpace = false;
                cell.AllowSpecialCharacters = false;

                cell.MustBeUnique = true;
                cell.AllowNull = false;
                cell.MustBeInteger = true;
            }
            if (cell.Type.Equals("Name"))
            {
                cell.OnlyNumerics = false;
                cell.OnlyLetters = true;

                cell.AllMustLower = false;
                cell.AllMustUpper = false;

                cell.AllowNumerics = false;
                cell.AllowSpace = true;
                cell.AllowSpecialCharacters = false;

                cell.MustBeUnique = false;
                cell.AllowNull = false;
                cell.MustBeInteger = false;
            }

        }
    }

    public class DataValidator : AbstractValidator<Data>
    {
        private List<Cell> headers;
        private List<Data> datas;
        public DataValidator(Data data, List<Cell> headers, List<Data> datas)
        {
            this.headers = headers;
            this.datas = datas;
            int rawNumberOfData = data.CsvIndex / headers.Count + 2;
            int columnNumberOfData = data.CsvIndex % headers.Count + 1;

            if (data.Type.Equals("Email")) // Develop This for default settings
            {
                RuleFor(x => x.Value).EmailAddress().WithMessage(ViolationMessage(rawNumberOfData, columnNumberOfData, "Invalid EMAIL Format!"));
            }
            if (!data.AllowNull)
            {
                RuleFor(x => x.Value).NotEmpty().WithMessage(ViolationMessage(rawNumberOfData, columnNumberOfData, "Can not be EMPTY!"));
            }
            if (!data.AllowNumerics)
            {
                RuleFor(x => x.Value).Must(AllowNumerics).WithMessage(ViolationMessage(rawNumberOfData, columnNumberOfData, "Numerics not ALLOWED!"));
            }
            if (data.OnlyNumerics)
            {
                RuleFor(x => x.Value).Must(OnlyNumeric).WithMessage(ViolationMessage(rawNumberOfData, columnNumberOfData, "ONLY Numerics ALLOWED!"));
            }
            if (data.OnlyLetters)
            {
                RuleFor(x => x.Value).Must(OnlyLetter).WithMessage(ViolationMessage(rawNumberOfData, columnNumberOfData, "ONLY Letters ALLOWED!"));
            }
            if (data.MustBeUnique)
            {
               RuleFor(x => x).Must(IsUnique).WithMessage(ViolationMessage(rawNumberOfData, columnNumberOfData, "Must be Unique"));
            }
            if (data.TotalLenght != 0)
            {
                RuleFor(x => x.Value).Length(data.TotalLenght).WithMessage(ViolationMessage(rawNumberOfData, columnNumberOfData, "Length must be EXACTLY: " + data.TotalLenght));
            }
            if (data.TotalLenght == 0)
            {
                RuleFor(x => x.Value).MinimumLength(data.MinLenght).WithMessage(ViolationMessage(rawNumberOfData, columnNumberOfData, "Length must be GREATER: " + data.MinLenght));
                RuleFor(x => x.Value).MaximumLength(data.MaxLenght).WithMessage(ViolationMessage(rawNumberOfData, columnNumberOfData, "Length must be LESS: " + data.MaxLenght));
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
                if(counter > 1)
                {
                    return false;
                }
            }
            return true;

        }

    }
}