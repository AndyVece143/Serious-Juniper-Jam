using UnityEngine;
using UnityEngine.UI;

public class SlidingPlayer : MonoBehaviour
{
    public float speed;
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    public Animator anim;
    public float spinSpeed = 1.0f;

    public enum State
    {
        Standard,
        Win,
        NoMove,
    }
    public State state;

    public Slider slider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Standard:
                break;
            case State.NoMove:
                break;

        }

        anim.speed = spinSpeed;
        slider.value = spinSpeed;
    }

    private void FixedUpdate()
    {
        if (state == State.Standard)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            body.AddForce(new Vector2((horizontalInput * speed * 4), 0f), ForceMode2D.Force);
            body.AddForce(new Vector2(0f, (verticalInput * speed * 4)), ForceMode2D.Force);

            body.linearVelocity = new Vector2(Mathf.Clamp(body.linearVelocity.x, -speed, speed), (Mathf.Clamp(body.linearVelocity.y, -speed, speed)));
        }

    }

    private void Movement()
    {

    }

    public void SlowSpin()
    {
        spinSpeed -= 0.1f;
        speed -= 0.3f;
    }

    public void StopSpinning()
    {
        body.linearVelocity = new Vector2(0, 0);
        anim.SetTrigger("still");
        state = State.NoMove;
        spinSpeed = 0;
    }
}
