using UnityEngine;

public class OnTriggerOpenDoor : MonoBehaviour
{
    public Animator animator;
    public bool isOpen;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("isOpen", isOpen);
        }
    }
}
