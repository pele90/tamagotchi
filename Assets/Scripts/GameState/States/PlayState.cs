using UnityEngine;
using UnityEngine.UI;

public class PlayState : IGameState
{
    private const string PLAY_VIEW = "play_view";
    private const int NUMBER_OF_GUESSES = 5;
    private GameObject leftChoice;
    private GameObject rightChoice;
    private Text playResultText;
    private int guessesLeft;
    private int successfullGuesses;

    public void Init()
    {
        ViewManager.Instance.ActivateView(PLAY_VIEW);
        guessesLeft = NUMBER_OF_GUESSES;
        successfullGuesses = 0;

        var view = ViewManager.Instance.GetView(PLAY_VIEW);
        leftChoice = view.transform.GetChild(0).gameObject;
        leftChoice.SetActive(false);
        rightChoice = view.transform.GetChild(1).gameObject;
        rightChoice.SetActive(false);

        playResultText = view.transform.GetChild(2).GetComponent<Text>();
        playResultText.gameObject.SetActive(false);
    }

    public void Update()
    {
        if(guessesLeft == NUMBER_OF_GUESSES)
        {
            // show animation for play tutorial text
        }

        if (guessesLeft == 0 && GameManager.Instance.Interactable == true)
        {
            // calculate result
            int failedGuesses = NUMBER_OF_GUESSES - successfullGuesses;
            bool success = failedGuesses > successfullGuesses ? false : true;

            // show qutie animation based on result
            if(success)
            {
                
                playResultText.gameObject.SetActive(true);
                // play happy animation
                // add happiness
                GameManager.Instance.monsterController.monsterData.AddHappiness();
            }
            else
            {
                playResultText.text = "You lost!";
                playResultText.gameObject.SetActive(true);
                // play sad animation
                // remove happiness
                GameManager.Instance.monsterController.monsterData.ReduceHappiness();
            }

            GameManager.Instance.Interactable = true;

            // return to action selection screen
            ViewManager.Instance.DeactivateView(PLAY_VIEW);
        }
    }

    public void AButton()
    {
        leftChoice.SetActive(true);
        guessesLeft--;

        // if player guessed that the quties will turn LEFT (from player perspective)
        if (GuessSide(0))
        {
            playResultText.text = "Correct!";
            playResultText.gameObject.SetActive(true);
            successfullGuesses++;
        }
        else
        {
            playResultText.text = "Wrong!";
            playResultText.gameObject.SetActive(true);
        }


        GameManager.Instance.Interactable = false;
    }

    public void BButton()
    {
        rightChoice.SetActive(true);
        GameManager.Instance.Interactable = false;

        // if player guessed that the quties will turn RIGHT (from player perspective)
        if(GuessSide(1))
        {
            playResultText.text = "Correct!";
            playResultText.gameObject.SetActive(true);
            successfullGuesses++;
        }
        else
        {
            playResultText.text = "Wrong!";
            playResultText.gameObject.SetActive(true);
        }

        guessesLeft--;
    }

    public void CButton()
    {
        ViewManager.Instance.DeactivateView(PLAY_VIEW);
    }

    private bool GuessSide(int side)
    {
        // side 0 = LEFT, 1 = RIGHT

        // randomSide = qutie random guess
        int randomSide = Util.GetRandomNumberBeetween(0, 1);
        Debug.Log(randomSide);

        return side == randomSide;
    }
}
