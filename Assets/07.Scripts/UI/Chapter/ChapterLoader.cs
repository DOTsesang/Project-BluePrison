using UnityEngine;

public class ChapterLoader : MonoBehaviour
{
    public FrameOpacityControl chapterState;
    public LoadSceneToName sceneChanger;

    [Header("SceneNames")]
        public string chapter1;
        public string chapter2;
        public string chapter3;
        public string chapter4;
    
    void Update(){
        if(Input.GetKeyDown(KeyCode.E)){
            int chapter = chapterState.GetChapter();
            LoadChapter(chapter);
        }
    }

    void LoadChapter(int chapter){
        switch(chapter){
            case 1:
                sceneChanger.SceneName = chapter2;
                sceneChanger.OnLoadScene();
                break;
            case 2:
                sceneChanger.SceneName = chapter1;
                sceneChanger.OnLoadScene();
                break;
            case 3:
                sceneChanger.SceneName = chapter3;
                sceneChanger.OnLoadScene();
                break;
            case 4:
                sceneChanger.SceneName = chapter4;
                sceneChanger.OnLoadScene();
                break;
            default:
                Debug.Log("Invalid chapter number.");
                break;
        }
    }
    
}
