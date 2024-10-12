using UnityEngine;

public class PlayerCollisionDetector : MonoBehaviour
{
    public GameObject deadGIFImage;
    private GIFAnimator gif;
    private bool isCollided = false;

    void Start()
    {
        if (deadGIFImage != null)
        {
            gif = deadGIFImage.GetComponent<GIFAnimator>();
            if (gif == null)
            {
                Debug.LogError("GIFAnimator 컴포넌트를 찾을 수 없습니다.");
            }
        }
        else
        {
            Debug.LogError("deadGIFImage가 설정되지 않았습니다.");
        }
    }

    void Update()
    {
        if (isCollided && Input.GetKeyDown(KeyCode.E))
        {
            if (gif != null)
            {
                gif.HideAnimation();
            }
            Time.timeScale = 1;
            isCollided = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gif != null)
            {
                gif.PlayAnimation();
            }
            isCollided = true;
            Time.timeScale = 0;
            Debug.Log("Player와 충돌했습니다.");
        }
    }
}