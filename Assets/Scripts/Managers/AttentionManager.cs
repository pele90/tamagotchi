using UnityEngine;
using UnityEngine.UI;

public class AttentionManager : MonoBehaviour
{
    public GameObject attentionIcon;
    public float MaxActiveTime;
    public float TimeBetweenAlarm;
    public bool active;

    private Image iconImage;
    private MonsterController monsterController;
    private MonsterData monsterData;

    private float activeTime;
    private float notActiveTime;
    private bool initialized;

    // Use this for initialization
    void Start()
    {
        iconImage = attentionIcon.GetComponent<Image>();
        monsterController = GameManager.Instance.monsterController;
        monsterData = monsterController.monsterData;
        initialized = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            activeTime += Time.deltaTime;

            if (activeTime >= MaxActiveTime)
            {
                DeactivateAttentionAlarm();
            }
        }
        else
        {
            notActiveTime += Time.deltaTime;

            if(!initialized || notActiveTime > TimeBetweenAlarm)
            {
                if (monsterData.hunger == 0
                || monsterData.happiness == 0
                || monsterController.IsSick()
                || !monsterController.IsPoopContainerEmpty()
            )
                {
                    AttentionAlarm();

                    if (!initialized)
                        initialized = true;
                }
            }
        }



        // TODO: if discipline is low and some random reason, set false alarm to the user
    }

    void AttentionAlarm()
    {
        // Make sound
        // Vibrate
        //Etc
        notActiveTime = 0;
        active = true;
        Util.ShowObject(iconImage);
    }

    public void DeactivateAttentionAlarm()
    {
        activeTime = 0;
        active = false;
        Util.MakeObjectSeeThrough(iconImage);
    }
}
