using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class Tools
{
    public static T StringToEnum<T>(string strName)
    {
        return (T)System.Enum.Parse(typeof(T), strName);
    }
    public static Vector2 StringToVector2(string strVector2)
    {
        string[] str = strVector2.TrimEnd(')').TrimStart('(').Split(',');
        float x = (float)System.Convert.ToDouble(str[0]);
        float y = (float)System.Convert.ToDouble(str[1]);
        return new Vector2(x, y);
    }
}
