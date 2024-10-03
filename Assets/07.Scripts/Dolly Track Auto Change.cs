using UnityEngine;
using Cinemachine;

public class DollyTrackSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera firstCamera;
    public CinemachineVirtualCamera secondCamera;
    public CinemachineDollyCart dollyCart;
    public CinemachineSmoothPath secondPath;
    public float switchThreshold = 0.99f; // 전환 임계값 (0.0 ~ 1.0)
    public float endThreshold = 0.001f; // 경로 끝 근처로 간주할 거리

    private bool hasSwitched = false;
    private CinemachineSmoothPath firstPath;

    private void Start()
    {
        // 초기화
        hasSwitched = false;

        if (dollyCart == null)
        {
            Debug.LogError("DollyCart is not assigned!");
            enabled = false;
            return;
        }

        dollyCart.m_Position = 0;
        firstPath = dollyCart.m_Path as CinemachineSmoothPath;

        if (firstPath == null)
        {
            Debug.LogError("First path is not a CinemachineSmoothPath!");
            enabled = false;
            return;
        }

        // 카메라 초기 설정
        SetActiveCamera(firstCamera);
    }

    private void Update()
    {
        if (dollyCart == null || dollyCart.m_Path == null)
        {
            Debug.LogError("DollyCart or its path is not assigned!");
            return;
        }

        if (hasSwitched) return;

        float pathLength = dollyCart.m_Path.PathLength;
        float distanceFromEnd = pathLength - dollyCart.m_Position;

        // 경로의 끝에 도달했는지 또는 switchThreshold를 넘었는지 확인
        if (distanceFromEnd <= endThreshold || dollyCart.m_Position / pathLength >= switchThreshold)
        {
            SwitchToSecondTrack();
        }
    }

    private void SwitchToSecondTrack()
    {
        // 두 번째 경로가 null이 아닌지 확인
        if (secondPath == null)
        {
            Debug.LogError("Second path is not assigned!");
            return;
        }

        // 첫 번째 트랙의 끝에 도달하면 두 번째 트랙으로 전환
        dollyCart.m_Path = secondPath;
        dollyCart.m_Position = 0;

        // 카메라 전환
        SetActiveCamera(secondCamera);

        hasSwitched = true;
        Debug.Log("Switched to second track");
    }

    private void SetActiveCamera(CinemachineVirtualCamera activeCamera)
    {
        firstCamera.gameObject.SetActive(activeCamera == firstCamera);
        secondCamera.gameObject.SetActive(activeCamera == secondCamera);
    }

    // 에디터에서 테스트를 위한 메서드
    public void ResetToFirstTrack()
    {
        hasSwitched = false;
        dollyCart.m_Path = firstPath;
        dollyCart.m_Position = 0;
        SetActiveCamera(firstCamera);
        Debug.Log("Reset to first track");
    }
}
