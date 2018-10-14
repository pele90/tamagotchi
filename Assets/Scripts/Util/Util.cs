using UnityEngine;

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
        return Random.Range(min, max);
    }

    private class Nested
    {
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Nested(){}

        internal static readonly Util instance = new Util();
    }
}
