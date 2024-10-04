using UnityEngine;
using Cinemachine;

public class CameraSwitchTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera currentCamera;
    public CinemachineVirtualCamera nextCamera;
    public int priorityBoost = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SwitchCamera();
        }
    }

    private void SwitchCamera()
    {
        if (currentCamera != null && nextCamera != null)
        {
            // 현재 카메라의 우선순위를 낮춤
            currentCamera.Priority -= priorityBoost;

            // 다음 카메라의 우선순위를 높임
            nextCamera.Priority += priorityBoost;

            // 현재 카메라와 다음 카메라를 교체
            CinemachineVirtualCamera temp = currentCamera;
            currentCamera = nextCamera;
            nextCamera = temp;
        }
        else
        {
            Debug.LogWarning("카메라가 올바르게 설정되지 않았습니다.");
        }
    }
}