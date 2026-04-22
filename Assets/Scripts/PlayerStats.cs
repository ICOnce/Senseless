using UnityEngine;

public static class PlayerStats
{
    public static float health;
    public static int skillPoints;
    public static int Sight;
    public static int Smell;
    public static int Hearing;

    public static void Learn(StatType stat, int statAmount)
    {
        switch (stat)
        {
            case StatType.Sight:
                Sight += statAmount;
                break;
            case StatType.Smell:
                Smell += statAmount;
                break;
            case StatType.Hearing:
                Hearing += statAmount;
                break;
        }
    }

    public static void UnLearn(StatType stat, int statAmount)
    {
        switch (stat)
        {
            case StatType.Sight:
                Sight -= statAmount;
                break;
            case StatType.Smell:
                Smell -= statAmount;
                break;
            case StatType.Hearing:
                Hearing -= statAmount;
                break;
        }
    }
}
