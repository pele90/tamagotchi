using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisciplineState : IGameState
{
    private const string DISCIPLINE_VIEW = "discipline_view";

    public void AButton() { }

    public void BButton() { }

    public void CButton()
    {
        ViewManager.Instance.DeactivateView(DISCIPLINE_VIEW);
    }

    public void Init()
    {
        ViewManager.Instance.ActivateView(DISCIPLINE_VIEW);
    }

    public void Update()
    {
        Debug.Log("Updating Discipline State...");
    }
}
