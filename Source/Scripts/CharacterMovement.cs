using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{   
    
    [Header("Speed Settings")]

    [Range(-10.0f,10.0f)]
    public float WalkSpeed = 3.0f;
    private float WalkSpeedBackup = 3.0f;
    [Range(-10.0f,10.0f)]
    public float RunSpeed = 6.0f;
    [Range(-10.0f,10.0f)]
    public float TurnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    [Header("Movement Settings")]
    public Transform Camera;
    public Animator animator;
    public CharacterController controller;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        WalkSpeedBackup = WalkSpeed;
    }
    
    // Update is called once per frame
    private void Update()
    {
        // Input mouse X and Y
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");

        /* character can move X and Z // can't move it on the Y
        I used normalized because if we hold down two keys and go diagonally, we don't move faster
        */
        Vector3 direction = new Vector3(Horizontal, 0.0f, Vertical).normalized;
        
        if(direction.magnitude >= 0.1f){
            
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.localRotation.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.localRotation.eulerAngles.y, targetAngle, ref turnSmoothVelocity, TurnSmoothTime);
            transform.localRotation = Quaternion.Euler(0.0f, angle, 0.0f);

            Vector3 MoveDirection = Quaternion.Euler(0.0f, targetAngle, 0.0f) * Vector3.forward;
            controller.Move(MoveDirection.normalized * Speed * Time.deltaTime);
        }
        // Left shift run button settings // Increasing speed
        if(Input.GetButtonDown("Run")){
            WalkSpeed = RunSpeed;
        }else if (Input.GetButtonUp("Run")){
            WalkSpeed = WalkSpeedBackup;
        }
        // Returning 2 float value to animator
        animator.SetFloat("InputX", Horizontal);
        animator.SetFloat("InputY", Vertical);
    }
}
