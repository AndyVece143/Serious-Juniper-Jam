using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class WheelManager : MonoBehaviour
{
    public GameObject wheel;
    public float spinSpeed;
    public WheelLight light1;
    public WheelLight light2;
    public WheelLight light3;
    public Light2D globalLight;
    public float energy;
    public float stamina;

    //public TextMeshProUGUI energyText;
    //public TextMeshProUGUI staminaText;
    public Slider energySlider;
    public Slider staminaSlider;

    public bool recovering = false;

    public WheelPlayer player;

    public MinigameTextBox minigameTextBox;
    public MinigameTextBox endingBox;
    private bool gameStarted = false;
    public LevelLoader loader;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MinigameTextBox newTextBox = Instantiate(minigameTextBox);
        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted == true)
        {
            if (recovering == false)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    spinSpeed += 25;

                    if (spinSpeed > 250)
                    {
                        spinSpeed = 250;
                    }
                }
            }
        }

        wheel.transform.Rotate(Vector3.forward * -spinSpeed * Time.deltaTime);

        spinSpeed -= 60 * Time.deltaTime;

        if (spinSpeed < 0)
        {
            spinSpeed = 0;
        }

        energy += ((spinSpeed / 5) * Time.deltaTime);
        //energyText.text = "" + energy;
        //staminaText.text = "" + stamina;
        energySlider.value = energy;
        staminaSlider.value = stamina;

        LightChecker();
        StaminaCheck();
    }

    private void LightChecker()
    {
        if (energy > 500)
        {
            light1.TurnOnLight();
        }

        if (energy > 1000)
        {
            light2.TurnOnLight();
        }

        if (energy > 1500 && gameStarted == true)
        {
            light3.TurnOnLight();
            globalLight.intensity = 1;
            StartCoroutine(EndGame());

            gameStarted = false;
        }
    }

    void StaminaCheck()
    {
        if (spinSpeed > 130)
        {
            stamina -= (0.5f * Time.deltaTime);
        }
        if (spinSpeed > 180)
        {
            stamina -= Time.deltaTime;
        }

        if (spinSpeed > 230)
        {
            stamina -= (2 *Time.deltaTime);
        }

        if (spinSpeed <= 100)
        {
            stamina += (2 * Time.deltaTime);
        }

        if (stamina <= 0 && recovering == false)
        {
            stamina = 0;
            recovering = true;
            StartCoroutine(RecoverStamina());
        }
        if (stamina <= 0)
        {
            stamina = 0;
        }

        if (stamina >= 20)
        {
            stamina = 20;
        }

        if (recovering)
        {
            stamina = 0;
        }
    }

    IEnumerator RecoverStamina()
    {
        player.GetComponent<SpriteRenderer>().color = Color.lightBlue;
        yield return new WaitForSeconds(10);
        stamina = 20;
        recovering = false;
        player.GetComponent<SpriteRenderer>().color = Color.white;
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(4);
        gameStarted = true;
    }

    IEnumerator EndGame()
    {
        MinigameTextBox ending = Instantiate(endingBox);
        yield return new WaitForSeconds(4);

        if (StaticData.storyMode == true)
        {
            loader.LoadNextLevel("Floor3");
        }
    }
}
