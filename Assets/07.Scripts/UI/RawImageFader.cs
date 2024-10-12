using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RawImageFader : MonoBehaviour
{
    [Header("Target")]
    [Tooltip("The RawImage to apply the fade effect. If not set, it will try to get the component from this GameObject.")]
    public RawImage targetImage;

    [Header("Fade Settings")]
    [Tooltip("Duration of each fade (in seconds)")]
    [Range(0.1f, 5f)]
    public float fadeDuration = 1f;

    [Tooltip("Delay between fades (in seconds)")]
    [Range(0f, 3f)]
    public float delayBetweenFades = 0.5f;

    [Tooltip("Minimum alpha value (0 = fully transparent)")]
    [Range(0f, 1f)]
    public float minAlpha = 0.2f;

    [Tooltip("Maximum alpha value (1 = fully opaque)")]
    [Range(0f, 1f)]
    public float maxAlpha = 1f;

    [Header("Fade Behavior")]
    [Tooltip("If true, the fading will start automatically on Start")]
    public bool autoStart = true;

    [Tooltip("Number of times to repeat the fade cycle. Set to -1 for infinite loop.")]
    public int repeatCount = -1;

    private Coroutine fadeCoroutine;

    private void Start()
    {
        if (targetImage == null)
        {
            targetImage = GetComponent<RawImage>();
        }

        if (targetImage == null)
        {
            Debug.LogError("No RawImage component found!");
            return;
        }

        // Ensure minAlpha is always less than maxAlpha
        minAlpha = Mathf.Min(minAlpha, maxAlpha - 0.1f);

        if (autoStart)
        {
            StartFading();
        }
    }

    public void StartFading()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeLoop());
    }

    public void StopFading()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
            fadeCoroutine = null;
        }
    }

    private IEnumerator FadeLoop()
    {
        int currentRepeat = 0;
        while (repeatCount == -1 || currentRepeat < repeatCount)
        {
            yield return StartCoroutine(FadeImage(minAlpha, maxAlpha)); // 페이드 인
            yield return new WaitForSeconds(delayBetweenFades);
            yield return StartCoroutine(FadeImage(maxAlpha, minAlpha)); // 페이드 아웃
            yield return new WaitForSeconds(delayBetweenFades);

            if (repeatCount != -1)
            {
                currentRepeat++;
            }
        }
    }

    private IEnumerator FadeImage(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        Color currentColor = targetImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            targetImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
            yield return null;
        }

        targetImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, endAlpha);
    }
}