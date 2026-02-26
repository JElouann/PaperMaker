using System.Globalization;
using UnityEngine;

public class ParsingUtility : MonoBehaviour
{
    public static Vector3 Vector3FromString(string s)
    {
        string v;
        Vector3 pos;

        v = s.Substring(1, s.Length - 2);
        string[] splitValue = v.Split(',');

        pos.x = float.Parse(splitValue[0], new CultureInfo("en-US"));
        pos.y = float.Parse(splitValue[1], new CultureInfo("en-US"));
        pos.z = float.Parse(splitValue[2], new CultureInfo("en-US"));

        return pos;
    }

    public static Color ColorFromString(string s)
    {
        string v;
        Color color;

        v = s.Substring(1, s.Length - 2);
        string[] splitValue = s.Split(',');

        color.r = float.Parse(splitValue[0].Replace("RGBA(", ""), new CultureInfo("en-US"));
        color.g = float.Parse(splitValue[1], new CultureInfo("en-US"));
        color.b = float.Parse(splitValue[2], new CultureInfo("en-US"));
        color.a = float.Parse(splitValue[3].Replace(")", ""), new CultureInfo("en-US"));

        return color;
    }
}
