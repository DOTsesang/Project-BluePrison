using UnityEngine;
using Cinemachine;

public class AutoDollyCamera : MonoBehaviour
{
    public CinemachineDollyCart dollyCart; // Dolly Cart
    public Transform target; // 따라갈 대상
    public float distanceFromTarget = 5f; // 대상을 기준으로 유지할 거리
    public float heightOffset = 2f; // 카메라 높이 오프셋

    private void Start()
    {
        if (dollyCart == null || target == null)
        {
            Debug.LogError("DollyCart or target is not assigned!");
            enabled = false;
        }
    }

    private void Update()
    {
        // 대상의 위치를 기준으로 카메라 위치 계산
        Vector3 targetPosition = target.position;
        Vector3 desiredPosition = targetPosition - target.forward * distanceFromTarget + Vector3.up * heightOffset;

        // Dolly Cart의 위치 업데이트
        dollyCart.transform.position = desiredPosition;

        // Dolly Cart의 방향 업데이트 (카메라가 항상 대상을 바라보도록)
        dollyCart.transform.LookAt(target);
    }
}
