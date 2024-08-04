using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public float turnSpeed = 5; // ���콺 ����   
    public float moveSpeed = 5;
    public Transform cameraTransform; 

    void Start()
    {
        if (cameraTransform == null)
        {
            //cameraTransform = Camera.main.transform; // ī�޶� �������� �ʾ����� ���� ī�޶� ���
        }
    }
    // Update is called once per frame
    void Update()
    {
        DroneMove();
    }

    void DroneMove(){
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        Vector3 movement1, movement2;
        
        if (horizontalInput > 0.0f)
            movement1 = Vector3.forward;
        else if (horizontalInput < 0.0f)
            movement1 = Vector3.back;
        else
            movement1 = Vector3.zero;


        if(verticalInput > 0.0f){
            movement2 = Vector3.left;
        }
        else if (verticalInput < 0.0f){
            movement2 = Vector3.right;
        }   
        else{
            movement2 = Vector3.zero;
        }
        Vector3 moveDirection = (movement1 + movement2).normalized;

        if (Input.GetKey(KeyCode.Space) == true){
            moveDirection += Vector3.up;
        }
        if (Input.GetKey(KeyCode.LeftShift) == true){
            moveDirection -= Vector3.up;
        }

        
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
            
    }

    void DroneRotate(){
        transform.Rotate(Vector3.up * turnSpeed  * Time.deltaTime * Input.GetAxis("Mouse X"));

    }
}
