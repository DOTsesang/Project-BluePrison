using UnityEngine;

public class CloseTrigger : MonoBehaviour
{
    public Animator doorAnimator;  // Ʈ���ſ� ������ ���� Animator
    public string triggerName = "CloseDoor";  // �ִϸ��̼� Ʈ���� �̸�
    private bool isClosed = false;  // ���� �������� Ȯ���ϴ� �÷���

    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾ Ʈ���ſ� ������, ���� ���� ������ �ʾ��� ���� ����
        if (other.CompareTag("Player") && !isClosed)
        {
            doorAnimator.SetTrigger(triggerName);  // ���� �ݴ� �ִϸ��̼� �ߵ�
            isClosed = true;  // ���� ���� ���·� ����
        }
    }
}
