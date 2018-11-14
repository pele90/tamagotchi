using UnityEngine;
using UnityEngine.UI;

public class Util
{
    private Util(){}

    public static Util Instance
    {
        get { return Nested.instance;}
    }

    public static int GetRandomNumberBeetween(int min, int max)
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        return Random.Range(min, max+1); // +1 because max is exclusive
    }

    public static void HideObject(Image image)
    {
        Color hiddenColor = image.color;
        hiddenColor.a = 0;
        image.color = hiddenColor;
    }

    public static void ShowObject(Image image)
    {
        Color shownColor = image.color;
        shownColor.a = 1;
        image.color = shownColor;
    }

    private class Nested
    {
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Nested(){}

        internal static readonly Util instance = new Util();
    }
}
