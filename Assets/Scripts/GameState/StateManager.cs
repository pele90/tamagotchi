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
        StateCounter = 0;
        activeState = ActionType.ActionOption.None;
        currentSelectedState = new FeedState();
        currentActionObject = GameObject.FindGameObjectWithTag("Feed");
        selectedDeco = GameObject.FindGameObjectWithTag("SelectDeco");
        SetDecoPosition(currentActionObject);
    }

    public void UpdateState()
    {
        if (activeState != ActionType.ActionOption.None)
            timeSinceLastInput = 0;
        else
        {
            if (!idle && timeSinceLastInput > timeToIdle)
            {
                idle = true;
                Debug.Log("Going into IDLE state...");
                selectedDeco.SetActive(false);
                currentActionObject = null;
                currentSelectedState = new TimerState();
                //activeState = ActionType.ActionOption.Timer;
            }
        }

        if(_stateCounter != _lastStateCounter)
        {
            ActionType.ActionOption option;
            ActionType.ActionOptions.TryGetValue(StateCounter, out option);
           
            //if(activeState == ActionType.ActionOption.Timer)
            //{
            //    option = lastSelectedOption;
            //}
            //else
            //    lastSelectedOption = option;

            switch (option)
            {
                case ActionType.ActionOption.Feed:
                    currentActionObject = GameObject.FindGameObjectWithTag("Feed");
                    currentSelectedState = new FeedState();
                    break;

                case ActionType.ActionOption.Light:
                    currentActionObject = GameObject.FindGameObjectWithTag("Light");
                    currentSelectedState = new LightState();
                    break;

                case ActionType.ActionOption.Play:
                    currentActionObject = GameObject.FindGameObjectWithTag("Play");
                    currentSelectedState = new PlayState();
                    break;

                case ActionType.ActionOption.Medicine:
                    currentActionObject = GameObject.FindGameObjectWithTag("Medicine");
                    currentSelectedState = new MedicineState();
                    break;

                case ActionType.ActionOption.Duck:
                    currentActionObject = GameObject.FindGameObjectWithTag("Duck");
                    currentSelectedState = new DuckState();
                    break;

                case ActionType.ActionOption.HealthMeter:
                    currentActionObject = GameObject.FindGameObjectWithTag("HealthMeter");
                    currentSelectedState = new HealthMeterState();
                    break;

                case ActionType.ActionOption.Discipline:
                    currentActionObject = GameObject.FindGameObjectWithTag("Discipline");
                    currentSelectedState = new DisciplineState();
                    break;

                case ActionType.ActionOption.Attention:
                    currentActionObject = GameObject.FindGameObjectWithTag("Attention");
                    currentSelectedState = new AttentionState();
                    break;
            }

            SetDecoPosition(currentActionObject);

            _lastStateCounter = _stateCounter;

            timeSinceLastInput = 0;
            idle = false;
        }

        timeSinceLastInput += Time.deltaTime;
    }

    public void AButton()
    {
        if(GameManager.Instance.Interactable)
        {
            if (activeState == ActionType.ActionOption.None)
                StateCounter++;
            else
                currentSelectedState.AButton();
        }
    }

    public void BButton()
    {
        if (GameManager.Instance.Interactable)
        {
            if (activeState == ActionType.ActionOption.None)
            {
                ActionType.ActionOption option;
                ActionType.ActionOptions.TryGetValue(StateCounter, out option);
                activeState = option;
                currentSelectedState.Init();
            }
            else
            {
                currentSelectedState.BButton();
            }
        }
    }

    public void CButton()
    {
        if (GameManager.Instance.Interactable)
        {
            if (activeState == ActionType.ActionOption.None)
                throw new System.NotImplementedException();
            else
                currentSelectedState.CButton();
        }
    }

    public void SetDefaultActiveState()
    {
        activeState = ActionType.ActionOption.None;
    }

    private void SetDecoPosition(GameObject gameObject)
    {
        if (!selectedDeco.activeInHierarchy)
            selectedDeco.SetActive(true);

        selectedDeco.transform.SetParent(gameObject.transform);
        selectedDeco.transform.position = gameObject.GetComponentInChildren<RectTransform>().GetChild(0).position;
    }
}
