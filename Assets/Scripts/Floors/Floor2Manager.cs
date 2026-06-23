using System.Collections;
using UnityEngine;

public class Floor2Manager : MonoBehaviour
{
    public Player player;
    public CameraController mainCamera;
    public SoloBigDialogue dialogue1;
    public BigDialogue dialogue2;
    private int progress;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
    }
}
