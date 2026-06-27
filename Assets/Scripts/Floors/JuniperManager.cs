using System.Collections;
using UnityEngine;

public class JuniperManager : MonoBehaviour
{
    public Player player;
    public LevelLoader loader;
    private int progress;
    public GameObject juniper;
    public SpriteRenderer juniperRenderer;
    public Color blackColor;
    public Color whiteColor;

    public SoloBigDialogue dialogue1;
    public BigDialogue dialogue2;
    public AudioSource source;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        juniperRenderer.color = blackColor;
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
                FinishTheGame();
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
        StartCoroutine(player.GoToPlace(new Vector2(2.5f, -1.8f), 6));
        yield return new WaitForSeconds(7);

        float time = 0;
        while (time < 3)
        {
            time += Time.deltaTime;
            float t = time / 3;
            juniperRenderer.color = Color.Lerp(blackColor, whiteColor, t);

            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        BigDialogue newDialogue = Instantiate(dialogue2);
        source.Play();
    }

    private void FinishTheGame()
    {
        loader.LoadNextLevel("Epilogue");
    }
}
