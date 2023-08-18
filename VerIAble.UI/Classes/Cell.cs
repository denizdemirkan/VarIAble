using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerIAble.UI.Classes
{
    public class Cell
    {
        public string Value { get; set; }
        [Browsable(false)]
        public string Type { get; set; }
        public bool MustBeUnique { get; set; }
        public bool AllowNull { get; set; }
        public int MaxLenght { get; set; }
        public int MinLenght { get; set; }
        public int TotalLenght { get; set; }

        public bool OnlyLetters { get; set; }
        public bool OnlyNumerics { get; set; }

        public bool AllowNumerics { get; set; }
        public bool AllowSpecialCharacters { get; set; }
        public bool AllowSpace { get; set; }

        public bool MustBeInteger { get; set; }
        public bool MustBeDecimal { get; set; }

        public bool AllMustUpper { get; set; }
        public bool AllMustLower { get; set; }

        public string MustStartsWith { get; set; }
        public string MustEndsWith { get; set; }
        public string AllowedValues { get; set; }

        [Browsable(false)]
        public string MustSameWith { get; set; }

        public string Pattern { get; set; }
        public Cell()
        {
            this.Type = "String";
            this.MaxLenght = 50;
            this.MinLenght = 1;
            this.TotalLenght = 0;

            this.AllMustLower = false;
            this.AllMustUpper = false;

            this.MustBeInteger = false;
            this.MustBeDecimal = false;
            this.MustBeUnique = false;

            this.OnlyLetters = false;
            this.OnlyNumerics = false;

            this.AllowNumerics = true;
            this.AllowSpecialCharacters = true;
            this.AllowSpace = false;
            this.AllowNull = true;

        }
    }
}