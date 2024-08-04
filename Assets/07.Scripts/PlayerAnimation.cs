using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    public bool canMove = true;  // 플레이어가 움직일 수 있는지 여부

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!canMove)
            return;

        // 'C' 키 입력에 따른 앉기/일어나기 애니메이션 제어
        if (Input.GetKeyDown(KeyCode.C))
        {
            bool isCrouch = anim.GetBool("isCrouch");
            anim.SetBool("isCrouch", !isCrouch);
        }

        // 기존 플레이어 이동 코드 추가 (예시)
        // MovePlayer();
    }

    // 플레이어 조작 비활성화
    public void DisableControls()
    {
        canMove = false;
    }

    // 플레이어 조작 활성화
    public void EnableControls()
    {
        canMove = true;
    }

    // 예시: 플레이어 이동 코드 (필요에 따라 구현)
    // void MovePlayer()
    // {
    //     // 이동 코드 작성
    // }
}
