using UnityEngine;
using UnityEngine.UI;
public class FeedState : IGameState
{
    public ActionType.FeedOption CurrentFeedOption { get; set; }

    private const string FEED_VIEW = "feed_view";
    private Image meal;
    private Image snack;
    private GameObject panel;
    private FeedData feedData;
    private MonsterController monsterController;
    private bool isDone;
    

    public void Init()
    {
        monsterController = GameManager.Instance.monsterController;

        if (monsterController.monsterData.hunger == monsterController.monsterData.MAX_HUNGER || monsterController.IsSick())
        {
            // TODO: play monster refusing animation
            Debug.Log("Don't want to eat.");
            ViewManager.Instance.DeactivateView(FEED_VIEW);
        }
        else
        {
            feedData = ViewManager.Instance.GetView(FEED_VIEW).GetComponent<FeedData>();

            var view = ViewManager.Instance.GetView(FEED_VIEW);

            CurrentFeedOption = ActionType.FeedOption.Meal;

            panel = view.transform.GetChild(0).gameObject;
            panel.SetActive(true);

            meal = view.transform.GetChild(1).GetComponent<Image>();
            meal.gameObject.SetActive(true);
            Util.ShowObject(meal);

            snack = view.transform.GetChild(2).GetComponent<Image>();
            snack.gameObject.SetActive(true);
            Util.MakeObjectSeeThrough(snack);

            ViewManager.Instance.ActivateView(FEED_VIEW);

            isDone = false;
        }       
    }

    public void Update()
    {
        // this is to prevent update from going multiple time if tough the action is done
        if(isDone)
        {
            return;
        }
        // TODO: check mood (number of happiness hearts and is sick) + personality
        if (monsterController.monsterData.hunger == monsterController.monsterData.MAX_HUNGER || monsterController.IsSick())
        {
            // TODO: play monster refusing animation
            Debug.Log("Don't want to eat.");
            ViewManager.Instance.DeactivateView(FEED_VIEW);
        }
    }

    public void AButton()
    {
        UpdateChoices();
    }

    public void BButton()
    {
        // hide food choices
        panel.SetActive(false);
        meal.gameObject.SetActive(false);
        snack.gameObject.SetActive(false);

        // TODO: Do animation of falling meal and monster eating the meal
        Animator animator = ViewManager.Instance.GetView(FEED_VIEW).transform.GetChild(3).GetComponent<Animator>();
        animator.Play("food_falling");
        GameManager.Instance.Interactable = false;

        if (CurrentFeedOption == ActionType.FeedOption.Meal)
        {
            GameManager.Instance.monsterController.monsterData.AddHunger();
            GameManager.Instance.monsterController.monsterData.AddWeight(feedData.mealWeightAmount);
        }
        else
        {
            GameManager.Instance.monsterController.monsterData.AddHunger();
            GameManager.Instance.monsterController.monsterData.AddHappiness();
            GameManager.Instance.monsterController.monsterData.AddWeight(feedData.snackWeightAmount);
            GameManager.Instance.monsterController.AddSicknessIndex(feedData.snackSicknessIndexAmount);
        }

        isDone = true;
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

            Util.MakeObjectSeeThrough(meal);
            Util.ShowObject(snack);
        }
        else
        {
            CurrentFeedOption = ActionType.FeedOption.Meal;

            Util.ShowObject(meal);
            Util.MakeObjectSeeThrough(snack);
        }
    }
}
