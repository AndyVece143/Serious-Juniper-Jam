using System.Collections;
using TMPro;
using UnityEngine;

public class MinigameTextBox : MonoBehaviour
{
    public Canvas canvas;
    public float duration;

    public GameObject textBox;
    private Vector3 textBoxPosition;
    private Vector3 textBoxEndPosition;
    public float distance;
    public float dampSpeed;
    private bool rotating = true;
    public bool bigRotation;
    public TextMeshProUGUI text;
    private float hue = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        SetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (rotating == true)
        {
            switch (bigRotation)
            {
                case true:
                    textBox.transform.Rotate(0, 180 * Time.deltaTime, 0);
                    hue += Time.deltaTime;

                    if (hue > 1)
                    {
                        hue -= 1;
                    }

                    Color rainbowColor = Color.HSVToRGB(hue, 1, 1);
                    text.color = rainbowColor;

                        break;
                    
                case false:
                    textBox.transform.Rotate(0, 360 * Time.deltaTime, 0);
                    break;
            }
        }

    }

    void SetPosition()
    {
        textBoxPosition = textBox.transform.position;

        textBox.transform.position = new Vector2(textBox.transform.position.x + distance, textBox.transform.position.y);
        textBoxEndPosition = textBox.transform.position;
        StartCoroutine(MoveSpriteBeginning());
    }

    IEnumerator MoveSpriteBeginning()
    {
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = 1.0f - Mathf.Exp(-dampSpeed * Time.deltaTime);

            textBox.transform.position = Vector2.Lerp(textBox.transform.position, textBoxPosition, t);
            yield return null;
        }

        if (bigRotation == false)
        {
            rotating = false;
            textBox.transform.rotation = new Quaternion(0, 0, 0, 0);
        }


        yield return new WaitForSeconds(2);
        rotating = true;
        StartCoroutine(MoveSpriteEnd());
    }

    IEnumerator MoveSpriteEnd()
    {
        rotating = true;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = 1.0f - Mathf.Exp(-dampSpeed * Time.deltaTime);

            textBox.transform.position = Vector2.Lerp(textBox.transform.position, textBoxEndPosition, t);
            yield return null;
        }
        Destroy(gameObject);
    }
}
