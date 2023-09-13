using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerIAble.UI.Classes;

namespace VerIAble.UI.HelperClasses
{
    public class ViolationCalculator
    {

        public static void SeeViolationsOnConlose(List<Data> CellDatas, List<Data> Fields, List<string> fieldNames, List<ValidationResult> results)
        {
            foreach (Data data in CellDatas)
            {
                CustomDataValidator validator = new CustomDataValidator(data, Fields, CellDatas, fieldNames);
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

        public static void CreateRaportAsPDF()
        {

        }

        public static void CalculateStatistics(List<ValidationResult> results)
        {

        }
    }
}
