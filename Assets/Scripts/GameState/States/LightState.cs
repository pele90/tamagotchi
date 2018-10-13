using UnityEngine;

public class LightState : IGameState
{
    private const string LIGHT_VIEW = "light_view";

    private GameObject onButton;
    private GameObject offButton;
    private bool isOff;

    public void Init()
    {
        ViewManager.Instance.ActivateView(LIGHT_VIEW);

        var view = ViewManager.Instance.GetView(LIGHT_VIEW);
        onButton = view.transform.GetChild(0).gameObject;
        onButton.SetActive(true);
        offButton = view.transform.GetChild(1).gameObject;
        offButton.SetActive(false);
        isOff = false;
    }

    public void Update()
    {
        Debug.Log("Updating Light State...");
    }

    public void AButton()
    {
        ToggleLightSwitch();
    }

    public void BButton()
    {
        if (isOff)
            ViewManager.Instance.lights.SetActive(false);
        else
            ViewManager.Instance.lights.SetActive(true);

        ViewManager.Instance.DeactivateView(LIGHT_VIEW);
    }

    public void CButton()
    {
        ViewManager.Instance.DeactivateView(LIGHT_VIEW);
    }

    private void ToggleLightSwitch()
    {
        isOff = !isOff;

        if(isOff)
        {
            onButton.SetActive(false);
            offButton.SetActive(true);
        }
        else
        {
            offButton.SetActive(false);
            onButton.SetActive(true);
        }
    }
}
