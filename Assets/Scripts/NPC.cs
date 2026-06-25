using UnityEngine;

public class NPC : MonoBehaviour
{
    public BigDialogue dialogue;
    public Player player;
    public BoxCollider2D boxCollider;
    public bool interactable;

    public bool checker = false;
    public CameraController mainCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = Player.FindAnyObjectByType<Player>();
        boxCollider = GetComponent<BoxCollider2D>();
        mainCamera = CameraController.FindAnyObjectByType<CameraController>();
        interactable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (boxCollider.IsTouching(player.boxCollider))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (player.state != Player.State.NoMove)
                {
                    player.StopMoving(1);
                    player.talkIcon.enabled = false;
                    mainCamera.state = CameraController.State.StayStill;
                    BigDialogue newDialogue = Instantiate(dialogue);
                    checker = true;
                }
            }
        }
    }
}
