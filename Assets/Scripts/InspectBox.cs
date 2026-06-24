using System.Collections;
using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using static UnityEditor.Recorder.OutputPath;

public class InspectBox : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    private int index;

    public Player player;
    public Canvas canvas;
    public InteractableObject interactableObject;
    public GameObject textBox;
    private Vector3 textBoxPosition;
    private Vector3 textBoxEndPosition;
    public float duration;
    public AudioClip audioClip;
    private const string HTML_ALPHA = "<color=#00000000>";
    public bool ready = false;
    public CameraController mainCamera;
    public float dampSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        mainCamera = CameraController.FindAnyObjectByType<CameraController>();
        player = Player.FindAnyObjectByType<Player>();
        textComponent.text = string.Empty;
        SetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (ready == true)
            {
                NextLine();
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    void NextLine()
    {
        ready = false;
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            StartCoroutine(MoveSpriteEnd());
        }
    }

    void SetPosition()
    {
        if (player.transform.position.y > 0)
        {
            textBoxPosition = textBox.transform.position;
            textBox.transform.position = new Vector3(textBox.transform.position.x, textBox.transform.position.y - 5f, textBox.transform.position.z);
            textBoxEndPosition = textBox.transform.position;
            StartCoroutine(MoveSpriteBeginning());
        }

        else
        {
            textBox.transform.position = new Vector3(textBox.transform.position.x, textBox.transform.position.y + 5.56f, textBox.transform.position.z);
            //textBox.transform.position = new Vector3(textBox.transform.position.x, textBox.transform.position.y + 490.31f, textBox.transform.position.z);
            textBoxPosition = textBox.transform.position;
            textBox.transform.position = new Vector3(textBox.transform.position.x, textBox.transform.position.y + 5f, textBox.transform.position.z);
            textBoxEndPosition = textBox.transform.position;
            StartCoroutine(MoveSpriteBeginning());
        }
    }

    IEnumerator TypeLine()
    {
        int i = 4;
        string originalText = lines[index];
        string displayedText = "";
        int alphaIndex = 0;

        foreach (char c in lines[index].ToCharArray())
        {
            alphaIndex++;
            textComponent.text = originalText;
            displayedText = textComponent.text.Insert(alphaIndex, HTML_ALPHA);
            textComponent.text = displayedText;

            i++;
            if (i == 5)
            {
                SoundManager.instance.PlaySound(audioClip);
                i = 0;
            }
            yield return new WaitForSeconds(textSpeed);
        }
        ready = true;
    }

    IEnumerator MoveSpriteBeginning()
    {
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = 1.0f - Mathf.Exp(-dampSpeed * Time.deltaTime);
            textBox.transform.position = Vector3.Lerp(textBox.transform.position, textBoxPosition, t);
            yield return null;
        }
        StartDialogue();
    }

    IEnumerator MoveSpriteEnd()
    {
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = 1.0f - Mathf.Exp(-dampSpeed * Time.deltaTime);
            textBox.transform.position = Vector3.Lerp(textBox.transform.position, textBoxEndPosition, t);
            yield return null;
        }
        if (interactableObject)
        {
            interactableObject.interactable = true;
        }

        player.StartMoving();
        mainCamera.state = mainCamera.initialState;
        Destroy(gameObject);
    }
}
