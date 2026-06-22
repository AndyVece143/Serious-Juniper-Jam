using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FallingObject : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public Rigidbody2D body;
    public float speed;
    public Animator anim;

    public float arcHeight;
    public float angle;
    public LaunchArea launchArea;

    public bool screenTime;
    public bool despawn = false;
    private float launchTimer = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        launchArea = LaunchArea.FindAnyObjectByType<LaunchArea>();
        AssignSupply();
        LaunchSupply();
    }

    // Update is called once per frame
    void Update()
    {
        if (screenTime == false && despawn == false)
        {
            transform.Rotate(Vector3.forward * 360 * Time.deltaTime);
        }


        if (screenTime == true)
        {
            transform.localScale += Vector3.one * 1f * Time.deltaTime;
            launchTimer -= Time.deltaTime;
            if (launchTimer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void LaunchTowardsScreen()
    {
        GetComponent<SpriteRenderer>().sortingOrder = 5;

        Vector2 startPos = transform.position;
        Vector2 targetPos = launchArea.transform.position;

        float displacementX = targetPos.x - startPos.x;

        float gravity = Physics2D.gravity.y * body.gravityScale;

        float timeToApex = Mathf.Sqrt(-2 * arcHeight / gravity);
        float timeToTarget = timeToApex + Mathf.Sqrt(2 * (targetPos.y - startPos.y - arcHeight) / gravity);

        float velocityX = displacementX / timeToTarget;
        float velocityY = Mathf.Sqrt(-2 * gravity * arcHeight);

        Vector2 forceToApply = new Vector2(velocityX, velocityY);
        body.linearVelocity = forceToApply;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            StartCoroutine(Despawning(1));
        }
    }

    private void AssignSupply()
    {
        int i = Random.Range(0, 6);

        anim.SetInteger("supply", i);

        if (i == 5)
        {
            gameObject.tag = "BadSupply";
        }
    }

    private void LaunchSupply()
    {
        int i = Random.Range(4, 9);
        Debug.Log(i);
        body.linearVelocity = new Vector2(-speed, i);
    }

    IEnumerator Despawning(float duration)
    {
        despawn = true;
        body.linearVelocity = new Vector2(0, 3);
        boxCollider.enabled = false;
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        Color startColor = renderer.color;
        //Debug.Log(startColor);
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0);
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            renderer.color = Color.Lerp(startColor, endColor, time / duration);
            yield return null;
        }

        Destroy(gameObject);
    }
}
