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
        float v = Time.deltaTime * 1000.0f;
        Random.InitState((int)v);
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
