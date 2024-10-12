using UnityEngine;
using UnityEngine.UI;
public class UIOnOffByTrigger : MonoBehaviour
{
    // 캔버스 받아오기
    [Header("OnOff UI 설정 ")]
    public Canvas canvas;
 
   
    [SerializeField][Header("체크->나타남 빈박스-> 사라짐")]
    private bool willShow = false;  

    void OnTriggerStay(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            Debug.Log("TriggerEnter");

            if (Input.GetKeyDown(KeyCode.E))
            {
                canvas.gameObject.SetActive(willShow);
                Debug.Log("E key를 눌렀습니다.");
            }
        }

    }  
}
