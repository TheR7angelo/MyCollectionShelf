using System.Collections.Generic;

namespace MyCollectionShelf.Wpf.Ui.Common.Comparator;

public static class DetailedCompare
{
    public static List<Variance> Compare<T>(this T val1, T val2)
    {
        var variances = new List<Variance>();
        var propertyInfos = val1!.GetType().GetProperties();
        foreach (var propertyInfo in propertyInfos)
        {
            var v = new Variance
            {
                Property = propertyInfo.Name,
                ValueA = propertyInfo.GetValue(val1),
                ValueB = propertyInfo.GetValue(val2)
            };
            if (!Equals(v.ValueA, v.ValueB)) variances.Add(v);

        }
        return variances;
    }
}

public class Variance
{
    public required string Property { get; set; }
    public object? ValueA { get; init; }
    public object? ValueB { get; init; }
}