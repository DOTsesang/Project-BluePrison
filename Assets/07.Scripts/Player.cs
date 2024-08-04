using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float turnSpeed = 5.0f;
    public float moveSpeed = 1f;
    public float sensitivity = 100f;
    
    public static bool isGrounded = true;
    // public static bool isCrawled = false;
    private bool isCrouch = false;
    public float jumpForce = 10f;
    public Vector3 currentPosition;
    public Vector3 previousPosition;

    private RaycastHit hit;
    Animator anim;
    Transform tr;
    Vector3 dir;
    Rigidbody rb;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rb  = GetComponent<Rigidbody>();
        // pv = GetComponent<PhotonView>();
        // virtualCamera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();

        // if(pv.IsMine){
        //     virtualCamera.Follow = transform;
        //     virtualCamera.LookAt = transform;
        // }

        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {   
        Debug.DrawRay(this.transform.position, Vector3.down* 100.0f, Color.green);
        Move();
        Jump();
        Run();
        CheckMovement();     
        dir.x = -Input.GetAxis("Vertical");
        dir.z = Input.GetAxis("Horizontal");
        
    }
    void FixedUpdate() {
        Fall();    
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {   
            isGrounded = true;
            anim.SetBool("isJump", false);
            anim.SetBool("isFalling", false);
        }
    }
    void OnTriggerEnter(Collider collider){
        if(collider.gameObject.CompareTag("Pushable")){
            anim.SetBool("isPush", true);
        }
        else if (collider.gameObject.CompareTag("Climbable")){
            anim.SetBool("isClimb", true);
        }
    }
    void OnTriggerExit(Collider collider){
        if(collider.gameObject.CompareTag("Pushable")){
            anim.SetBool("isPush", false);
        }
        else if (collider.gameObject.CompareTag("Climbable")){
            anim.SetBool("isClimb", false);
        }
    }

    // void Move(){
    //     float horizontalInput = Input.GetAxis("Horizontal");
    //     Vector3 moveDirection = new Vector3(horizontalInput, 0f, 0f).normalized;
    //     transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        
    //     float moveDistance = moveDirection.magnitude;
    //     // if (moveDistance > 0f)
    //     //     //anim.SetBool("isRun", true);
    //     // else
    //     //     //anim.SetBool("isRun", false);
            
    // }
    void CheckMovement(){
        if(dir == Vector3.zero){
            anim.SetBool("isWalk", false);

        }
        else{
            anim.SetBool("isWalk", true);
        }
    }
    void Move(){
        // v = Input.GetAxis("Horizontal");
        // h = -Input.GetAxis("Vertical");

        if (dir.z != 0.0f)
            anim.SetFloat("moveDir", 1.0f);
        else
            anim.SetFloat("moveDir", 0.0f);

        if(dir != Vector3.zero){
            transform.forward = Vector3.Lerp(transform.forward, dir, turnSpeed * Time.deltaTime);
        }
        rb.MovePosition(this.gameObject.transform.position + dir * moveSpeed*Time.deltaTime);
    }
    void Run(){
        if(Input.GetButtonDown("Sprint") && !isCrouch){
            moveSpeed += 1.5f;
            anim.SetBool("isRun", true);
        }
        else if (Input.GetButtonUp("Sprint") && !isCrouch){
            moveSpeed -= 1.5f;
            anim.SetBool("isRun", false);
        }
        if (moveSpeed == 4.5f){
            moveSpeed = 3.0f;
        }
    }


    void Jump(){
        if (Input.GetButtonDown("Jump") && isGrounded && !isCrouch)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            anim.SetBool("isJump", true);
        }
    }
    void Fall(){
        Physics.Raycast(
            (this.transform.position + Vector3.up),
            Vector3.down,
            out hit,
            1000.0f,
            1 << 6
        );
        float distanceToGround = transform.position.y - hit.point.y;
        if(transform.position.y - hit.point.y > 10.0f){
            anim.SetBool("isFalling", true);
        }
    }

    void Crouch(){ 
        if (Input.GetButtonDown("Crouch") && !isCrouch){
            moveSpeed = moveSpeed / 2;
            isCrouch = true;

            //anim.SetBool("isCrawled", true);
        }
        else{
            moveSpeed = moveSpeed * 2;
            isCrouch = false;

            //anim.SetBool("isCrawled", false);
        }
    }
    void Climb(){
        // v = Input.GetAxis("Horizontal");
        // h = -Input.GetAxis("Vertical");

        if(dir != Vector3.zero){
            transform.forward = Vector3.Slerp(transform.forward, dir, turnSpeed * Time.deltaTime);
        }
        rb.MovePosition(this.gameObject.transform.position + dir * moveSpeed*Time.deltaTime);
    }

}

