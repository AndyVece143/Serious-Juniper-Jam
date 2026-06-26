using System.Collections;
using UnityEngine;

public class StopItem : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public Vector2 originalPosition;
    public float dampSpeed;
    private Color fullColor;
    private Color transparent;
    private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalPosition = transform.position;
        boxCollider.enabled = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        fullColor = spriteRenderer.color;
        transparent = new Color(fullColor.r, fullColor.g, fullColor.b, 0);
        StartCoroutine(SpawnIn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnIn()
    {
        transform.position = new Vector2(originalPosition.x, originalPosition.y + 4);
        Debug.Log(transform.position);
        spriteRenderer.color = transparent;

        float time = 0;
        while (time < 0.5f)
        {
            time += Time.deltaTime;
            spriteRenderer.color = Color.Lerp(transparent, fullColor, time / 0.5f);

            float t = 1.0f - Mathf.Exp(-dampSpeed * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, originalPosition, t);
            yield return null;
        }
        boxCollider.enabled = true;
    }
}
