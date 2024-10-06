using UnityEngine;

public class EnemyChaseBox : MonoBehaviour
{

    public bool willchase = false;
    public bool isplayer = false;
    public GameObject enemy;
    Enemy_2 e;
    private void Start()
    {
        e = enemy.GetComponent<Enemy_2>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && !isplayer )
        {
            if (willchase)
            {
                e.ResumeChase();
            }
            else
            {
                e.StopChase();
            }
        }
        if (other.gameObject.CompareTag("Player") && isplayer )
        {
            if (willchase)
            {
                enemy.SetActive(true);
                e.ResumeChase();
            }
            else
            {
                e.StopChase();
                enemy.SetActive(false);
            }
        }
    }

}
