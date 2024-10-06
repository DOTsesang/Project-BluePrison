using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GIFAnimator : MonoBehaviour
{
    public Sprite[] frames;
    public float framesPerSecond = 30f;
    public RawImage[] childImages; // 자식 이미지들을 저장할 배열
    public float fadeInDuration = 1f; // 페이드 인 지속 시간

    private Image image;
    private bool isPlaying = false;

    void Start()
    {
        image = GetComponent<Image>();
        image.enabled = false; // 시작 시 이미지 비활성화
        // 자식 이미지들 초기화
        foreach (var childImage in childImages)
        {
            Color c = childImage.color;
            c.a = 0f; // 알파값을 0으로 설정
            childImage.color = c;
        }
    }

    public void PlayAnimation()
    {
        if (!isPlaying)
        {
            StartCoroutine(AnimateCoroutine());
        }
    }

    private IEnumerator AnimateCoroutine()
    {
        isPlaying = true;
        image.enabled = true;

        for (int i = 0; i < frames.Length; i++)
        {
            image.sprite = frames[i];
            yield return new WaitForSeconds(1f / framesPerSecond);
        }

        isPlaying = false;
        // 애니메이션 종료 후 자식 이미지들 페이드 인
        StartCoroutine(FadeInChildImages());
    }

    private IEnumerator FadeInChildImages()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeInDuration);
            foreach (var childImage in childImages)
            {
                Color c = childImage.color;
                c.a = alpha;
                childImage.color = c;
            }
            yield return null;
        }
    }

    // 애니메이션을 중지하고 이미지를 숨기는 메서드
    public void HideAnimation()
    {
        StopAllCoroutines();
        isPlaying = false;
        image.enabled = false;
        // 자식 이미지들도 숨김
        foreach (var childImage in childImages)
        {
            Color c = childImage.color;
            c.a = 0f;
            childImage.color = c;
        }
    }
}