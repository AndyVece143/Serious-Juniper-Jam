using UnityEngine;

public class WheelPlayer : MonoBehaviour
{
    public WheelManager manager;
    public Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("move", manager.spinSpeed != 0);
        anim.speed = manager.spinSpeed / 50;

        if (manager.spinSpeed == 0)
        {
            anim.speed = 1;
        }
    }
}
