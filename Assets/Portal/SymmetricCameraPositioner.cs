using UnityEngine;

public class PortalCameraPositioner : MonoBehaviour
{
    public Transform player;        // �÷��̾��� Transform
    public Transform portal1;       // ù ��° ���� (Quad)
    public Transform portal2;       // �� ��° ����
    public Camera portalCamera;     // ���� ī�޶�

    void Update()
    {
        if (player == null || portal1 == null || portal2 == null || portalCamera == null)
            return;

        float angle = GetAngleFromAToB(player, portal1);
        // Debug.Log("Player�� portal 1��  ���� :"+ angle);
        SetCameraRotation(angle);
    }
    public float GetAngleFromAToB(Transform A, Transform B){
        // A���� B�� ���ϴ� ���� ����
        Vector3 directionAtoB = B.position - A.position;
        
        // A�� forward ������ �������� ȸ�� ���
        Quaternion rotationA = A.rotation;
        Quaternion rotationAtoB = Quaternion.LookRotation(directionAtoB);
        
        // �� ȸ�� ������ ���� ���
        float angle = Quaternion.Angle(rotationA, rotationAtoB);
        
        return angle;
    }

    public void SetCameraRotation(float angle){
        Vector3 currentRotation = portalCamera.transform.eulerAngles;
        currentRotation.y = angle;
        portalCamera.transform.rotation = Quaternion.Euler(currentRotation);    
    }
}