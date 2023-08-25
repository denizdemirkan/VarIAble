using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VerIAble.UI.Classes;

namespace VerIAble.UI.HelperClasses
{
    public class CustomDataValidator : AbstractValidator<Data>
    {
        private List<Data> fields;
        private List<Data> datas;
        private List<string> fieldNames;
        public CustomDataValidator(Data data, List<Data> fields, List<Data> datas, List<string> fieldNames)
        {
            this.fields = fields;
            this.datas = datas;
            this.fieldNames = fieldNames;
            int rawNumberOfData = data.CsvIndex / fields.Count + 2;
            int columnNumberOfData = data.CsvIndex % fields.Count + 1;

            // if value is null & AllowNull is false, there is no need for other rules.
            if (!data.AllowNull)
            {
                RuleFor(x => x.Value).NotEmpty().WithMessage(ViolationMessage(rawNumberOfData, columnNumberOfData, "Can not be EMPTY!"));
                //if (!String.IsNullOrEmpty(data.Value))
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
            if (data.MustSameWith != "" && data.MustSameWith != null)
            {
                RuleFor(x => x).Must(IsSameWith).WithMessage(ViolationMessage(rawNumberOfData, columnNumberOfData, "Must be same with: " + data.MustSameWith));
            }
            if (data.AllMustLower == true)
            {
                RuleFor(x => x.Value).Must(AllLower).WithMessage(ViolationMessage(rawNumberOfData, columnNumberOfData, "Must be LOWER CASE: " + data.Value));
            }
            if (data.AllMustUpper == true)
            {
                RuleFor(x => x.Value).Must(AllUpper).WithMessage(ViolationMessage(rawNumberOfData, columnNumberOfData, "Must be UPPER CASE: " + data.Value));
            }
            if (!String.IsNullOrEmpty(data.MustStartsWith))
            {
                RuleFor(x => x).Must(IsStartsWith).WithMessage(ViolationMessage(rawNumberOfData, columnNumberOfData, "Must be START with: " + data.MustStartsWith));
            }
            if (!String.IsNullOrEmpty(data.MustEndsWith))
            {
                RuleFor(x => x).Must(IsEndsWith).WithMessage(ViolationMessage(rawNumberOfData, columnNumberOfData, "Must be END with: " + data.MustEndsWith));
            }
            if (!String.IsNullOrEmpty(data.MustContains))
            {
                RuleFor(x => x).Must(IsContains).WithMessage(ViolationMessage(rawNumberOfData, columnNumberOfData, "Must CONTAIN: " + data.MustContains));
            }
            if (!String.IsNullOrEmpty(data.Pattern))
            {
                RuleFor(x => x).Must(ValidRegex).WithMessage(ViolationMessage(rawNumberOfData, columnNumberOfData, "Invalid Regex in: " + data.Value));
            }
            if (!String.IsNullOrEmpty(data.AllowedValues))
            {
                RuleFor(x => x).Must(IsAllowedValue).WithMessage(ViolationMessage(rawNumberOfData, columnNumberOfData, "This value is Not Allowed!: " + data.Value));
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
                if (tempData.CsvIndex % fields.Count == data.CsvIndex % fields.Count && data.Value.Equals(tempData.Value))
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
            int rowIndex = (data.CsvIndex / fields.Count);
            int columnIndex = data.CsvIndex + 1 % fields.Count;
            int sameIndex = fieldNames.IndexOf(data.MustSameWith); // Same Column Index

            string sameValue = datas.ElementAt(fields.Count * rowIndex + sameIndex % fields.Count).Value;

            return data.Value.Equals(sameValue);
        }
        private bool IsStartsWith(Data data)
        {
            return data.Value.StartsWith(data.MustStartsWith);
        }
        private bool IsEndsWith(Data data)
        {
            return data.Value.EndsWith(data.MustEndsWith);
        }
        private bool IsContains(Data data)
        {
            return data.Value.Contains(data.MustContains);
        }
        private bool IsAllowedValue(Data data)
        {
            string[] allowedValues = data.AllowedValues.Split(',');

            foreach (string value in allowedValues)
            {
                if (value.Trim() == data.Value)
                {
                    return true;
                }
            }

            return false;
        }
        private bool ValidRegex(Data data)
        {
            Regex regex = new Regex(@"" + data.Pattern);

            Match match = regex.Match(data.Value);

            return match.Success;
        }
    }
}
