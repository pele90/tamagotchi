using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Properties

    public MonsterData monsterData;

    private static GameManager _gameManager;

    public static GameManager Instance
    {
        get
        {
            if (!_gameManager)
            {
                _gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (!_gameManager)
                {
                    Debug.LogError("There needs to be one active GameManager script on a GameObject in your scene.");
                }
                else
                {

                }
            }

            return _gameManager;
        }
    }

    public bool Interactable { get; set; }

    #endregion

    // Use this for initialization
    void Start()
    {
        StateManager.Instance.Init();
        Interactable = true;
    }

    // Update is called once per frame
    void Update()
    {
        StateManager.Instance.Update();
    }
}
