using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VerIAble.UI.Classes;

namespace VerIAble.UI.HelperClasses
{
    public static class FieldCopyHelper
    {
        // There's a need of CopyField function to cover all above (Data -> Data // CustomType -> Data).
        // Since I use [Browsable(false)] in the classes I couldn't found a proper way to do it..

        public static void ApplyRulesToData(Data field, Data data)
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

            data.CodexThreshold = field.CodexThreshold;
            data.Codex = field.Codex;

            data.Type = field.Type;
        }

        public static void ApplyRulesToField(Data data, CustomType customType)
        {
            data.Type = customType;

            // actually no need for this check. But once application runs with async functions, we may need this.
            if (data.Type == customType)
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

                data.CodexThreshold = data.Type.CodexThreshold;
                data.Codex = data.Type.Codex;

                data.Pattern = data.Type.Pattern;
            }
        }

    }
}
