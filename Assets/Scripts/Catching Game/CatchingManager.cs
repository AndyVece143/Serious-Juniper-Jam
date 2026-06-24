using System.Collections;
using TMPro;
using UnityEngine;

public class CatchingManager : MonoBehaviour
{
    public CatchingPlayer player;
    public int score;
    public FallingObject fallingObject;
    private float timer;
    public Transform spawnPoint;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    private bool gameStarted = false;
    public MinigameTextBox minigameTextBox;
    public MinigameTextBox frenzyTime;
    public MinigameTextBox endingBox;

    public float gameTimer = 60;

    private bool frenzyMode = false;
    public LevelLoader loader;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted == true)
        {
            timer -= Time.deltaTime;

            if (timer < 0)
            {
                FallingObject newObject = Instantiate(fallingObject);
                newObject.transform.position = spawnPoint.position;

                if (frenzyMode)
                {
                    timer = 0.4f;
                }
                else
                {
                    timer = 2;
                }
            }

            gameTimer -= Time.deltaTime;
            if (gameTimer < 30 && frenzyMode == false)
            {
                frenzyMode = true;
                MinigameTextBox frenzy = Instantiate(frenzyTime);
            }
            if (gameTimer < 0)
            {
                player.StopMoving();
                StartCoroutine(EndGame());

                gameTimer = 0;
                gameStarted = false;
            }
            
        }

        scoreText.text = "Score: " + score;

        int seconds = (int)gameTimer % 60;
        int minutes = ((int)gameTimer / 60);
        timerText.text = "" + string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    public void IncreaseScore()
    {
        score++;
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.5f);
        MinigameTextBox newTextBox = Instantiate(minigameTextBox);
        yield return new WaitForSeconds(4f);
        gameStarted = true;
    }

    IEnumerator EndGame()
    {
        MinigameTextBox ending = Instantiate(endingBox);
        yield return new WaitForSeconds(4);

        if (StaticData.storyMode == true)
        {
            loader.LoadNextLevel("Floor2");
        }
    }
}
