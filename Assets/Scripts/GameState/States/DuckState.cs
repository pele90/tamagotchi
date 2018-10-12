using UnityEngine;

public class DuckState : IGameState
{
    private const string DUCK_VIEW = "duck_view";

    public void AButton(){}

    public void BButton(){}

    public void CButton()
    {
        ViewManager.Instance.DeactivateView(DUCK_VIEW);
    }

    public void Init()
    {
        ViewManager.Instance.ActivateView(DUCK_VIEW);
    }

    public void UpdateState()
    {
        Debug.Log("Updating Duck State...");
    }
}
