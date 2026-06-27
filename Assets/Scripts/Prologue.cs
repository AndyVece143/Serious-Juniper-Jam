using System.Collections;
using UnityEngine;

public class Prologue : MonoBehaviour
{
    public SoloBigDialogue dialogue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Cutscene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Cutscene()
    {
        yield return new WaitForSeconds(1);
        SoloBigDialogue newDialogue = Instantiate(dialogue);
    }
}
