using UnityEngine;

public class AttentionState : IGameState
{
    private const string ATTENTION_VIEW = "attention_view";

    public void AButton() { }

    public void BButton() { }

    public void CButton()
    {
        ViewManager.Instance.DeactivateView(ATTENTION_VIEW);
    }

    public void Init()
    {
        ViewManager.Instance.ActivateView(ATTENTION_VIEW);
    }

    public void UpdateState()
    {
        Debug.Log("Updating Attention State...");
    }
}
