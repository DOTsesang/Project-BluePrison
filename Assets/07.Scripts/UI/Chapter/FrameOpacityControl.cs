using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FrameOpacityControl : MonoBehaviour
{
    [System.Serializable]
    public class RawImageSetting
    {
        public string name;
        public RawImage image;
        [Range(0f, 1f)]
        public float opacity = 1f;
    }

    [Header("RawImage Settings")]
    public List<RawImageSetting> rawImages = new List<RawImageSetting>();

    private Dictionary<string, RawImage> imageDict = new Dictionary<string, RawImage>();

    private int x = 0, y = 1;
    private int previousChapter;
    private int curChapter;

    void Awake(){
        InitializeRawImages();
    }
    private void Start()
    {
        curChapter = GetChapter();
        previousChapter = -1; // Set to an invalid chapter to ensure initial update
    }

    void Update()
    {
        UpdateChapterOnKeyDown();
    }

    private void InitializeRawImages()
    {
        imageDict.Clear();
        foreach (var setting in rawImages)
        {
            if (setting.image == null)
            {
                Debug.LogWarning($"RawImage '{setting.name}' is not assigned!");
                continue;
            }   

            imageDict[setting.name] = setting.image;
            SetOpacity(setting.name, setting.opacity);
        }
    }

    public void SetOpacity(string imageName, float opacity)
    {
        if (!imageDict.TryGetValue(imageName, out RawImage image))
        {
            Debug.LogWarning($"RawImage '{imageName}' not found!");
            return;
        }

        Color color = image.color;
        color.a = Mathf.Clamp01(opacity);
        image.color = color;

        var setting = rawImages.Find(s => s.name == imageName);
        if (setting != null)
        {
            setting.opacity = opacity;
        }
    }

    void UpdateChapterOnKeyDown()
    {
        bool chapterChanged = false;

        if (Input.GetKeyDown(KeyCode.A) && x != 0)
        {
            x = 0;
            chapterChanged = true;
        }
        if (Input.GetKeyDown(KeyCode.D) && x != 1)
        {
            x = 1;
            chapterChanged = true;
        }
        if (Input.GetKeyDown(KeyCode.S) && y != 0)
        {
            y = 0;
            chapterChanged = true;
        }
        if (Input.GetKeyDown(KeyCode.W) && y != 1)
        {
            y = 1;
            chapterChanged = true;
        }

        if (chapterChanged)
        {
            previousChapter = curChapter;
            curChapter = GetChapter();
            UpdateChapterOpacity();
            Debug.Log(" x " + x + " y " + y);
        }
    }

    public int GetChapter()
    {
        if (x == 0 && y == 0) return 3;
        if (x == 0 && y == 1) return 2;
        if (x == 1 && y == 1) return 1;
        if (x == 1 && y == 0) return 4;
        return 0; // Should never happen
    }

    void UpdateChapterOpacity()
    {
        if (curChapter != previousChapter)
        {
            if (previousChapter != -1) // Skip this for the initial update
            {
                UpdateOpacityByChapter(previousChapter, 0f);
            }
            UpdateOpacityByChapter(curChapter, 1f);
        }
    }

    void UpdateOpacityByChapter(int chapter, float opacity)
    {
        switch (chapter)
        {
            case 1:
                SetOpacity("chapter1", opacity);
                break;
            case 2:
                SetOpacity("chapter2", opacity);
                break;
            case 3:
                SetOpacity("chapter3", opacity);
                break;
            case 4:
                SetOpacity("chapter4", opacity);
                break;
            default:
                Debug.LogWarning($"Invalid chapter number: {chapter}");
                break;
        }
    }
}