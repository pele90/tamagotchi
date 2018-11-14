using UnityEngine;

public class PlayAnimationHandler : MonoBehaviour
{
    public void EndPlayResultAnimation()
    {
        GameManager.Instance.Interactable = true;
        ViewManager.Instance.DeactivateView("play_view");
    }

}
