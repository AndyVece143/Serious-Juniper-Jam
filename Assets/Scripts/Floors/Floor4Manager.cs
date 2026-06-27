using System.Collections;
using UnityEngine;

public class Floor4Manager : MonoBehaviour
{
    public Player player;
    public GameObject playerShadow;
    public CameraController mainCamera;
    public LevelLoader loader;
    private int progress;
    public GameObject sonny;
    public GameObject sonnyShadow;
    public AudioClip jump;
    public AudioClip explosion;
    public GameObject fadeToBlack;

    public SoloBigDialogue dialogue1;
    public SoloBigDialogue dialogue2;
    public BigDialogue dialogue3;
    public BigDialogue dialogue4;
    public BigDialogue dialogue5;
    public BigDialogue dialogue6;
    public AudioSource source;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        switch (StaticData.floor4Second)
        {
            case false:
                player.transform.position = new Vector2(-13.32f, -1.8f);
                break;
            case true:
                player.transform.position = new Vector2(-8.68f, -1.8f);
                sonny.transform.position = new Vector2(-4.6f, -0.68f);
                progress = 6;
                source.Play();
                break;
        }

        StartCutscene();
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
                StartCoroutine(Cutscene4());
                progress++;
                break;
            case 4:
                StartCoroutine(Cutscene5());
                progress++;
                break;
            case 5:
                StartSlidingGame();
                break;
            case 6:
                StartCoroutine (Cutscene6());
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
        StartCoroutine(player.GoToPlace(new Vector2(-10f, -3f), 3));
        yield return new WaitForSeconds(3);
        StartCoroutine(player.GoToPlace(new Vector2(-0f, -3f), 5));
        yield return new WaitForSeconds(6);
        player.transform.localScale = new Vector3(-1, 1, 1);
        yield return new WaitForSeconds(1);
        SoloBigDialogue newDialogue = Instantiate(dialogue2);
    }

    private IEnumerator Cutscene3()
    {
        mainCamera.state = CameraController.State.StayStill;
        sonny.GetComponent<Animator>().SetBool("happy", true);
        float time = 0;
        Vector2 startingPos = sonny.transform.position;
        while (time < 3)
        {
            float t = time / 3;
            sonny.transform.position = Vector2.Lerp(startingPos, new Vector2(8.24f, -3), t);
            time += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(1);

        float hopTime = 0;
        Vector2 startingHopPos = sonny.transform.position;
        SoundManager.instance.PlaySound(jump);
        sonnyShadow.SetActive(false);
        while (hopTime < 0.5)
        {
            float t = hopTime / 0.5f;
            Vector2 currentPos = Vector2.Lerp(startingHopPos, new Vector2(3, -3), t);
            float arc = Mathf.Sin(t * Mathf.PI) * 2;
            currentPos.y += arc;

            sonny.transform.position = currentPos;

            sonny.transform.Rotate(Vector3.forward * 720 * Time.deltaTime);
            hopTime += Time.deltaTime;
            yield return null;
        }
        mainCamera.state = CameraController.State.StayStill;
        sonnyShadow.SetActive(true);
        sonny.transform.rotation = new Quaternion(0, 0, 0, 0);
        SoundManager.instance.PlaySound(explosion);
        player.StopMoving(2);
        player.transform.localScale = new Vector3(1, 1, 1);

        float playerHopTime = 0;
        Vector2 playerStart = player.transform.position;
        playerShadow.SetActive(false);
        while (playerHopTime < 0.5)
        {
            float t = playerHopTime / 0.5f;
            Vector2 currentPos = Vector2.Lerp(playerStart, new Vector2(-1, -3), t);
            float arc = Mathf.Sin(t * Mathf.PI) * 2;
            //Debug.Log(arc);
            currentPos.y += arc;
            player.transform.position = currentPos;
            playerHopTime += Time.deltaTime;
            yield return null;
        }
        playerShadow.SetActive(true);
        yield return new WaitForSeconds(2);
        BigDialogue newDialogue = Instantiate(dialogue3);
        source.Play();
    }

    private IEnumerator Cutscene4()
    {
        mainCamera.state = CameraController.State.FollowPlayer;
        player.StopMoving(0);
        player.transform.localScale = new Vector3(-1, 1, 1);
        StartCoroutine(player.GoToPlace(new Vector2(-8.8f, -3f), 3));
        yield return new WaitForSeconds(3);
        player.transform.localScale = new Vector3(1, 1, 1);
        StartCoroutine(player.GoToPlace(new Vector2(-8.8f, -1.8f), 2));
        yield return new WaitForSeconds(3);
        BigDialogue newDialogue = Instantiate(dialogue4);
    }

    private IEnumerator Cutscene5()
    {
        //loader.transition.SetTrigger("start");
        fadeToBlack.GetComponent<Animator>().SetTrigger("black");
        yield return new WaitForSeconds(2);
        BigDialogue newDialogue = Instantiate(dialogue5);
    }

    private void StartSlidingGame()
    {
        StaticData.floor4Second = true;
        loader.LoadNextLevel("SlidingGame");
    }

    private IEnumerator Cutscene6()
    {
        yield return new WaitForSeconds(2);
        BigDialogue newDialogue = Instantiate(dialogue6);
    }
}
