using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private float smoothTime = 0.15f;
    private Vector3 velocity = Vector3.zero;

    public enum State
    {
        FollowPlayer,
        StayStill,
    }
    public State state;
    public State initialState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.FollowPlayer:
                FollowPlayer();
                break;
            case State.StayStill:
                break;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    private void FollowPlayer()
    {
        Vector3 targetPosition = player.position + offset;
        targetPosition.y = 0;

        if (targetPosition.x < -7.5f)
        {
            targetPosition.x = -7.5f;
        }
        if (targetPosition.x > 7.5f)
        {
            targetPosition.x = 7.5f;
        }

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    public IEnumerator GoToPlace(Vector3 location, float duration)
    {
        state = State.StayStill;
        float time = 0;
        Vector3 startingPos = transform.position;

        while (time < duration)
        {
            //time += Time.deltaTime;
            //float t = 1.0f - Mathf.Exp(-dampSpeed * Time.deltaTime);
            //transform.position = Vector2.Lerp(transform.position, location, t);
            //yield return null;

            float t = time / duration;
            transform.position = Vector3.Lerp(startingPos, location, t);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = location;
    }
}
