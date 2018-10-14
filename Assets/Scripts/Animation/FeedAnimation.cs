using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedAnimation : MonoBehaviour
{
    void EndFeedAnimation()
    {
        GameManager.Instance.Interactable = true;
        transform.localPosition = new Vector3(0, 315, 0);
        ViewManager.Instance.DeactivateView("feed_view");

        GameManager.Instance.monsterData.AddToPoopQueue();
    }
}
