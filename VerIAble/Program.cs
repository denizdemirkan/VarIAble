using System.Threading.Channels;

string patternStart = "[/*";
string patternEnd = "*/]";

string givenRegex = "asd[/*firstname*/]1112[/*lastName*/]bcd";

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

        result = result.Replace(patternStart+searchedVal+patternEnd, searchedVal);
        Console.WriteLine(searchedVal);

    }
    return result;
}