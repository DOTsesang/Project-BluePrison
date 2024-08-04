using UnityEngine;
using Cinemachine;

public class TeleportAndSwitchCamera : MonoBehaviour
{
    // 순간이동할 목표 GameObject
    public GameObject teleportTarget;

    // 전환할 가상 카메라
    public CinemachineVirtualCamera newVirtualCamera;

    // 기존의 가상 카메라 (현재 활성화된 카메라)
    private CinemachineVirtualCamera currentVirtualCamera;

    private void Start()
    {
        // 현재 활성화된 가상 카메라를 찾습니다.
        currentVirtualCamera = FindObjectOfType<CinemachineBrain>().ActiveVirtualCamera as CinemachineVirtualCamera;
    }

    // Collider와 충돌했을 때 호출되는 함수
    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트가 Player 태그를 가지고 있는지 확인
        if (collision.gameObject.CompareTag("Player"))
        {
            // Player 오브젝트를 목표 GameObject의 위치로 이동
            collision.gameObject.transform.position = teleportTarget.transform.position;

            // 현재 가상 카메라를 비활성화하고 새로운 가상 카메라를 활성화
            if (currentVirtualCamera != null)
            {
                currentVirtualCamera.Priority = 0;
            }
            newVirtualCamera.Priority = 10;

            // 현재 활성화된 가상 카메라를 새로운 가상 카메라로 업데이트
            currentVirtualCamera = newVirtualCamera;
        }
    }
}
