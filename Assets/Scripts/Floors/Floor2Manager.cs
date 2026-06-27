using System.Collections;
using UnityEngine;

public class Floor2Manager : MonoBehaviour
{
    public Player player;
    public CameraController mainCamera;
    public SoloBigDialogue dialogue1;
    public BigDialogue dialogue2;
    public BigDialogue dialogue3;
    public BigDialogue dialogue4;
    private int progress;
    public AudioClip earthquake;
    public LevelLoader loader;
    public AudioSource source;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (StaticData.floor2Second == true)
        {
            player.transform.position = new Vector2(-3.5f, -1.8f);
            mainCamera.state = CameraController.State.StayStill;
            mainCamera.transform.position = new Vector3(0, 0, -10);
            source.Play();
            progress = 4;
        }
        StartCutscene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCutscene()
    {
        switch (progress)
        {
            case 0:
                StartCoroutine(Cutscene1());
                progress++;
                break;
            case 1:
                StartCoroutine(Cutscene2());
                progress++;
                break;
            case 2:
                StartCoroutine(Cutscene3());
                progress++;
                break;
            case 3:
                StartCatchingGame();
                break;
            case 4:
                StartCoroutine(Cutscene4());
                break;
        }
    }

    private IEnumerator Cutscene1()
    {
        yield return new WaitForSeconds(2);
        SoloBigDialogue newDialogue = Instantiate(dialogue1);
    }

    private IEnumerator Cutscene2()
    {
        StartCoroutine(player.GoToPlace(new Vector2(-3.5f, -1.8f), 5));
        yield return new WaitForSeconds(5);
        StartCoroutine(mainCamera.GoToPlace(new Vector3(0, 0, -10), 3));
        yield return new WaitForSeconds(3);
        BigDialogue newDialogue = Instantiate(dialogue2);
        source.Play();
    }

    private IEnumerator Cutscene3()
    {
        yield return new WaitForSeconds(0.5f);
        source.Stop();
        mainCamera.SwitchToEarthquake();
        SoundManager.instance.PlaySound(earthquake);
        player.StopMoving(2);
        yield return new WaitForSeconds(3);
        BigDialogue newDialogue = Instantiate(dialogue3);
    }

    private void StartCatchingGame()
    {
        progress++;
        StaticData.floor2Second = true;
        loader.LoadNextLevel("CatchingGame");
    }

    private IEnumerator Cutscene4()
    {
        yield return new WaitForSeconds(1);
        player.transform.localScale = new Vector3(-1, 1, 1);
        yield return new WaitForSeconds(1);
        player.transform.localScale = new Vector3(1, 1, 1);
        yield return new WaitForSeconds(1);
        BigDialogue newDialogue = Instantiate(dialogue4);
    }
}
