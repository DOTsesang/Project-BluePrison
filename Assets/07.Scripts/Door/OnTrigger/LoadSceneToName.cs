using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadSceneToName: MonoBehaviour
{
    public string SceneName;
    public void OnLoadScene(){
        SceneManager.LoadScene(SceneName);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
