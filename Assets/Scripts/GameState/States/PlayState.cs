using UnityEngine;
using UnityEngine.UI;

public class PlayState : IGameState
{
    private const string PLAY_VIEW = "play_view";
    private const int NUMBER_OF_GUESSES = 1;
    private GameObject leftChoice;
    private GameObject rightChoice;
    private Text choiceResultText;
    private Text playResultText;
    private int guessesLeft;
    private int successfullGuesses;
    private bool gameFinished;

    private MonsterController monsterController;

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

        choiceResultText = view.transform.GetChild(2).GetComponent<Text>();
        choiceResultText.gameObject.SetActive(false);
        playResultText = view.transform.GetChild(3).GetComponent<Text>();
        playResultText.gameObject.SetActive(false);

        monsterController = GameManager.Instance.monsterController;
        gameFinished = false;
    }

    public void Update()
    {
        if(gameFinished)
        {
            return;
        }

        // TODO: check mood (number of happiness hearts and is sick) + personality
        if(monsterController.monsterData.happiness == monsterController.monsterData.MAX_HAPPINESS || monsterController.IsSick())
        {
            // TODO: play monster refusing animation

            playResultText.text = "Don't want to play!";
            playResultText.gameObject.SetActive(true);
        }

        // Start of the game
        if(guessesLeft == NUMBER_OF_GUESSES)
        {
            // show animation for play tutorial text
        }

        // if player has available guesses and can press buttons
        if (guessesLeft == 0 && GameManager.Instance.Interactable == true)
        {
            // calculate result
            int failedGuesses = NUMBER_OF_GUESSES - successfullGuesses;
            bool success = failedGuesses > successfullGuesses ? false : true;

            // show qutie animation based on result
            if(success)
            {
                // play happy animation
                // add happiness
                GameManager.Instance.monsterController.monsterData.AddHappiness();

                playResultText.text = "Nice! You won.";
                // This will run animation on gameObject active and deactivate current view thus return to main area
                playResultText.gameObject.SetActive(true);
            }
            else
            {
                // play sad animation
                // remove happiness
                GameManager.Instance.monsterController.monsterData.ReduceHappiness();

                playResultText.text = "Sorry! You lost.";
                // This will run animation on gameObject active and deactivate current view thus return to main area
                playResultText.gameObject.SetActive(true);
            }

            GameManager.Instance.Interactable = false;
            gameFinished = true;
        }
    }

    public void AButton()
    {
        leftChoice.SetActive(true);
        guessesLeft--;

        // if player guessed that the quties will turn LEFT (from player perspective)
        if (GuessSide(0))
        {
            choiceResultText.text = "Correct!";
            choiceResultText.gameObject.SetActive(true);
            successfullGuesses++;
        }
        else
        {
            choiceResultText.text = "Wrong!";
            choiceResultText.gameObject.SetActive(true);
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
            choiceResultText.text = "Correct!";
            choiceResultText.gameObject.SetActive(true);
            successfullGuesses++;
        }
        else
        {
            choiceResultText.text = "Wrong!";
            choiceResultText.gameObject.SetActive(true);
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

        return side == randomSide;
    }
}
