using UnityEngine;
using Cinemachine;

public class CameraSwitchTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera currentCamera;
    public CinemachineVirtualCamera nextCamera;
    public int priorityBoost = 10;
    
    // 활성화할 오브젝트 추가
    public GameObject objectToActivate;

    private void Start()
    {
        InitializeCameras();
    }

    private void InitializeCameras()
    {
        if (currentCamera != null && nextCamera != null)
        {
            // 현재 카메라의 우선순위를 높게 설정
            currentCamera.Priority += priorityBoost;

            // 다음 카메라의 우선순위를 낮게 설정
            nextCamera.Priority -= priorityBoost;

            // 다음 카메라 게임오브젝트 비활성화
            nextCamera.gameObject.SetActive(false);

            Debug.Log($"카메라 초기화: {currentCamera.name} 활성화, {nextCamera.name} 비활성화");
        }
        else
        {
            Debug.LogWarning("카메라가 올바르게 설정되지 않았습니다.");
        }

        // 활성화할 오브젝트 초기 비활성화
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false);
            Debug.Log($"오브젝트 '{objectToActivate.name}'가 초기에 비활성화되었습니다.");
        }
    }

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

            // 다음 카메라 게임오브젝트 활성화
            nextCamera.gameObject.SetActive(true);
            
            // 오브젝트 활성화
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
                Debug.Log($"카메라 전환: {currentCamera.name} -> {nextCamera.name}. 오브젝트 '{objectToActivate.name}'가 활성화되었습니다.");
            }
            else
            {
                Debug.LogWarning("활성화할 오브젝트가 설정되지 않았습니다.");
            }

            // 현재 카메라와 다음 카메라를 교체
            CinemachineVirtualCamera temp = currentCamera;
            currentCamera = nextCamera;
            nextCamera = temp;

            // 이전 카메라(이제 nextCamera가 된) 게임오브젝트 비활성화
            nextCamera.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("카메라가 올바르게 설정되지 않았습니다.");
        }
    }
}