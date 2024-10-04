using UnityEngine;
using Cinemachine;

public class AutoDollyCamera : MonoBehaviour
{
    public Camera mainCamera; // Main Camera
    public Transform target; // 따라갈 대상
    public float distanceFromTarget = 5f; // 대상을 기준으로 유지할 거리
    public float heightOffset = 2f; // 카메라 높이 오프셋

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Main Camera를 자동으로 찾습니다.
        }

        if (mainCamera == null || target == null)
        {
            Debug.LogError("Main Camera or target is not assigned!");
            enabled = false;
        }
    }

    private void LateUpdate()
    {
        if (mainCamera == null || target == null) return;

        // 대상의 위치를 기준으로 카메라 위치 계산
        Vector3 targetPosition = target.position;
        Vector3 desiredPosition = targetPosition - target.forward * distanceFromTarget + Vector3.up * heightOffset;

        // Main Camera의 위치 업데이트
        mainCamera.transform.position = desiredPosition;

        // Main Camera가 항상 대상을 바라보도록 설정
        mainCamera.transform.LookAt(target);
    }
}