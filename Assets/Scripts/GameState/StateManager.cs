using UnityEngine;

public class StateManager : MonoBehaviour
{
    private static StateManager _stateManager;
    private GameObject selectedDeco;
    private GameObject currentActionObject;
    private IGameState currentSelectedState;
    private ActionType.ActionOption activeState;
    private ActionType.ActionOption lastSelectedOption;
    private int _stateCounter;
    private int _lastStateCounter;
    private float timeSinceLastInput = 0;
    private bool idle;

    public float timeToIdle;

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
        Instance.StateCounter = 0;
        Instance.activeState = ActionType.ActionOption.None;
        Instance.currentSelectedState = new FeedState();
        Instance.currentActionObject = GameObject.FindGameObjectWithTag("Feed");
        Instance.selectedDeco = GameObject.FindGameObjectWithTag("SelectDeco");
        SetDecoPosition(currentActionObject);
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
            if (!idle && timeSinceLastInput > timeToIdle)
            {
                Instance.idle = true;
                //Debug.Log("Going into IDLE state...");
                Instance.selectedDeco.SetActive(false);
                Instance.currentActionObject = null;
                Instance.currentSelectedState = new TimerState();
                //activeState = ActionType.ActionOption.Timer;
            }
        }

        if (Instance._stateCounter != Instance._lastStateCounter)
        {
            ActionType.ActionOption option;
            ActionType.ActionOptions.TryGetValue(Instance.StateCounter, out option);

            //if(activeState == ActionType.ActionOption.Timer)
            //{
            //    option = lastSelectedOption;
            //}
            //else
            //    lastSelectedOption = option;

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

                case ActionType.ActionOption.Attention:
                    Instance.currentActionObject = GameObject.FindGameObjectWithTag("Attention");
                    Instance.currentSelectedState = new AttentionState();
                    break;
            }

            // set selected action marker on currently selected state
            SetDecoPosition(Instance.currentActionObject);

            // set current state as last state
            Instance._lastStateCounter = Instance._stateCounter;

            Instance.timeSinceLastInput = 0;
            Instance.idle = false;
        }

        Instance.timeSinceLastInput += Time.deltaTime;
    }

    public void AButton()
    {
        if (GameManager.Instance.Interactable)
        {
            if (Instance.activeState == ActionType.ActionOption.None)
                Instance.StateCounter++;
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
                throw new System.NotImplementedException();
            else
                Instance.currentSelectedState.CButton();
        }
    }

    public void SetDefaultActiveState()
    {
        Instance.activeState = ActionType.ActionOption.None;
    }

    private void SetDecoPosition(GameObject gameObject)
    {
        if (!Instance.selectedDeco.activeInHierarchy)
            Instance.selectedDeco.SetActive(true);

        Instance.selectedDeco.transform.SetParent(gameObject.transform);
        Instance.selectedDeco.transform.position = gameObject.GetComponentInChildren<RectTransform>().GetChild(0).position;
    }
}
