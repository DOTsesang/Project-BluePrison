using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageTrigger : MonoBehaviour
{
    public RawImage targetImage;  // RawImage UI
    public float fadeDuration = 1.0f;  // ������ ��Ÿ���� ������� �ð�
    private bool isFading = false;  // ���� ���̵� ������ ����

    private void Start()
    {
        // ���� �� �̹����� �����ϰ� ����
        SetImageAlpha(0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾ Ʈ���� �ڽ��� ������ �̹����� ������ ��Ÿ��
        if (other.CompareTag("Player") && !isFading)
        {
            StartCoroutine(FadeImage(1f));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // �÷��̾ Ʈ���� �ڽ��� ����� �̹����� ������ ������� ��
        if (other.CompareTag("Player") && !isFading)
        {
            StartCoroutine(FadeImage(0f));
        }
    }

    // RawImage�� ���İ��� �����ϴ� �Լ�
    private void SetImageAlpha(float alpha)
    {
        Color color = targetImage.color;
        color.a = alpha;
        targetImage.color = color;
    }

    // �̹����� ������ ��Ÿ���ų� ������� �ϴ� �ڷ�ƾ �Լ�
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
