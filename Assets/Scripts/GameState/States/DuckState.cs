using UnityEngine;

public class DuckState : IGameState
{
    private const string DUCK_VIEW = "duck_view";

    public GameObject poopContainer;

    public void AButton() { }

    public void BButton() { }

    public void CButton()
    {
        ViewManager.Instance.DeactivateView(DUCK_VIEW);
    }

    public void Init()
    {
        ViewManager.Instance.ActivateView(DUCK_VIEW);
        poopContainer = GameObject.FindGameObjectWithTag("PoopContainer");
    }

    public void Update()
    {
        int childCount = poopContainer.transform.childCount;

        if(childCount != 0)
        {
            for (int i = 0; i < childCount; i++)
            {
                Transform childTranform = poopContainer.transform.GetChild(i);
                Object.Destroy(childTranform.gameObject, i * 2);
            }
        }

        ViewManager.Instance.DeactivateView(DUCK_VIEW);
    }
}
