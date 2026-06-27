using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour
{
    public GameObject mainTitle;
    public GameObject credits;
    public GameObject mode;

    public float duration;

    private float buttonTimer;
    public LevelLoader loader;
    public float moving;
    public float dampSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(mainTitle.transform.position);
        Debug.Log(credits.transform.position);
        StaticData.floor2Second = false;
        StaticData.floor3Second = false;
        StaticData.floor4Second = false;
        StaticData.storyMode = false;
    }

    // Update is called once per frame
    void Update()
    {
        buttonTimer -= Time.deltaTime;
    }

    public void SlideLeft()
    {
        if (buttonTimer <= 0)
        {
            StartCoroutine(MoveElements(false));
            buttonTimer = duration;
        }
    }

    public void SlideRight()
    {
        if (buttonTimer <= 0)
        {
            StartCoroutine(MoveElements(true));
            buttonTimer = duration;
        }
    }

    public void StartStory()
    {
        StaticData.storyMode = true;
        loader.LoadNextLevel("Prologue");
    }

    public void StartMinigames()
    {
        StaticData.storyMode = false;
        loader.LoadNextLevel("Minigames");
    }

    IEnumerator MoveElements(bool right)
    {
        float time = 0;
        float moveAmount = moving;

        if (!right)
        {
            moveAmount = -moving;
        }

        Vector2 titleVector = new Vector2(mainTitle.transform.position.x + moveAmount, 0);
        Vector2 creditsVector = new Vector2(credits.transform.position.x + moveAmount, 0);
        Vector2 modeVector = new Vector2(mode.transform.position.x + moveAmount, 0);

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = 1.0f - Mathf.Exp(-dampSpeed * Time.deltaTime);

            mainTitle.gameObject.transform.position = Vector2.Lerp(mainTitle.transform.position, titleVector, t);
            credits.gameObject.transform.position = Vector2.Lerp(credits.transform.position, creditsVector, t);
            mode.gameObject.transform.position = Vector2.Lerp(mode.transform.position, modeVector, t);

            yield return null;
        }
    }
}
