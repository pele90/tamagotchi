using UnityEngine;
using System.Collections;

public class PlayState : IGameState
{
    private const string PLAY_VIEW = "play_view";
    private const int NUMBER_OF_GUESSES = 5;

    private GameObject leftChoice;
    private GameObject rightChoice;
    private int guessesLeft;

    public void Init()
    {
        ViewManager.Instance.ActivateView(PLAY_VIEW);
        guessesLeft = NUMBER_OF_GUESSES;

        var view = ViewManager.Instance.GetView(PLAY_VIEW);
        leftChoice = view.transform.GetChild(0).gameObject;
        leftChoice.SetActive(false);
        rightChoice = view.transform.GetChild(1).gameObject;
        rightChoice.SetActive(false);
    }

    public void UpdateState() {}

    public void AButton()
    {
        leftChoice.SetActive(true);
        GameManager.Instance.Interactable = false;
    }

    public void BButton()
    {
        rightChoice.SetActive(true);
        GameManager.Instance.Interactable = false;
    }

    public void CButton()
    {
        ViewManager.Instance.DeactivateView(PLAY_VIEW);
    }
}
