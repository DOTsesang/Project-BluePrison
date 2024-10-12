using UnityEngine;
using Cinemachine;

public class VirtualCameraFollow : MonoBehaviour
{
    public Transform player;
    public CinemachineVirtualCamera virtualCamera;
    public float distanceFromPlayer = 5f;
    public float smoothTime = 0.3f;
    public float predictionTime = 0.5f;
    public float pathEndThreshold = 0.1f; // 패스 끝 감지를 위한 임계값

    private CinemachineTrackedDolly trackedDolly;
    private CinemachinePathBase path;
    private Vector3 velocity = Vector3.zero;
    private Vector3 lastPlayerPosition;
    private Vector3 playerVelocity;

    private void Start()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        if (player == null || virtualCamera == null)
        {
            Debug.LogError("VirtualCameraFollow: 필요한 컴포넌트가 연결되지 않았습니다.");
            return;
        }

        trackedDolly = virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        if (trackedDolly == null)
        {
            Debug.LogError("VirtualCameraFollow: Virtual Camera에 Tracked Dolly 바디가 없습니다.");
            return;
        }

        path = trackedDolly.m_Path;
        if (path == null)
        {
            Debug.LogError("VirtualCameraFollow: Tracked Dolly에 경로가 설정되지 않았습니다.");
            return;
        }

        lastPlayerPosition = player.position;
        UpdateCameraPosition();

        Debug.Log("VirtualCameraFollow: 초기화 완료");
    }

    private void LateUpdate()
    {
        if (player == null || trackedDolly == null || path == null) return;

        UpdatePlayerVelocity();
        UpdateCameraPosition();
    }

    private void UpdatePlayerVelocity()
    {
        Vector3 currentPlayerPosition = player.position;
        playerVelocity = (currentPlayerPosition - lastPlayerPosition) / Time.deltaTime;
        lastPlayerPosition = currentPlayerPosition;
    }

    private void UpdateCameraPosition()
    {
        Vector3 predictedPlayerPosition = player.position + playerVelocity * predictionTime;
        Vector3 desiredPosition = CalculateDesiredPosition(predictedPlayerPosition);

        float desiredPathPosition = path.FindClosestPoint(desiredPosition, 0, -1, 10);
        float currentPathPosition = trackedDolly.m_PathPosition;

        // 패스 끝에 가까워질 때 처리
        float pathEndDistance = Mathf.Abs(path.MaxPos - desiredPathPosition);
        if (pathEndDistance < pathEndThreshold)
        {
            // 패스 끝에 가까워지면 움직임을 감속
            float slowdownFactor = pathEndDistance / pathEndThreshold;
            desiredPathPosition = Mathf.Lerp(currentPathPosition, desiredPathPosition, slowdownFactor);
        }

        // 부드러운 이동을 위해 SmoothDamp 사용
        float smoothedPathPosition = Mathf.SmoothDamp(currentPathPosition, desiredPathPosition, ref velocity.x, smoothTime);

        // 패스 범위 내로 제한
        trackedDolly.m_PathPosition = Mathf.Clamp(smoothedPathPosition, 0, path.MaxPos);

        Debug.Log($"Player Position: {player.position}, Predicted Position: {predictedPlayerPosition}, Camera Path Position: {trackedDolly.m_PathPosition}");
    }

    private Vector3 CalculateDesiredPosition(Vector3 targetPosition)
    {
        Vector3 playerDirection = playerVelocity.normalized;
        if (playerDirection == Vector3.zero)
        {
            playerDirection = player.forward;
        }

        Vector3 cameraOffset = playerDirection * distanceFromPlayer;
        return targetPosition + cameraOffset;
    }
}