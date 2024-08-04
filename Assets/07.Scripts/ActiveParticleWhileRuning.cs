using UnityEngine;

public class ActiveParticleWhileRuning : MonoBehaviour
{
    public GameObject particle;

    void Start(){
        particle.GetComponent<ParticleSystem>().Stop();
    }
    void Update()
    {
        if (particle == null)
        {
            Debug.LogError("Particle GameObject is not assigned!");
        }
        if(Input.GetButtonDown("Sprint")){
            particle.GetComponent<ParticleSystem>().Play();
        }
        if(Input.GetButtonUp("Sprint")){
            particle.GetComponent<ParticleSystem>().Stop();
        }
    }
}