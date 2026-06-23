using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    private Rigidbody2D body;
    public BoxCollider2D boxCollider;
    public Animator anim;
    public enum State
    {
        Standard,
        NoMove,
    }
    public State state;
    public float dampSpeed;

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

        body.linearVelocity = new Vector2(horizontalInput * speed, verticalInput * speed);

        //Flip Sprite
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }

        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        anim.SetBool("move", horizontalInput != 0 || verticalInput != 0);
    }

    public void StopMoving(int react)
    {
        body.linearVelocity = new Vector2(0, 0);
        state = State.NoMove;
        anim.SetInteger("react", react);
    }

    public void StartMoving()
    {
        state = State.Standard;
        anim.SetInteger("react", 0);
    }

    public IEnumerator GoToPlace(Vector2 location, float duration)
    {
        float time = 0;
        Vector2 startingPos = transform.position;

        anim.SetBool("move", true);
        while (time < duration)
        {
            //time += Time.deltaTime;
            //float t = 1.0f - Mathf.Exp(-dampSpeed * Time.deltaTime);
            //transform.position = Vector2.Lerp(transform.position, location, t);
            //yield return null;

            float t = time / duration;
            transform.position = Vector2.Lerp(startingPos, location, t);
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = location;

        body.linearVelocity = new Vector2(0, 0);
        anim.SetBool("move", false);
    }
}
