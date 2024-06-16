using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{   
    // �ܹ��� ��Ż
    public Transform exitPoint; 
    public float delayTime = 1.0f;

    
    void OnCollisionEnter(Collision col){
        StartCoroutine(Teleport(col));
        
    }

    IEnumerator Teleport(Collision col)    
    {
        yield return new WaitForSeconds(delayTime);
        col.transform.position = exitPoint.position;
        col.transform.position += Vector3.up;
        
        Debug.Log($"{this.gameObject.name}�� �̵��մϴ�.");
    }   
  
}
