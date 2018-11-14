using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    #region Properties

    public MonsterController monsterController;
    public DateTime CurrentGameTime { get; set; }
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

    private PlayerData playerData;
    private static GameManager _gameManager;

    public GameObject currentTimeText;
    public UnityEngine.UI.Text SicknessText;
    public UnityEngine.UI.Image BulbImage;

    #endregion

    public PlayerData GetPlayerData()
    {
        return playerData;
    }

    // Use this for initialization
    void Start()
    {
        playerData = GetComponent<PlayerData>();
        playerData.Init();

        StateManager.Instance.Init();
        Interactable = true;

        SicknessText.gameObject.SetActive(false);
        BulbImage.gameObject.SetActive(false);

        if (playerData.GetData().IsInitialized)
        {
            LoadTime();
        }
    }

    // Update is called once per frame
    void Update()
    {
        StateManager.Instance.Update();

        if(playerData.GetData().IsInitialized)
            CurrentGameTime = CurrentGameTime.AddSeconds(Time.deltaTime);

        (currentTimeText.GetComponent<UnityEngine.UI.Text>()).text = CurrentGameTime.ToString("HH:mm:ss", System.Globalization.CultureInfo.CreateSpecificCulture("hr-HR"));

    }

    void LoadTime()
    {
        TimeSpan timeDiff = DateTime.Now - playerData.GetData().RecordedRealTime;
        CurrentGameTime = playerData.GetData().RecordedGameTime.Add(timeDiff);
        monsterController.SetBirthDateTime(playerData.GetData().birthDateTime);
    }

    void OnApplicationQuit()
    {
        playerData.GetData().RecordedGameTime = CurrentGameTime;
        playerData.GetData().RecordedRealTime = DateTime.Now;
        playerData.Save();
    }
}
