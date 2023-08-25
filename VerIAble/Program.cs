using System;
using System.Reflection;

public class SourceClass
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Value { get; set; }
}

public class TargetClass
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Value { get; set; }
    public DateTime Date { get; set; }
}

class Program
{
    static void CopyMatchingProperties(object source, object target)
    {
        Type sourceType = source.GetType();
        Type targetType = target.GetType();

        PropertyInfo[] sourceProperties = sourceType.GetProperties();
        PropertyInfo[] targetProperties = targetType.GetProperties();

        foreach (PropertyInfo sourceProperty in sourceProperties)
        {
            PropertyInfo targetProperty = Array.Find(targetProperties, prop => prop.Name == sourceProperty.Name && prop.PropertyType == sourceProperty.PropertyType);

            if (targetProperty != null)
            {
                object valueToCopy = sourceProperty.GetValue(source);
                targetProperty.SetValue(target, valueToCopy);
            }
        }
    }

    static void Main(string[] args)
    {
        SourceClass source = new SourceClass
        {
            Id = 1,
            Name = "Source Object",
            Value = 42.0
        };

        TargetClass target = new TargetClass
        {
            Id = 10,
            Name = "Target Object",
            Value = 99.0,
            Date = DateTime.Now
        };

        Console.WriteLine("Before Copy:");
        Console.WriteLine($"Target Id: {target.Id}, Name: {target.Name}, Value: {target.Value}, Date: {target.Date}");

        CopyMatchingProperties(source, target);

        Console.WriteLine("\nAfter Copy:");
        Console.WriteLine($"Target Id: {target.Id}, Name: {target.Name}, Value: {target.Value}, Date: {target.Date}");
    }
}
