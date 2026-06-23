using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

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

    public TextMeshProUGUI energyText;
    public TextMeshProUGUI staminaText;
    public bool recovering = false;

    public WheelPlayer player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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


        wheel.transform.Rotate(Vector3.forward * -spinSpeed * Time.deltaTime);

        spinSpeed -= 60 * Time.deltaTime;

        if (spinSpeed < 0)
        {
            spinSpeed = 0;
        }

        energy += (spinSpeed / 1000);
        energyText.text = "" + energy;
        staminaText.text = "" + stamina;

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

        if (energy > 1500)
        {
            light3.TurnOnLight();
            globalLight.intensity = 1;
        }
    }

    void StaminaCheck()
    {
        if (spinSpeed > 90)
        {
            stamina -= (0.5f * Time.deltaTime);
        }
        if (spinSpeed > 150)
        {
            stamina -= Time.deltaTime;
        }

        if (spinSpeed > 210)
        {
            stamina -= (2 *Time.deltaTime);
        }

        if (spinSpeed <= 50)
        {
            stamina += Time.deltaTime;
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
}
