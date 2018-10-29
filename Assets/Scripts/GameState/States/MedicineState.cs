using UnityEngine;

public class MedicineState : IGameState
{
    private const string MEDICINE_VIEW = "medicine_view";

    public void AButton() { }

    public void BButton()
    {
        GameManager.Instance.monsterController.ReduceSicknessIndex(0.5);
        ViewManager.Instance.DeactivateView(MEDICINE_VIEW);
    }

    public void CButton()
    {
        ViewManager.Instance.DeactivateView(MEDICINE_VIEW);
    }

    public void Init()
    {
        ViewManager.Instance.ActivateView(MEDICINE_VIEW);
    }

    public void Update()
    {
        Debug.Log("Giving monster a medicine. Please wait...");
    }
}
