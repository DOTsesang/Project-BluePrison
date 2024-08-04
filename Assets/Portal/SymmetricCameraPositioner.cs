using UnityEngine;

public class PortalCameraPositioner : MonoBehaviour
{
    public Transform player;        // 플레이어의 Transform
    public Transform portal1;       // 첫 번째 포털 (Quad)
    public Transform portal2;       // 두 번째 포털
    public Camera portalCamera;     // 포털 카메라

    void Update()
    {
        if (player == null || portal1 == null || portal2 == null || portalCamera == null)
            return;

        float angle = GetAngleFromAToB(player, portal1);
        // Debug.Log("Player와 portal 1의  각도 :"+ angle);
        SetCameraRotation(angle);
    }
    public float GetAngleFromAToB(Transform A, Transform B){
        // A에서 B를 향하는 방향 벡터
        Vector3 directionAtoB = B.position - A.position;
        
        // A의 forward 방향을 기준으로 회전 계산
        Quaternion rotationA = A.rotation;
        Quaternion rotationAtoB = Quaternion.LookRotation(directionAtoB);
        
        // 두 회전 사이의 각도 계산
        float angle = Quaternion.Angle(rotationA, rotationAtoB);
        
        return angle;
    }

    public void SetCameraRotation(float angle){
        Vector3 currentRotation = portalCamera.transform.eulerAngles;
        currentRotation.y = angle;
        portalCamera.transform.rotation = Quaternion.Euler(currentRotation);    
    }
}