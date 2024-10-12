using UnityEngine;

public class OnTriggerSpawnEnemy : MonoBehaviour { 

    public GameObject enemy;
    public bool isActive;
    public float spawnDelayTime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Invoke("SpawnEnemy", spawnDelayTime);
        }
    }
    void SpawnEnemy()
    {
        enemy.SetActive(isActive);
    }
}
