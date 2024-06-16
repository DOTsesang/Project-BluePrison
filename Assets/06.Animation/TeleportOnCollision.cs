using UnityEngine;
using Cinemachine;

public class TeleportAndSwitchCamera : MonoBehaviour
{
    // �����̵��� ��ǥ GameObject
    public GameObject teleportTarget;

    // ��ȯ�� ���� ī�޶�
    public CinemachineVirtualCamera newVirtualCamera;

    // ������ ���� ī�޶� (���� Ȱ��ȭ�� ī�޶�)
    private CinemachineVirtualCamera currentVirtualCamera;

    private void Start()
    {
        // ���� Ȱ��ȭ�� ���� ī�޶� ã���ϴ�.
        currentVirtualCamera = FindObjectOfType<CinemachineBrain>().ActiveVirtualCamera as CinemachineVirtualCamera;
    }

    // Collider�� �浹���� �� ȣ��Ǵ� �Լ�
    private void OnCollisionEnter(Collision collision)
    {
        // �浹�� ������Ʈ�� Player �±׸� ������ �ִ��� Ȯ��
        if (collision.gameObject.CompareTag("Player"))
        {
            // Player ������Ʈ�� ��ǥ GameObject�� ��ġ�� �̵�
            collision.gameObject.transform.position = teleportTarget.transform.position;

            // ���� ���� ī�޶� ��Ȱ��ȭ�ϰ� ���ο� ���� ī�޶� Ȱ��ȭ
            if (currentVirtualCamera != null)
            {
                currentVirtualCamera.Priority = 0;
            }
            newVirtualCamera.Priority = 10;

            // ���� Ȱ��ȭ�� ���� ī�޶� ���ο� ���� ī�޶�� ������Ʈ
            currentVirtualCamera = newVirtualCamera;
        }
    }
}
