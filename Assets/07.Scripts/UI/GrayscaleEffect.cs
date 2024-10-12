using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(PostProcessVolume))]
public class GrayscaleEffect : MonoBehaviour
{
    private PostProcessVolume postProcessVolume;
    private ColorGrading colorGrading;

    void Awake()
    {
        // PostProcessVolume 컴포넌트를 가져오거나 생성합니다
        postProcessVolume = GetComponent<PostProcessVolume>();
        if (postProcessVolume == null)
        {
            postProcessVolume = gameObject.AddComponent<PostProcessVolume>();
        }

        // 새로운 PostProcessProfile을 생성합니다
        postProcessVolume.profile = ScriptableObject.CreateInstance<PostProcessProfile>();

        // ColorGrading 효과를 추가합니다
        colorGrading = postProcessVolume.profile.AddSettings<ColorGrading>();

        // 초기 설정
        postProcessVolume.isGlobal = true;
        colorGrading.enabled.Override(true);
        colorGrading.saturation.Override(0f);
    }

    public void EnableGrayscale()
    {
        if (colorGrading != null)
        {
            colorGrading.enabled.Override(true);
            colorGrading.saturation.Override(-100f);
            Debug.Log("Grayscale effect enabled");
        }
        else
        {
            Debug.LogError("ColorGrading effect is not available!");
        }
    }

    public void DisableGrayscale()
    {
        if (colorGrading != null)
        {
            colorGrading.saturation.Override(0f);
            Debug.Log("Grayscale effect disabled");
        }
        else
        {
            Debug.LogError("ColorGrading effect is not available!");
        }
    }

    // 테스트를 위한 Update 메서드
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            EnableGrayscale();
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            DisableGrayscale();
        }
    }
}