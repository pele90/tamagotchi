using UnityEngine;

public class AnimationEventTest : MonoBehaviour
{
    public void EndAnimation()
    {
        GameManager.Instance.Interactable = true;
        gameObject.SetActive(false);
    }

    public void EndFallingFoodAnimation()
    {
        ViewManager.Instance.DeactivateView("feed_view");
    }
    
}
