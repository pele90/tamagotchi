using System;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public GameObject PoopContainer;

    //private DateTime birthDateTime;

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
    private double poopSicknessIndexAmount;

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

    [Header("SLEEP")]
    [SerializeField]
    private bool isSleepy;

    [SerializeField]
    private bool isSleeping;

    [SerializeField]
    private float sleepingTime;

    [SerializeField]
    private float awakeTime;

    [SerializeField]
    private float timeToGetSleepy;

    [SerializeField]
    private float minSleepingTime;

    [SerializeField]
    private double insufficientSleepSickness;

    #endregion

    // Use this for initialization
    void Awake()
    {
        monsterData = GetComponent<MonsterData>();

        // TODO: set fields based on personality
    }

    // Update is called once per frame
    void Update()
    {
        if (isSleeping)
            sleepingTime += Time.deltaTime;
        else
        {
            awakeTime += Time.deltaTime;

            // Sets the isSleepy flag if the time is for sleep
            IsTimeForSleep();
        }


        if(PoopContainer.transform.childCount != 0)
        {
            timeWithPoop += Time.deltaTime;

            if(timeWithPoop >= addToSicknessIndexInterval)
            {
                GameManager.Instance.monsterController.AddSicknessIndex(poopSicknessIndexAmount);
                timeWithPoop = 0;
            }
        }
        else
            timeWithPoop = 0;

        // Check health status
        if (IsSick())
        {
            //EventManager.TriggerEvent(Events.MonsterSick);
            GameManager.Instance.SicknessText.gameObject.SetActive(true);

            if (IsDead())
            {
                Debug.Log("The monster died. Rest in piece.");
            }
        }
    }
    
    public void SetBirthDateTime(DateTime datetime)
    {
        //birthDateTime = datetime;
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

    public bool IsPoopContainerEmpty()
    {
        return PoopContainer.transform.childCount == 0;
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

    public bool IsSleepy()
    {
        return isSleepy;
    }

    public bool PutToSleep()
    {
        if (!isSleeping && isSleepy)
        {
            isSleeping = true;
            isSleepy = false;
            awakeTime = 0;
            return true;
        }

        return false;
    }

    public void WakeUp()
    {
        if (isSleeping)
        {
            isSleeping = false;

            // TODO: calculate amount of sleep and if under some value add to sickness index
            if(sleepingTime <= minSleepingTime)
            {
                AddSicknessIndex(insufficientSleepSickness);
            }

            sleepingTime = 0;
        }
    }

    public bool IsSick()
    {
        if(sicknessIndex >= sicknessThreshold)
            return true;
        
        return false;
    }

    public bool IsDead()
    {
        if (sicknessIndex >= sicknessDeathTreshold)
            return true;

        return false;
    }
    
    public void IsTimeForSleep()
    {
        //var birthTime = birthDateTime.ToShortTimeString();
        //var currentTime = GameManager.Instance.CurrentGameTime.ToShortTimeString();

        //TODO: kada se monsteru zapravo spava??

        if (awakeTime >= timeToGetSleepy)
            isSleepy = true;


    }
}
