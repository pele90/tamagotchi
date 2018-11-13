using System;
using UnityEngine;

public class TimerState : IGameState
{
    private const string TIMER_VIEW = "timer_view";
    private Timer timer;

    public void Init()
    {
        ViewManager.Instance.ActivateView(TIMER_VIEW);
        timer = ViewManager.Instance.GetView(TIMER_VIEW).GetComponent<Timer>();
    }

    public void Update()
    {

    }

    public void AButton()
    {
        if(!GameManager.Instance.GetPlayerData().GetData().IsInitialized)
            timer.IncreaseHours();
    }

    public void BButton()
    {
        if (!GameManager.Instance.GetPlayerData().GetData().IsInitialized)
            timer.IncreaseMinutes();
    }

    public void CButton()
    {
        if (GameManager.Instance.GetPlayerData().GetData().IsInitialized)
        {
            ViewManager.Instance.DeactivateView(TIMER_VIEW);
        }
        else
        {
            timer.SetTimer();
            GameManager.Instance.GetPlayerData().GetData().IsInitialized = true;
            ViewManager.Instance.DeactivateView(TIMER_VIEW);
        }
    }
}
