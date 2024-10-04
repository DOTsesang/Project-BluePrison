using UnityEngine;

public class WallDestruction : MonoBehaviour
{
    public GameObject intactWall; // 온전한 벽 (A)
    public GameObject destroyedWall; // 파편화된 벽 (B)
    public GameObject triggerArea; // Cube로 만든 트리거 영역 (C)

    private void Start()
    {
        intactWall.SetActive(true);
        destroyedWall.SetActive(false);

        SetupTriggerArea();
    }

    private void SetupTriggerArea()
    {
        Collider triggerCollider = triggerArea.GetComponent<Collider>();
        if (triggerCollider == null)
        {
            triggerCollider = triggerArea.AddComponent<BoxCollider>();
            Debug.Log("Added BoxCollider to triggerArea.");
        }
        
        triggerCollider.isTrigger = true;
        Debug.Log("TriggerArea collider has been set to trigger.");

        Rigidbody rb = triggerArea.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = triggerArea.AddComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.useGravity = false;
            Debug.Log("Added kinematic Rigidbody to triggerArea.");
        }

        // 연속 충돌 감지 설정
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        Debug.Log("Set Continuous Collision Detection on triggerArea.");

        Debug.Log($"TriggerArea setup complete. Position: {triggerArea.transform.position}, Scale: {triggerArea.transform.localScale}");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger entered by: {other.gameObject.name}, Tag: {other.tag}, Position: {other.transform.position}");

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy entered the trigger area. Destroying wall...");
            DestroyWall();
        }
        else
        {
            Debug.Log("Object entered trigger, but it's not an Enemy.");
        }
    }

    private void DestroyWall()
    {
        intactWall.SetActive(false);
        destroyedWall.SetActive(true);
        Debug.Log("Wall destroyed: A deactivated, B activated.");
    }
}