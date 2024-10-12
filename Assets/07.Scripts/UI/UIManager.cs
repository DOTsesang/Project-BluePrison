using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private List<Canvas> canvasList = new List<Canvas>();

    void Start(){
        ShowIntroUI();
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            ShowIntroUI();
        }
    }
    public void ShowIntroUI(){
        foreach (Canvas canvas in canvasList)
        {   
            canvas.gameObject.SetActive(false);
            if(canvas.gameObject.name == "IntroUICanvas"){
                canvas.gameObject.SetActive(true);
            }
        }
    }
    public void ShowChapterUI()
    {   
        foreach (Canvas canvas in canvasList)
        {   
            canvas.gameObject.SetActive(false);
            if(canvas.gameObject.name == "ChapterUICanvas"){
                canvas.gameObject.SetActive(true);
            }
        }
    }

}
