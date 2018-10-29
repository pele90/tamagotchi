using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    #region Properties

    public MonsterController monsterController;

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

    public DateTime CurrentSystemTime { get; set; }
    public GameObject currentTime;
    public UnityEngine.UI.Text SicknessText;

    #endregion

    // Use this for initialization
    void Start()
    {
        StateManager.Instance.Init();
        Interactable = true;

        SicknessText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        StateManager.Instance.Update();
        CurrentSystemTime = DateTime.Now;
        (currentTime.GetComponent<UnityEngine.UI.Text>()).text = CurrentSystemTime.ToString("HH:mm:ss", System.Globalization.CultureInfo.CreateSpecificCulture("hr-HR"));
    }
}
