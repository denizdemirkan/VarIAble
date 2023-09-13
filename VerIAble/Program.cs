using System.Threading.Channels;

string patternStart = "[/*";
string patternEnd = "*/]";

string patternParserStart = "{";
string patternParserEnd = "}";

string givenRegex = "asd[/*{f3}firstname*/]1112[/*{l6}lastName*/]bcd";

string value = "deniz";

int start = givenRegex.IndexOf(patternStart)+ patternStart.Length;
int end = givenRegex.IndexOf(patternEnd);
//Console.WriteLine(givenRegex.Substring(start,end-start));
Console.WriteLine(Changer(givenRegex));
// bul [/*firstname*/] patterninin tamamını deniz olarak değiştir oluşan yeni stringte tekrar arama yap.
// eğer başka patternler varsa bitene kadar devam et.



string Changer(string statement) 
{
    bool flag = true;
    string result = statement.ToString();

    while (flag)
    {
        if(result.IndexOf(patternStart) == -1)
        {
            flag = false;
            return result;
        }


        int start = result.IndexOf(patternStart) + patternStart.Length;
        int end = result.IndexOf(patternEnd);
        string searchedVal = result.Substring(start, end - start);
        string parsedSearchedVal = "";

        if (searchedVal.IndexOf(patternParserStart) != -1 && searchedVal.IndexOf(patternParserEnd) != -1)
        {
            int startParser = searchedVal.IndexOf(patternParserStart) + 2;
            int endParser = searchedVal.IndexOf(patternParserEnd);

            int intValueOfParserIndex = Convert.ToInt32(searchedVal.Substring(startParser, endParser - startParser));

            if (searchedVal.ElementAt(startParser-1).Equals('f'))
            {
                parsedSearchedVal = searchedVal.Substring(endParser + 1, intValueOfParserIndex);
            }
            else
            {
                parsedSearchedVal = searchedVal.Substring(searchedVal.Length-intValueOfParserIndex);
            }
        }
        if (!String.IsNullOrEmpty(parsedSearchedVal))
        {
            result = result.Replace(patternStart + searchedVal + patternEnd, parsedSearchedVal);
        }
        else
        {
            result = result.Replace(patternStart + searchedVal + patternEnd, searchedVal);
        }
    }
    return result;
}