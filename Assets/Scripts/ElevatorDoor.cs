using UnityEngine;

public class ElevatorDoor : MonoBehaviour
{
    public Player player;
    public BoxCollider2D boxCollider;
    public bool interactable;
    public SoloBigDialogue dialogue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = Player.FindAnyObjectByType<Player>();
        boxCollider = GetComponent<BoxCollider2D>();
        interactable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (boxCollider.IsTouching(player.boxCollider))
        {
            if (Input.GetKeyDown(KeyCode.Space) && interactable == true && player.state != Player.State.NoMove)
            {
                interactable = false;
                player.StopMoving(1);
                player.goIcon.enabled = false;
                SoloBigDialogue newSoloDialogue = Instantiate(dialogue);
            }
        }
    }
}
