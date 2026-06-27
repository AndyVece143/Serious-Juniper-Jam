using UnityEngine;

public class CatchingPlayer : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public Rigidbody2D body;
    public float speed;
    public Animator anim;

    public enum State
    {
        Standard,
        HitStun,
        NoMove,
    }
    public State state;

    public CatchingManager manager;
    [SerializeField] private LayerMask groundLayer;
    public float hitStunTime;
    public AudioClip hurt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        //state = State.NoMove;
        anim.SetBool("bucket", true);
        state = State.NoMove;
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
            case State.HitStun:
                HitStun();
                break;
        }
    }

    private void Movement()
    {
        hitStunTime = 0;
        float horizontalInput = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        anim.SetBool("move", horizontalInput != 0);
    }

    private void KnockBack()
    {
        body.linearVelocity = new Vector2(-3f, 5f);
        state = State.HitStun;
    }

    private void HitStun()
    {
        hitStunTime += Time.deltaTime;
        anim.SetBool("hurt", true);

        if (IsGrounded() && hitStunTime >= 0.2f)
        {
            anim.SetBool("hurt", false);
            state = State.Standard;
        }
    }

    public void StopMoving()
    {
        body.linearVelocity = new Vector2(0, 0);
        anim.SetBool("move", false);
        state = State.NoMove;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Supply")
    //    {
    //        manager.IncreaseScore();
    //        Destroy(collision.gameObject);
    //    }
    //}

    public bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Supply")
        {
            manager.IncreaseScore();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "BadSupply")
        {
            SoundManager.instance.PlaySound(hurt);
            KnockBack();
            collision.gameObject.GetComponent<FallingObject>().boxCollider.enabled = false;
            collision.gameObject.GetComponent<FallingObject>().screenTime = true;
            collision.gameObject.GetComponent<FallingObject>().LaunchTowardsScreen();
            //Destroy(collision.gameObject);
        }
    }
}
