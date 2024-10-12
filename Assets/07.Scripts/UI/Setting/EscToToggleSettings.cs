using UnityEngine;

public class EscToToggleSettings : MonoBehaviour
{
    public Canvas settingsCanvas;

    private bool isActive = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            settingsCanvas.gameObject.SetActive(!isActive);
            isActive = !isActive;
        }
    }
    public void onsettings(){
         settingsCanvas.gameObject.SetActive(!isActive);
            isActive = !isActive;
    }
}
