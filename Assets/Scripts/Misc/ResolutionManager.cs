using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    public int width;
    public int height;
    public bool isFullScreen = false;

    public void SetWidth(int newWidth)
    {
        width = newWidth;
    }

    public void SetHeight(int newHeight)
    {
        height = newHeight;
    }

    public void toggleFullScreen()
    {
        isFullScreen = !isFullScreen;
    }

    public void SetRes()
    {
        Screen.SetResolution(width, height, isFullScreen);
    }
}
