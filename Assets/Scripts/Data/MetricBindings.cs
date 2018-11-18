using UnityEngine;
using UnityEngine.UI;

public class MetricBindings : MonoBehaviour
{
    public Text AgeText;
    public Text WeightText;

    private MonsterData monsterData;
    private Transform hungerHearts;
    private int lastNumOfHungerHearts;
    private Transform happinessHearts;
    private int lastNumOfHappinessHearts;
    private Slider disciplineGauge;

    // Use this for initialization
    void Start()
    {
        monsterData = GameObject.FindGameObjectWithTag("Player").GetComponent<MonsterData>();
        AgeText.text = monsterData.age.ToString();
        WeightText.text = monsterData.weight.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(hungerHearts == null)
        {
            var tempGameObject = GameObject.FindGameObjectWithTag("HungerHearts");
            if(tempGameObject)
            {
                hungerHearts = tempGameObject.transform;
            }
            
        }

        if (happinessHearts == null)
        {
            var tempGameObject = GameObject.FindGameObjectWithTag("HappinessHearts");
            if (tempGameObject)
            {
                happinessHearts = tempGameObject.transform;
            }

        }

        if (disciplineGauge == null)
        {
            var tempGameObject = GameObject.FindGameObjectWithTag("DisciplineGauge");
            if (tempGameObject)
            {
                disciplineGauge = tempGameObject.GetComponent<Slider>();
            }

        }

        AgeText.text = monsterData.age.ToString();
        WeightText.text = monsterData.weight.ToString();

        if(hungerHearts != null)
            HandleHearts(hungerHearts, monsterData.hunger);

        if(happinessHearts != null)
            HandleHearts(happinessHearts, monsterData.happiness);

        if(disciplineGauge != null)
            disciplineGauge.value = monsterData.discipline;

    }

    void HandleHearts(Transform parent, int value)
    {
        // First hide all hearts
        HideAllHearts(parent);

        // Get the last heart index
        int childCount = parent.childCount - 1;

        // Iterate through transform child's (heart gameObjects) and set the image component to visible
        for (int i = 0; i < value; i++)
        {
            // Get Image component of child
            var childImage = parent.GetChild(childCount).GetComponent<Image>();
            if (childImage.color.a != 1)
            {
                // Set alpha to 1
                Util.ShowObject(childImage);
            }

            // Go to the next heart from the right hand side to left
            childCount--;
        }
    }

    void HideAllHearts(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            var childImage = parent.GetChild(i).GetComponent<Image>();
            if (childImage.color.a != 0)
            {
                Util.HideObject(childImage);
            }
        }
    }
}
