using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerIAble.UI.Classes
{
    public class CustomType : AbstractData
    {

        // Add this Annotation to Hide Fields in Data Grid Views

        [Browsable(false)]
        public string Value { get; set; }

        [Browsable(false)]
        public string MustSameWith { get; set; }

        public CustomType()
        {
            
        }
        public CustomType(string type)
        {
            // These are defaults
            this.Type = type;
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

        // Rules on Fields Change  && Bug -> priotizing of the called function matters.

        //public void SaveOnChange()
        //{
        //    ChangeOnOnlyLetters();
        //    ChangeOnOnlyNumerics();
        //}
        public void ChangeOnOnlyNumerics()
        {
            if (this.OnlyNumerics)
            {
                AllowNumerics = true;
                OnlyLetters = false;
                AllowSpace = false;
                AllowSpecialCharacters = false;
            }
        }
        public void ChangeOnOnlyLetters()
        {
            if (this.OnlyLetters)
            {
                AllowNumerics = false;
                OnlyNumerics = false;
                AllowSpace = false;
                AllowSpecialCharacters = false;
            }
        }
    }
}
