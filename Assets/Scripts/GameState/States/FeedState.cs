using UnityEngine;

public class FeedState : IGameState
{
    public ActionType.FeedOption CurrentFeedOption { get; set; }

    private const string FEED_VIEW = "feed_view";
    private Transform meal;
    private Transform snack;

    public void Init()
    {
        Debug.Log("Initializing Feed State...");

        var view = ViewManager.Instance.GetView(FEED_VIEW);

        CurrentFeedOption = ActionType.FeedOption.Meal;
        meal = view.transform.GetChild(0);
        meal.gameObject.SetActive(true);

        snack = view.transform.GetChild(1);
        snack.gameObject.SetActive(false);

        ViewManager.Instance.ActivateView(FEED_VIEW);
    }

    public void Update()
    {
        Debug.Log("Updating Feed State...");
    }

    public void AButton()
    {
        UpdateChoices();
    }

    public void BButton()
    {
        // TODO: Do animation of falling meal and monster eating the meal
        Animator animator = ViewManager.Instance.GetView(FEED_VIEW).transform.GetChild(2).GetComponent<Animator>();
        animator.Play("food_falling");
        GameManager.Instance.Interactable = false;

        if (CurrentFeedOption == ActionType.FeedOption.Meal)
        {
            GameManager.Instance.monsterData.AddHunger();
            GameManager.Instance.monsterData.AddWeight(0.1);
        }
        else
        {
            GameManager.Instance.monsterData.AddHunger();
            GameManager.Instance.monsterData.AddHappiness();
            GameManager.Instance.monsterData.AddWeight(0.20);
        }
    }

    public void CButton()
    {
        ViewManager.Instance.DeactivateView(FEED_VIEW);
    }

    private void UpdateChoices()
    {
        if (CurrentFeedOption == ActionType.FeedOption.Meal)
        {
            CurrentFeedOption = ActionType.FeedOption.Snack;
            Debug.Log("User selected SNACK!");

            // TODO: change feed option visually
            meal.gameObject.SetActive(false);
            snack.gameObject.SetActive(true);
        }
        else
        {
            CurrentFeedOption = ActionType.FeedOption.Meal;
            Debug.Log("User selected MEAL!");

            // TODO: change feed option visually
            meal.gameObject.SetActive(true);
            snack.gameObject.SetActive(false);
        }
    }
}
