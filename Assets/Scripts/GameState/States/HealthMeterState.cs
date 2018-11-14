using UnityEngine;

public class HealthMeterState : IGameState
{
    private const string HEALTHMETER_VIEW = "healthmeter_view";

    private GameObject firstPanel;
    private GameObject secondPanel;

    public void Init()
    {
        var view = ViewManager.Instance.GetView(HEALTHMETER_VIEW);

        firstPanel = view.transform.GetChild(0).gameObject;
        firstPanel.SetActive(true);

        secondPanel = view.transform.GetChild(1).gameObject;
        secondPanel.SetActive(false);

        ViewManager.Instance.ActivateView(HEALTHMETER_VIEW);
    }

    public void Update()
    {
       
    }

    public void AButton()
    {
        TogglePanels();
    }

    public void BButton()
    {

    }

    public void CButton()
    {
        ViewManager.Instance.DeactivateView(HEALTHMETER_VIEW);
    }

    private void TogglePanels()
    {
        if(firstPanel.activeInHierarchy)
        {
            firstPanel.SetActive(false);
            secondPanel.SetActive(true);
        }
        else if (secondPanel.activeInHierarchy)
        {
            secondPanel.SetActive(false);
            firstPanel.SetActive(true);
        }
    }

}
