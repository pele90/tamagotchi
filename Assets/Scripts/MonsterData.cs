using UnityEngine;

public class MonsterData : MonoBehaviour
{
    #region FIELDS and PROPERTIES

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
        weight += value;
    }

    public void ReduceWeight(double weight)
    {
        weight -= weight;
        if (weight < 0.0)
            weight = 0.0;
    }

    public void AddDiscipline(float value)
    {
        discipline += value;
        if (discipline > 1)
            discipline = 1;
    }

    public void ReduceDiscipline(float value)
    {
        discipline -= value;
        if (discipline < 0.0f)
            discipline = 0.0f;
    }

    public void AddHunger()
    {
        if (++hunger > 4)
            hunger = 4;
    }

    public void ReduceHunger()
    {
        hunger -= 1;
        if (hunger < 0)
            hunger = 0;
    }

    public void AddHappiness()
    {
        if (++happiness > 4)
            happiness = 4;
    }

    public void ReduceHappiness()
    {
        happiness -= 1;
        if (happiness < 0)
            happiness = 0;
    }

    #endregion
}
