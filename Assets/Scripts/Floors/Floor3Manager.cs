using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEditor.FilePathAttribute;

public class Floor3Manager : MonoBehaviour
{
    public Player player;
    public CameraController mainCamera;
    public LevelLoader loader;
    private int progress;
    public GameObject phoebe;
    public GameObject phoebeShadow;
    public AudioClip lightSound;
    public Light2D globalLight;
    public Light2D phoebeLight;
    public float spinSpeed;

    public SoloBigDialogue dialogue1;
    public SoloBigDialogue dialogue2;
    public BigDialogue dialogue3;
    public BigDialogue dialogue4;
    public BigDialogue dialogue5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        switch (StaticData.floor3Second)
        {
            case false:
                player.transform.position = new Vector2(-13.32f, -1.8f);
                //phoebe.SetActive(false);
                phoebeShadow.SetActive(false);
                phoebe.transform.position = new Vector2(-2.4f, 7.4f);
                break;
            case true:
                player.transform.position = new Vector2(-5.4f, -1.8f);
                phoebeShadow.SetActive(true);
                phoebeLight.enabled = false;
                phoebe.transform.position = new Vector2(-2.39f, -1.8f);
                mainCamera.transform.position = new Vector3(-5.4f, 0, -10);
                progress = 5;
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
                StartWheelGame();
                break;
            case 5:
                StartCoroutine(Cutscene5());
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
        StartCoroutine(player.GoToPlace(new Vector2(-5.4f, -1.8f), 5));
        yield return new WaitForSeconds(6);
        player.transform.localScale = new Vector3(-1, 1, 1);
        yield return new WaitForSeconds(1);
        player.transform.localScale = new Vector3(1, 1, 1);
        yield return new WaitForSeconds(1);

        SoloBigDialogue newDialogue = Instantiate(dialogue2);
    }

    private IEnumerator Cutscene3()
    {
        yield return new WaitForSeconds(0.5f);
        player.StopMoving(2);
        globalLight.intensity = 0.04f;
        SoundManager.instance.PlaySound(lightSound);
        yield return new WaitForSeconds(1f);

        phoebe.GetComponent<Animator>().SetBool("wand", true);
        float time = 0;
        Vector2 startingPos = phoebe.transform.position;
        while (time < 5)
        {
            //time += Time.deltaTime;
            //float t = 1.0f - Mathf.Exp(-dampSpeed * Time.deltaTime);
            //transform.position = Vector2.Lerp(transform.position, location, t);
            //yield return null;
            phoebe.transform.Rotate(Vector3.forward * spinSpeed * Time.deltaTime);

            float t = time / 5;
            phoebe.transform.position = Vector2.Lerp(startingPos, new Vector2(-2.39f,-1.8f), t);
            time += Time.deltaTime;
            yield return null;
        }
        phoebe.transform.rotation = new Quaternion(0, 0, 0, 0);
        phoebeShadow.SetActive(true);
        yield return new WaitForSeconds(1);
        phoebeLight.enabled = false;
        globalLight.intensity = 1;
        SoundManager.instance.PlaySound(lightSound);
        phoebe.GetComponent<Animator>().SetBool("wand", false);
        yield return new WaitForSeconds(1);
        player.StopMoving(0);
        BigDialogue newDialogue = Instantiate(dialogue3);
    }

    private IEnumerator Cutscene4()
    {
        yield return new WaitForSeconds(1);
        globalLight.intensity = 0;
        SoundManager.instance.PlaySound(lightSound);
        yield return new WaitForSeconds(1);
        BigDialogue newDialogue = Instantiate(dialogue4);
    }

    private void StartWheelGame()
    {
        progress++;
        StaticData.floor3Second = true;
        loader.LoadNextLevel("WheelGame");
    }

    private IEnumerator Cutscene5()
    {
        yield return new WaitForSeconds(1);
        BigDialogue newDialogue = Instantiate(dialogue5);
    }
}
