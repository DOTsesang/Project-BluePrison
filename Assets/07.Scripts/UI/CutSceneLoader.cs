using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;

public class CutSceneLoader : MonoBehaviour
{
    public RawImage screen;
    public VideoPlayer vp;
    public LoadSceneToName sceneLoader;
    private bool isPlayed = false;
    [Header("Screen")]
    public float fadeDuration = 2f;

    public void LoadScreen()
    {
        screen.rectTransform.anchoredPosition = new Vector2(0, 0);
        PlayCutScene();
    }
    void PlayCutScene()
    {
        vp.Play();
        Debug.Log("video play");
        vp.loopPointReached += endVideo;

    }

    void endVideo(UnityEngine.Video.VideoPlayer vp)
    {
        if (sceneLoader == null)
        {
            screen.rectTransform.anchoredPosition = new Vector2(4000, 0);
        }
        else
        {
            sceneLoader.OnLoadScene();
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player") && !isPlayed)
        {
            LoadScreen();
            isPlayed = true;
        }
    }

}
