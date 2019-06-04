using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
    private static StateManager _stateManager;
    private GameObject currentActionObject;
    private IGameState currentSelectedState;
    private ActionType.ActionOption activeState;
    private ActionType.ActionOption lastSelectedOption;
    private int _stateCounter;
    private int _lastStateCounter;
    private float timeSinceLastInput = 0;
    private bool idle;
    private bool selectLastOption;
    private AudioSource audioSource;

    public float timeToIdle;
    public AudioClip changeActionClip;
    public AudioClip selectActionClip;

    public static StateManager Instance
    {
        get
        {
            if (!_stateManager)
            {
                _stateManager = FindObjectOfType(typeof(StateManager)) as StateManager;

                if (!_stateManager)
                {
                    Debug.LogError("There needs to be one active StateManager script on a GameObject in your scene.");
                }
                else
                {

                }
            }

            return _stateManager;
        }
    }

    public int StateCounter
    {
        get
        {
            return _stateCounter;
        }

        set
        {
            _lastStateCounter = _stateCounter;
            _stateCounter = value;
            if (_stateCounter > ActionType.ActionOptions.Count - 1)
                _stateCounter = 0;
        }
    }

    public void Init()
    {
        bool isInitialized = GameManager.Instance.GetPlayerData().GetData().IsInitialized;
        audioSource = GetComponent<AudioSource>();

        Instance.StateCounter = 0;

        if (isInitialized)
        {
            Instance.activeState = ActionType.ActionOption.None;
            Instance.currentSelectedState = new FeedState();
            Instance.currentActionObject = GameObject.FindGameObjectWithTag("Feed");

            ShowActiveAction();
        }
        else
        {
            Instance.activeState = ActionType.ActionOption.Timer;
            Instance.currentSelectedState = new TimerState();
            Instance.currentActionObject = null;
            Instance.currentSelectedState.Init();
        }
    }

    public void Update()
    {
        if (Instance.activeState != ActionType.ActionOption.None)
        {
            Instance.timeSinceLastInput = 0;

            // update current state
            Instance.currentSelectedState.Update();
        }
        else
        {
            if (!idle && timeSinceLastInput > timeToIdle && GameManager.Instance.GetPlayerData().GetData().IsInitialized)
            {
                Instance.idle = true;
                Instance.currentActionObject = null;
                Instance.currentSelectedState = new TimerState();
            }
        }

        if (Instance._stateCounter != Instance._lastStateCounter || Instance.selectLastOption)
        {
            ActionType.ActionOption option;
            ActionType.ActionOptions.TryGetValue(Instance.StateCounter, out option);

            GameObject currentAction = Instance.currentActionObject;
            if(currentAction != null)
            {
                Transform currentActionTransform = currentAction.transform;
                HideLastActiveAction(currentActionTransform);
            }

            switch (option)
            {
                case ActionType.ActionOption.Feed:
                    Instance.currentActionObject = GameObject.FindGameObjectWithTag("Feed");
                    Instance.currentSelectedState = new FeedState();
                    break;

                case ActionType.ActionOption.Light:
                    Instance.currentActionObject = GameObject.FindGameObjectWithTag("Light");
                    Instance.currentSelectedState = new LightState();
                    break;

                case ActionType.ActionOption.Play:
                    Instance.currentActionObject = GameObject.FindGameObjectWithTag("Play");
                    Instance.currentSelectedState = new PlayState();
                    break;

                case ActionType.ActionOption.Medicine:
                    Instance.currentActionObject = GameObject.FindGameObjectWithTag("Medicine");
                    Instance.currentSelectedState = new MedicineState();
                    break;

                case ActionType.ActionOption.Duck:
                    Instance.currentActionObject = GameObject.FindGameObjectWithTag("Duck");
                    Instance.currentSelectedState = new DuckState();
                    break;

                case ActionType.ActionOption.HealthMeter:
                    Instance.currentActionObject = GameObject.FindGameObjectWithTag("HealthMeter");
                    Instance.currentSelectedState = new HealthMeterState();
                    break;

                case ActionType.ActionOption.Discipline:
                    Instance.currentActionObject = GameObject.FindGameObjectWithTag("Discipline");
                    Instance.currentSelectedState = new DisciplineState();
                    break;
            }

            // set selected action marker on currently selected state
            ShowActiveAction();

            // set current state as last state
            Instance._lastStateCounter = Instance._stateCounter;

            Instance.timeSinceLastInput = 0;
            Instance.idle = false;
            Instance.selectLastOption = false;
        }

        Instance.timeSinceLastInput += Time.deltaTime;
    }

    public void AButton()
    {
        if (GameManager.Instance.Interactable)
        {
            if (Instance.activeState == ActionType.ActionOption.None && !Instance.idle)
                Instance.StateCounter++;
            else if (Instance.activeState == ActionType.ActionOption.None && Instance.idle)
                Instance.selectLastOption = true;
            else
                Instance.currentSelectedState.AButton();
        }
    }

    public void BButton()
    {
        if (GameManager.Instance.Interactable)
        {
            if (Instance.activeState == ActionType.ActionOption.None)
            {
                audioSource.PlayOneShot(selectActionClip);

                ActionType.ActionOption option;
                ActionType.ActionOptions.TryGetValue(Instance.StateCounter, out option);
                Instance.activeState = option;
                Instance.currentSelectedState.Init();
            }
            else
            {
                Instance.currentSelectedState.BButton();
            }
        }
    }

    public void CButton()
    {
        if (GameManager.Instance.Interactable)
        {
            if (Instance.activeState == ActionType.ActionOption.None)
                return;
            else
                Instance.currentSelectedState.CButton();
        }
    }

    public void SetDefaultActiveState()
    {
        Instance.activeState = ActionType.ActionOption.None;
    }

    private void ShowActiveAction()
    {
        Transform parent = Instance.currentActionObject.transform;
        Image tempImage = parent.GetChild(0).GetComponent<Image>();

        var tempColor = tempImage.color;
        tempColor.a = 1;
        tempImage.color = tempColor;

        tempImage = parent.GetChild(1).GetComponent<Image>();
        tempColor = Instance.currentActionObject.transform.GetChild(1).GetComponent<Image>().color;
        tempColor.a = 1;
        tempImage.color = tempColor;
    }

    private void HideLastActiveAction(Transform parent)
    {
        audioSource.PlayOneShot(changeActionClip);

        Image tempImage = parent.GetChild(0).GetComponent<Image>();

        var tempColor = tempImage.color;
        tempColor.a = 0.2f;
        tempImage.color = tempColor;

        tempImage = parent.GetChild(1).GetComponent<Image>();
        tempColor = Instance.currentActionObject.transform.GetChild(1).GetComponent<Image>().color;
        tempColor.a = 0.2f;
        tempImage.color = tempColor;
    }
}
