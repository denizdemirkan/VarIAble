using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerIAble.UI.Classes
{
    public class Data : AbstractData
    {
        [Browsable(false)]
        public CustomType Type { get; set; }
        [Browsable(false)]
        public string Description { get; set; }
        [Browsable(false)]
        public bool MustBeUnique { get; set; }
        [Browsable(false)]
        public bool AllowNull { get; set; }
        [Browsable(false)]
        public int MaxLenght { get; set; }
        [Browsable(false)]
        public int MinLenght { get; set; }

        [Browsable(false)]
        public int TotalLenght { get; set; }

        [Browsable(false)]
        public bool OnlyLetters { get; set; }
        [Browsable(false)]
        public bool OnlyNumerics { get; set; }

        [Browsable(false)]
        public bool AllowNumerics { get; set; }
        [Browsable(false)]
        public bool AllowSpecialCharacters { get; set; }
        [Browsable(false)]
        public bool AllowSpace { get; set; }

        [Browsable(false)]
        public bool MustBeInteger { get; set; }
        [Browsable(false)]
        public bool MustBeDecimal { get; set; }

        [Browsable(false)]
        public bool AllMustUpper { get; set; }
        [Browsable(false)]
        public bool AllMustLower { get; set; }

        [Browsable(false)]
        public string MustStartsWith { get; set; }
        [Browsable(false)]
        public string MustEndsWith { get; set; }
        [Browsable(false)]
        public string MustContains { get; set; }
        [Browsable(false)]
        public string AllowedValues { get; set; }

        [Browsable(false)]
        public string MustSameWith { get; set; }

        [Browsable(false)]
        public string Pattern { get; set; }

        [Browsable(false)]
        public int CsvIndex { get; set; }

        public Data()
        {
            // Default Settings
            this.MaxLenght = 100;
            this.MinLenght = 0;
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
