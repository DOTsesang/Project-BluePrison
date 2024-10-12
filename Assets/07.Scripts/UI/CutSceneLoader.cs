using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class CutSceneLoader : MonoBehaviour
{
    public RawImage screen;
    public VideoPlayer vp;
    public LoadSceneToName sceneLoader;
    
    public void LoadScreen(){
        screen.rectTransform.anchoredPosition = new Vector2(0, 0);
        PlayCutScene();
    }
    void PlayCutScene(){
        vp.Play();
        vp.loopPointReached += endVideo;
        
    }

    void endVideo(UnityEngine.Video.VideoPlayer vp){
        if(sceneLoader == null){
            screen.rectTransform.anchoredPosition = new Vector2(4000, 0);      
        }
        else{
            sceneLoader.OnLoadScene();
        }
    }

    void OnTriggerEnter(Collider collider){
        if(collider.gameObject.CompareTag("Player")){
            LoadScreen();
        }
    }
  
}
