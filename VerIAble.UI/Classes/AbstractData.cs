using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerIAble.UI.Classes
{
    public class AbstractData
    {
        public string Value { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
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
        public string MustContains { get; set; }
        public string AllowedValues { get; set; }
        public string MustSameWith { get; set; }

        public string Pattern { get; set; }
    }
}
