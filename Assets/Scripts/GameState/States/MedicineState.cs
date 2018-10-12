using UnityEngine;

public class MedicineState : IGameState
{
    private const string MEDICINE_VIEW = "medicine_view";

    public void AButton() { }

    public void BButton() { }

    public void CButton()
    {
        ViewManager.Instance.DeactivateView(MEDICINE_VIEW);
    }

    public void Init()
    {
        ViewManager.Instance.ActivateView(MEDICINE_VIEW);
    }

    public void UpdateState()
    {
        Debug.Log("Updating Medicine State...");
    }
}
