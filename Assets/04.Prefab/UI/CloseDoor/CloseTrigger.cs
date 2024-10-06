using UnityEngine;

public class CloseTrigger : MonoBehaviour
{
    public Animator doorAnimator;  // 트리거에 연결할 문의 Animator
    public string triggerName = "CloseDoor";  // 애니메이션 트리거 이름
    private bool isClosed = false;  // 문이 닫혔는지 확인하는 플래그

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어가 트리거에 들어오고, 문이 아직 닫히지 않았을 때만 실행
        if (other.CompareTag("Player") && !isClosed)
        {
            doorAnimator.SetTrigger(triggerName);  // 문을 닫는 애니메이션 발동
            isClosed = true;  // 문이 닫힌 상태로 설정
        }
    }
}
