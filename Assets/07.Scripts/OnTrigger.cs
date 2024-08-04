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
                Debug.Log("트랩입니다");
                break;
            case ObjectType.Door:
                Debug.Log("문입니다");
                break;
        }
    }
}
