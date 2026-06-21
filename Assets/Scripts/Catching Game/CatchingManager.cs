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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            FallingObject newObject = Instantiate(fallingObject);
            newObject.transform.position = spawnPoint.position;
            timer = 2;
        }

        scoreText.text = "Score: " + score;
    }

    public void IncreaseScore()
    {
        score++;
    }
}
