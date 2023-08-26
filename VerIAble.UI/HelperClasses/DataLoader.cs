using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VerIAble.UI.Classes;

namespace VerIAble.UI.HelperClasses
{
    public class DataLoader
    {
        static char FindCsvSeparator(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string firstLine = reader.ReadLine();
                if (firstLine != null)
                {
                    int commaCount = CountCharOccurrences(firstLine, ',');
                    int semicolonCount = CountCharOccurrences(firstLine, ';');
                    int tabCount = CountCharOccurrences(firstLine, '\t');
                   
                    // highest will be consider as seperator. 
                    if (commaCount >= semicolonCount && commaCount >= tabCount)
                    {
                        return ',';
                    }
                    else if (semicolonCount >= commaCount && semicolonCount >= tabCount)
                    {
                        return ';';
                    }
                    else
                    {
                        return '\t';
                    }
                }
            }

            throw new Exception("Couldn't read file.");
        }

        static int CountCharOccurrences(string text, char target)
        {
            int count = 0;
            foreach (char c in text)
            {
                if (c == target)
                {
                    count++;
                }
            }
            return count;
        }

        public static void LoadFromCSV(List<Data> Fields, List<Data> CellDatas)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV file (*.csv)|*.csv";
                openFileDialog.Title = "Select CSV File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                    {
                        int lineCount = 1;
                        int indexCount = 0;
                        while (!reader.EndOfStream)
                        {

                            string line = reader.ReadLine();
                            string[] cells = line.Split(FindCsvSeparator(openFileDialog.FileName));

                            foreach (string cell in cells)
                            {
                                if (lineCount == 1)
                                {
                                    Data newField = new Data();
                                    newField.Value = cell;
                                    Fields.Add(newField);
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
                }
            }
        }
    }
}
