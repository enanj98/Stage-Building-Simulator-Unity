using UnityEngine;


public class ToggleHUD : MonoBehaviour
{
    public GameObject hud;
    public GameObject colorPickerHud;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("Tab key was pressed."); 
            hud.SetActive(!hud.activeSelf); 
            Debug.Log("HUD active state is now: " + hud.activeSelf); 
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("C key was pressed.");
            colorPickerHud.SetActive(!colorPickerHud.activeSelf);
            Debug.Log("HUD active state is now: " + colorPickerHud.activeSelf);
        }
    }
}