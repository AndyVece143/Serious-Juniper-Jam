using System.Collections;
using TMPro;
using UnityEngine;

public class SoloBigDialogue : MonoBehaviour
{
    public TextMeshProUGUI mainText;
    public TextMeshProUGUI nameText;
    public string[] lines;
    public float textSpeed;
    private int index;
    public int[] emotionChanges;
    public Canvas canvas;

    public Portrait character1;
    public float moveDuration;

    private Vector3 character1Position;
    public GameObject textBox;
    private Vector3 textBoxPosition;
    private Vector3 character1EndPosition;
    private Vector3 textBoxEndPosition;
    //public CameraController mainCamera;

    private const string HTML_ALPHA = "<color=#00000000>";
    public bool ready = false;
    public AudioClip audioClip;
    private bool ending = false;
    public float dampSpeed;

    public bool sceneTransition;
    public string sceneName;
    public LevelLoader loader;
    public CameraController mainCamera;

    public bool level2;
    private Floor2Manager floor2Manager;

    public bool level3;
    private Floor3Manager floor3Manager;

    public bool level4;
    private Floor4Manager floor4Manager;

    public bool juniper;
    private JuniperManager juniperManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainText.text = string.Empty;
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        mainText.text = string.Empty;
        mainCamera = CameraController.FindAnyObjectByType<CameraController>();
        mainCamera.state = CameraController.State.StayStill;

        nameText.text = "Gloria";

        if (sceneTransition)
        {
            loader = LevelLoader.FindAnyObjectByType<LevelLoader>();
        }

        if (level2)
        {
            floor2Manager = Floor2Manager.FindAnyObjectByType<Floor2Manager>();
        }

        if (level3)
        {
            floor3Manager = Floor3Manager.FindAnyObjectByType<Floor3Manager>();
        }

        if (level4)
        {
            floor4Manager = Floor4Manager.FindAnyObjectByType<Floor4Manager>();
        }
        if (juniper)
        {
            juniperManager = JuniperManager.FindAnyObjectByType<JuniperManager>();
        }
        BeginningSprite();
        SetPositions();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if (ready == true)
            {
                NextLine();
            }
        }

        if (Input.GetKeyDown(KeyCode.L) && ending == false)
        {
            StopAllCoroutines();
            StartCoroutine(MoveSpritesEnd());
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
            ChangeEmotion();
            mainText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            StartCoroutine(MoveSpritesEnd());
        }
    }

    void BeginningSprite()
    {
        ChangeEmotion();
    }

    void ChangeEmotion()
    {
        character1.ChangeEmotion(emotionChanges[index]);
    }

    void SetPositions()
    {
        character1Position = character1.transform.position;
        textBoxPosition = textBox.transform.position;

        character1.transform.position = new Vector3(character1Position.x - 14f, character1Position.y, character1Position.z);
        textBox.transform.position = new Vector3(textBoxPosition.x, textBoxPosition.y - 6, textBoxPosition.z);

        character1EndPosition = character1.transform.position;
        textBoxEndPosition = textBox.transform.position;

        StartCoroutine(MoveSpritesBeginning());
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
            mainText.text = originalText;
            displayedText = mainText.text.Insert(alphaIndex, HTML_ALPHA);
            mainText.text = displayedText;

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

    IEnumerator MoveSpritesBeginning()
    {
        float time = 0;
        while (time < moveDuration)
        {
            time += Time.deltaTime;
            float t = 1.0f - Mathf.Exp(-dampSpeed * Time.deltaTime);
            //character1.gameObject.transform.position = Vector3.Lerp(character1.gameObject.transform.position, character1Position, time / moveDuration);
            //textBox.transform.position = Vector3.Lerp(textBox.transform.position, textBoxPosition, time / moveDuration);
            character1.gameObject.transform.position = Vector3.Lerp(character1.gameObject.transform.position, character1Position, t);
            textBox.transform.position = Vector3.Lerp(textBox.transform.position, textBoxPosition, t);
            yield return null;
        }
        StartDialogue();
    }

    IEnumerator MoveSpritesEnd()
    {
        ending = true;
        float time = 0;
        while (time < moveDuration)
        {
            time += Time.deltaTime;
            float t = 1.0f - Mathf.Exp(-dampSpeed * Time.deltaTime);
            character1.gameObject.transform.position = Vector3.Lerp(character1.gameObject.transform.position, character1EndPosition, t);
            textBox.transform.position = Vector3.Lerp(textBox.transform.position, textBoxEndPosition, t);
            yield return null;
        }
        if (sceneTransition)
        {
            loader.LoadNextLevel(sceneName);
        }

        if (level2)
        {
            floor2Manager.StartCutscene();
        }

        if (level3)
        {
            floor3Manager.StartCutscene();
        }
        if (level4)
        {
            floor4Manager.StartCutscene();
        }
        if (juniper)
        {
            juniperManager.StartCutscene();
        }

        mainCamera.state = mainCamera.initialState;

        Destroy(gameObject);
        //player.state = Player.State.Standard;
        //mainCamera.state = CameraController.State.FollowPlayer;
        //mainCamera.anim.enabled = false;
    }
}
