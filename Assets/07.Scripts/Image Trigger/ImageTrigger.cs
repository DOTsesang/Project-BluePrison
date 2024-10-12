using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageTrigger : MonoBehaviour
{
    public RawImage targetImage;  // RawImage UI
    public float fadeDuration = 1.0f;  // 서서히 나타나고 사라지는 시간
    private bool isFading = false;  // 현재 페이드 중인지 여부

    private void Start()
    {
        // 시작 시 이미지를 투명하게 설정
        SetImageAlpha(0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어가 트리거 박스에 들어오면 이미지를 서서히 나타냄
        if (other.CompareTag("Player") && !isFading)
        {
            StartCoroutine(FadeImage(1f));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 플레이어가 트리거 박스를 벗어나면 이미지를 서서히 사라지게 함
        if (other.CompareTag("Player") && !isFading)
        {
            StartCoroutine(FadeImage(0f));
        }
    }

    // RawImage의 알파값을 조정하는 함수
    private void SetImageAlpha(float alpha)
    {
        Color color = targetImage.color;
        color.a = alpha;
        targetImage.color = color;
    }

    // 이미지를 서서히 나타나거나 사라지게 하는 코루틴 함수
    private IEnumerator FadeImage(float targetAlpha)
    {
        isFading = true;
        float startAlpha = targetImage.color.a;
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, timeElapsed / fadeDuration);
            SetImageAlpha(alpha);
            yield return null;
        }

        SetImageAlpha(targetAlpha);
        isFading = false;
    }
}
