using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public UnityEngine.UI.Text Hours;
    public UnityEngine.UI.Text Minutes;
    public UnityEngine.UI.Text Seconds;

    private DateTime CurrentTime;
    private bool IsInitialized;

    // Use this for initialization
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GetPlayerData().GetData().IsInitialized)
        {
            CurrentTime = GameManager.Instance.CurrentGameTime;
        }

        Hours.text = CurrentTime.Hour.ToString("00");
        Minutes.text = CurrentTime.Minute.ToString("00");
        Seconds.text = CurrentTime.Second.ToString("00");
        
    }

    public void IncreaseHours()
    {
        CurrentTime = CurrentTime.AddHours(1);
    }

    public void IncreaseMinutes()
    {
        CurrentTime = CurrentTime.AddMinutes(1);
    }

    public void SetTimer()
    {
        GameManager.Instance.CurrentGameTime = CurrentTime;
        GameManager.Instance.GetPlayerData().GetData().birthDateTime = CurrentTime;
    }
}
