using UnityEngine;
using UnityEngine.UI;

public class ItemUIOnOff : MonoBehaviour
{
    [Header("Item 이미지 설정")]
    public RawImage image;
    bool isActive = false;
    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ToggleImage();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("플레이어가 아이템 범위에 들어왔습니다.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("플레이어가 아이템 범위를 벗어났습니다.");
        }
    }

    private void ToggleImage()
    {
        if (image != null)
        {
            isActive = !isActive;
            image.gameObject.SetActive(isActive);

            // 이미지가 활성화되면 시간을 멈추고, 비활성화되면 시간을 다시 흐르게 합니다.
            Time.timeScale = isActive ? 0f : 1f;

            Debug.Log($"이미지 가시성이 토글되었습니다. 활성화 상태: {isActive}");
            Debug.Log($"Time.timeScale: {Time.timeScale}");
        }
        else
        {
            Debug.LogError("이미지 참조가 설정되지 않았습니다. Inspector에서 할당해주세요.");
        }
    }
}
