using UnityEngine;
using UnityEngine.UI;

public class Portrait : MonoBehaviour
{
    public bool isActiveSpeaker;
    public Image image;
    public Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<Image>();
    }

    public void ChangeEmotion(int i)
    {
        anim.SetInteger("emotion", i);
    }
}
