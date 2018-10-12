using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMeterState : IGameState
{
    private const string HEALTHMETER_VIEW = "healthmeter_view";

    public void AButton() { }

    public void BButton() { }

    public void CButton()
    {
        ViewManager.Instance.DeactivateView(HEALTHMETER_VIEW);
    }

    public void Init()
    {
        ViewManager.Instance.ActivateView(HEALTHMETER_VIEW);
    }

    public void UpdateState()
    {
        Debug.Log("Updating HealthMeter State...");
    }
}
