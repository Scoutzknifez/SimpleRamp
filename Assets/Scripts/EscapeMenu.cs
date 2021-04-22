using UnityEngine;
using UnityEngine.UI;

public class EscapeMenu : MonoBehaviour
{
    public static bool inEscapeMenu = false;

    public GameObject canvasManagerParent = null;
    public GameObject escapeMenu = null;
    public Text timeKeeper = null;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            updateDisplay();
        }
    }

    public void updateDisplay()
    {
        inEscapeMenu = !inEscapeMenu;

        escapeMenu.SetActive(inEscapeMenu);
        timeKeeper.gameObject.SetActive(!inEscapeMenu);

        if (inEscapeMenu)
        {
            canvasManagerParent.GetComponent<CanvasManager>().goToMainMenu();
        }

        Cursor.lockState = inEscapeMenu ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = inEscapeMenu ? true : false;
    }
}
