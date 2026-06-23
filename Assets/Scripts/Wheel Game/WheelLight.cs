using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WheelLight : MonoBehaviour
{
    public Light2D spotLight;
    public float speed;
    public float targetAngle1;
    public float targetAngle2;
    private bool lightOn = false;
    public AudioClip click;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spotLight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        float wave = (Mathf.Sin(Time.time * speed) + 1.0f) / 2f;

        float currentAngle = Mathf.LerpAngle(targetAngle1, targetAngle2, wave);
        transform.rotation = Quaternion.Euler(0, 0, currentAngle);
    }

    public void TurnOnLight()
    {
        if (lightOn == false)
        {
            spotLight.enabled = true;
            SoundManager.instance.PlaySound(click);
            lightOn = true;
        }

    }
}
