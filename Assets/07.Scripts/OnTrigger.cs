using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTrigger : MonoBehaviour
{   
    enum ObjectType
    { 
        Trap,
        Door
    }

    [SerializeField]
    private ObjectType type;

    void OnTriggerEnter(Collider col){

        switch (type){
            case ObjectType.Trap:
                Debug.Log("Ʈ���Դϴ�");
                break;
            case ObjectType.Door:
                Debug.Log("���Դϴ�");
                break;
        }
    }
}
