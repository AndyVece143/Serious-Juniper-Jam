using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public Player player;
    public string[] dialogue;
    public InspectBox inspectBox;

    public bool interactable;
    public BoxCollider2D boxCollider;
    public int react;
    public CameraController mainCamera;
    public bool checker = false;

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
            if (Input.GetKeyDown(KeyCode.Space) && interactable == true)
            {
                interactable = false;
                player.inspectIcon.enabled = false;
                mainCamera.state = CameraController.State.StayStill;
                player.StopMoving(react);

                InspectBox newInspectBox = Instantiate(inspectBox);
                newInspectBox.lines = dialogue;
                newInspectBox.interactableObject = this;
                checker = true;
            }
        }
    }
}
