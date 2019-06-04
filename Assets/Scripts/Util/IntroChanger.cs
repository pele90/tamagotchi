using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroChanger : MonoBehaviour
{
    public Image introImage;

    public float modifier;

    public int sceneNumber;

    float currentAlpha = 0;

    private Color color;

    bool decrease = false;

    // Update is called once per frame
    void Update()
    {
        if (currentAlpha > 1)
        {
            decrease = true;
        }

        if (decrease)
        {
            currentAlpha -= modifier * Time.deltaTime;
        }
        else
        {
            currentAlpha += modifier * Time.deltaTime;
        }

        color = introImage.color;
        color.a = currentAlpha;
        introImage.color = color;

        if (decrease && currentAlpha <= 0)
        {
            SceneManager.LoadScene(sceneNumber);
        }
    }
}
