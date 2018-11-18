using UnityEngine;

public class MonsterData : MonoBehaviour
{
    #region FIELDS and PROPERTIES

    public readonly int MAX_HUNGER = 4;
    public readonly int MAX_HAPPINESS = 4;

    public int age;
    public double weight;
    public float discipline; // can be between 0 and 1
    public int hunger;
    public int happiness;

    public MonsterData()
    {
        age = 0;
        weight = 0.0;
        discipline = 0.0f;
        hunger = 0;
        happiness = 0;
    }

    public MonsterData(int age, double weight, float discipline, int hunger, int happiness)
    {
        this.age = age;
        this.weight = weight;
        this.discipline = discipline;
        this.hunger = hunger;
        this.happiness = happiness;
    }

    #endregion

    #region PUBLIC METHODS

    public void AddAge(int age)
    {
        age += age;
    }

    public void ResetAge()
    {
        age = 0;
    }

    public void AddWeight(double value)
    {
        Debug.Log("Weight added!");
        weight += value;
    }

    public void ReduceWeight(double weight)
    {
        Debug.Log("Weight reduced!");
        weight -= weight;
        if (weight < 0.0)
            weight = 0.0;
    }

    public void AddDiscipline(float value)
    {
        Debug.Log("Discipline added!");
        discipline += value;
        if (discipline > 1)
            discipline = 1;
    }

    public void ReduceDiscipline(float value)
    {
        Debug.Log("Discipline reduced!");
        discipline -= value;
        if (discipline < 0.0f)
            discipline = 0.0f;
    }

    public void AddHunger()
    {
        Debug.Log("Hunger added!");
        if (++hunger > 4)
            hunger = 4;
    }

    public void ReduceHunger()
    {
        Debug.Log("Hunger reduced!");
        hunger -= 1;
        if (hunger < 0)
            hunger = 0;
    }

    public void AddHappiness()
    {
        Debug.Log("Happines added!");
        if (++happiness > 4)
            happiness = 4;
    }

    public void ReduceHappiness()
    {
        Debug.Log("Happines reduced!");
        happiness -= 1;
        if (happiness < 0)
            happiness = 0;
    }

    #endregion
}
