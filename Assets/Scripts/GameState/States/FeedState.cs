using UnityEngine;

public class FeedState : IGameState
{
    public ActionType.FeedOption CurrentFeedOption { get; set; }

    private const string FEED_VIEW = "feed_view";
    private Transform meal;
    private Transform snack;

    public void Init()
    {
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
            GameManager.Instance.monsterController.monsterData.AddHunger();
            GameManager.Instance.monsterController.monsterData.AddWeight(0.1);
        }
        else
        {
            GameManager.Instance.monsterController.monsterData.AddHunger();
            GameManager.Instance.monsterController.monsterData.AddHappiness();
            GameManager.Instance.monsterController.monsterData.AddWeight(0.20);
            GameManager.Instance.monsterController.AddSicknessIndex(0.1);
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

            // TODO: change feed option visually
            meal.gameObject.SetActive(false);
            snack.gameObject.SetActive(true);
        }
        else
        {
            CurrentFeedOption = ActionType.FeedOption.Meal;

            // TODO: change feed option visually
            meal.gameObject.SetActive(true);
            snack.gameObject.SetActive(false);
        }
    }
}
