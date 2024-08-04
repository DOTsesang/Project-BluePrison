using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // 회전 속도를 조절하기 위한 변수
    public float rotationSpeed = 100f;

    void Update()
    {
        // 매 프레임마다 객체를 회전시키기 위해 Rotate 메서드 사용
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}