using System.Collections.Generic;

public static class CustomExtensions
{
    public static bool CheckIfNull(this object obj)
    {
        return obj == null;
    }

    /// <summary>
    /// Convierto el array en una lista equivalente;
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <returns></returns>
    public static List<T> ArrayToList<T>(this T[] array)
    {
        var list = new List<T>();

        if (array == null) return list;

        foreach (var item in array) { list.Add(item); }

        return list;
    }
}
