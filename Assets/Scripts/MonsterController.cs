using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public GameObject PoopContainer;

    [HideInInspector]
    public MonsterData monsterData;
    //private Personality personality;


    #region PRIVATE PROPERTIES

    [Header("POOP")]
    [SerializeField]
    private float poopInterval;

    [SerializeField]
    private int addToSicknessIndexInterval;

    [SerializeField]
    private float timeWithPoop;

    [SerializeField]
    private int poopQueueAmount;

    [Header("SICKNESS")]
    [SerializeField]
    private double sicknessThreshold;

    [SerializeField]
    private double sicknessDeathTreshold;

    [SerializeField]
    private double sicknessIndex;

    #endregion

    // Use this for initialization
    void Start()
    {
        monsterData = GetComponent<MonsterData>();
        sicknessThreshold = 1.0;
        sicknessDeathTreshold = 2.0;

        // set fields based on personality
    }

    // Update is called once per frame
    void Update()
    {
        if(PoopContainer.transform.childCount != 0)
        {
            timeWithPoop += Time.deltaTime;

            if(timeWithPoop >= addToSicknessIndexInterval)
            {
                GameManager.Instance.monsterController.AddSicknessIndex(0.1);
                timeWithPoop = 0;
            }
        }
        else
            timeWithPoop = 0;

        // Check health status
        if (CheckIsSick())
        {
            //EventManager.TriggerEvent(Events.MonsterSick);
            GameManager.Instance.SicknessText.gameObject.SetActive(true);

            if (CheckIsDead())
            {
                Debug.Log("The monster died. Rest in piece.");
            }
        }
    }
    
    public float GetPoopInterval()
    {
        return poopInterval;
    }

    public int GetPoopQueueAmount()
    {
        return poopQueueAmount;
    }

    public void AddToPoopQueue()
    {
        poopQueueAmount++;
    }

    public void ReducePoopQueue()
    {
        if (poopQueueAmount != 0)
            --poopQueueAmount;
    }

    public void AddSicknessIndex(double value)
    {
        sicknessIndex += value;
    }

    public void ReduceSicknessIndex(double value)
    {
        sicknessIndex -= value;

        if (sicknessIndex < 0 )
            sicknessIndex = 0;

        if(sicknessIndex < 1)
            GameManager.Instance.SicknessText.gameObject.SetActive(false);
    }

    bool CheckIsSick()
    {
        if(sicknessIndex >= sicknessThreshold)
            return true;
        
        return false;
    }

    bool CheckIsDead()
    {
        if (sicknessIndex >= sicknessDeathTreshold)
            return true;

        return false;
    }
}
