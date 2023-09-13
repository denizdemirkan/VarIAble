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
