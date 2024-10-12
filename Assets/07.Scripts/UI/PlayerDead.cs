using UnityEngine;

public class PlayerDead : MonoBehaviour
{
    public GameObject deadGIFImage;
    GIFAnimator gif; 
    bool isDead = false;
    void Start()
    {
        gif = deadGIFImage.GetComponent<GIFAnimator>();
    }
    void Update(){
        if(isDead && Input.GetKeyDown(KeyCode.E)){
            gif.HideAnimation();
            Time.timeScale = 1;
        }
    }
    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("Enemy")){
            gif.PlayAnimation();
            isDead = true;
            // Time.timeScale = 0.1f;
        }
    }

}
