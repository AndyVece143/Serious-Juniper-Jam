using UnityEngine;

public class MinigameSelect : MonoBehaviour
{
    public LevelLoader loader;

    public void GoToCatch()
    {
        loader.LoadNextLevel("CatchingGame");
    }

    public void GoToWheel()
    {
        loader.LoadNextLevel("WheelGame");
    }

    public void GoToSlide()
    {
        loader.LoadNextLevel("SlidingGame");
    }

    public void GoToTitle()
    {
        loader.LoadNextLevel("Title");
    }
}
