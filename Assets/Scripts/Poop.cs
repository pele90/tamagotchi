using UnityEngine;

public class Poop : MonoBehaviour
{
    public GameObject poop;
    public GameObject poopContainer;
    public float poopInterval;
    public int xFrom, xTo;

    private float poopTimer;

    // Use this for initialization
    void Start()
    {
        poopTimer = 0;
        poopInterval = GameManager.Instance.monsterData.GetPoopInterval();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.monsterData.GetPoopQueueAmount() != 0)
        {
            if (poopTimer > poopInterval)
            {
                // Instantiate poop on random location
                float x = Util.GetRandomNumberBeetween(xFrom, xTo);
                var poopObject = Instantiate(poop, new Vector3(x, 1.0f, 0), transform.rotation);
                poopObject.transform.parent = poopContainer.transform;

                GameManager.Instance.monsterData.ReducePoopQueue();
                poopTimer = 0;
            }
            else
            {
                poopTimer += Time.deltaTime;
            }
        }
    }
}
