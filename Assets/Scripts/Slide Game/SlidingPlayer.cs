using UnityEngine;

public class SlidingPlayer : MonoBehaviour
{
    public float speed;
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    public Animator anim;

    public enum State
    {
        Standard,
        Win,
        NoMove,
    }
    public State state;

    public CatchingManager manager;

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
                Movement();
                break;
            case State.NoMove:
                break;

        }
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        body.AddForce(new Vector2(horizontalInput * speed * 1, 0f), ForceMode2D.Force);
        body.AddForce(new Vector2(0f, verticalInput * speed * 1), ForceMode2D.Force);

        body.linearVelocity = new Vector2(Mathf.Clamp(body.linearVelocity.x, -speed, speed), (Mathf.Clamp(body.linearVelocity.y, -speed, speed)));
    }
}
