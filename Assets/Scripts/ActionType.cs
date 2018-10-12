using System.Collections.Generic;

public class ActionType
{
    public enum ActionOption
    {
        None,
        Timer,
        Feed,
        Light,
        Play,
        Medicine,
        Duck,
        HealthMeter,
        Discipline,
        Attention
    }

    public enum FeedOption
    {
        Meal,
        Snack
    }

    public static Dictionary<int, ActionOption> ActionOptions = new Dictionary<int, ActionOption>()
    {
        { 0, ActionOption.Feed },
        { 1, ActionOption.Light },
        { 2, ActionOption.Play },
        { 3, ActionOption.Medicine },
        { 4, ActionOption.Duck },
        { 5, ActionOption.HealthMeter },
        { 6, ActionOption.Discipline },
        { 7, ActionOption.Attention },
    };
}
