using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerIAble.UI.Classes;

namespace VerIAble.UI.HelperClasses
{
    public class StringFieldFilter
    {
        static string patternStart = "[/*";
        static string patternEnd = "*/]";

        // [/*...*/] --> will return & format string with the value of ... field
        // ie. [/*FirstName*/].[/*LaseName*/].abc will return like "deniz.demirkan.abc"
        public static string NonRegexFillWithFields(string expression, List<Data> Fields, List<Data> CellDatas, Data Data)
        {
            bool flag = true;
            string result = expression.ToString();
            int dataRow = (Data.CsvIndex / Fields.Count) + 1;
            
            while (flag)
            {
                if (result.IndexOf(patternStart) == -1 || result.IndexOf(patternEnd) == -1)
                {
                    flag = false;
                    return result;
                }

                int searchedFieldColumn = -1;
                int start = result.IndexOf(patternStart) + patternStart.Length;
                int end = result.IndexOf(patternEnd);
                string searchedVal = result.Substring(start, end - start);

                foreach (Data field in Fields)
                {
                    if (field.Value.Equals(searchedVal)) // FirstName , FirstName
                    {
                        searchedFieldColumn = Fields.IndexOf(field);
                        continue;
                    }
                }
                if(searchedFieldColumn == -1)
                {
                    break;
                }

                int searchedIndex = (Fields.Count * dataRow) - 1 - (Fields.Count - searchedFieldColumn - 1);
                
                result = result.Replace(patternStart + searchedVal + patternEnd, CellDatas.ElementAt(searchedIndex).Value);
            }
            return result;
        }

        static string RegexFillWithFields()
        {
            return "";
        }
    }
}
