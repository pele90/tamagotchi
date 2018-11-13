using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    #region Properties

    public MonsterController monsterController;
    public DateTime CurrentSystemTime { get; set; }
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

    public GameObject currentTime;
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
        else
        {
            //CurrentSystemTime = DateTime.Now;
            //playerData.GetData().IsInitialized = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        StateManager.Instance.Update();

        if(playerData.GetData().IsInitialized)
            CurrentSystemTime = CurrentSystemTime.AddSeconds(Time.deltaTime);

        (currentTime.GetComponent<UnityEngine.UI.Text>()).text = CurrentSystemTime.ToString("HH:mm:ss", System.Globalization.CultureInfo.CreateSpecificCulture("hr-HR"));

        if (Input.GetKey(KeyCode.R))
        {
            RecordCurrentTime();
        }

    }

    void RecordCurrentTime()
    {
        playerData.GetData().RecordedTime = CurrentSystemTime;
    }

    void LoadTime()
    {
        CurrentSystemTime = playerData.GetData().RecordedTime;
    }

    void OnApplicationQuit()
    {
        playerData.GetData().RecordedTime = CurrentSystemTime;
        playerData.Save();
    }
}
