using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SlidingManager : MonoBehaviour
{
    public Transform[] spawns;
    public StopItem stopItem;
    private int previousSpawn = 67;
    private StopItem currentItem;
    private bool gameStarted = false;
    public SlidingPlayer player;
    public MinigameTextBox minigameTextBox;
    public MinigameTextBox endingBox;
    private int signNumber = 0;
    public AudioClip collect;
    public LevelLoader loader;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {
        if (currentItem == null && gameStarted == true && signNumber < 10)
        {
            SpawnStopSign();

            //if (player.spinSpeed <= 0)
            //{
            //    player.StopSpinning();
            //    gameStarted = false;
            //    StartCoroutine(EndGame());
            //}
        }

        if (signNumber == 10 && gameStarted == true)
        {
            player.StopSpinning();
            gameStarted = false;
            StartCoroutine(EndGame());
        }
    }

    private void SpawnStopSign()
    {
        if (signNumber == 10)
        {
            return;
        }
        int i = Random.Range(0, 5);
        if (i == previousSpawn)
        {
            SpawnStopSign();
            return;
        }

        StopItem newStop = Instantiate(stopItem);
        newStop.transform.position = spawns[i].position;
        currentItem = newStop;

        previousSpawn = i;
        //signNumber++;
    }

    public void NewItem()
    {
        SoundManager.instance.PlaySound(collect);
        signNumber++;
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.5f);
        MinigameTextBox newTextBox = Instantiate(minigameTextBox);
        yield return new WaitForSeconds(4f);
        gameStarted = true;
        player.state = SlidingPlayer.State.Standard;
    }

    IEnumerator EndGame()
    {
        Destroy(currentItem);
        MinigameTextBox ending = Instantiate(endingBox);
        yield return new WaitForSeconds(4);

        if (StaticData.storyMode == true)
        {
            loader.LoadNextLevel("Floor4");
        }
        else
        {
            loader.LoadNextLevel("Minigames");
        }
    }
}
