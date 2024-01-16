using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private CharacterController characterController;
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private Camera followCamera;

    [SerializeField] private float rotationSpeed = 10f;

    private Vector3 PlayerVelocity;
    [SerializeField] private float gravityValue = -13f; // check กันตก

    public bool groundedPlayer;
    [SerializeField] private float jumpHeight = 2.5f; // check jump

    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    void Movement()
    {
        groundedPlayer = characterController.isGrounded;
        
        if(characterController.isGrounded && PlayerVelocity.y <-2)
        {
            PlayerVelocity.y = -1f;
        }

        float horizentalInput = Input.GetAxis("Horizontal");
        float vertocalInput = Input.GetAxis("Vertical");

        Vector3 movementInput = Quaternion.Euler(0, followCamera.transform.eulerAngles.y, 0)
                                * new Vector3(horizentalInput, 0, vertocalInput);
        Vector3 movementDirection = movementInput.normalized;

        characterController.Move(movementDirection*playerSpeed*Time.deltaTime);

        if(movementDirection != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(movementDirection,Vector3.up); //เก็บตัวแปรจุดหมุน 
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed*Time.deltaTime);
        }

       if(Input.GetButtonDown("Jump")&& groundedPlayer)
       {
        PlayerVelocity.y += Mathf.Sqrt(jumpHeight *-3.0f * gravityValue);
            animator.SetTrigger("Jumping");
       }
       
        PlayerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(PlayerVelocity * Time.deltaTime);

        animator.SetFloat("Speed", Mathf.Abs(movementDirection.x) + Mathf.Abs(movementDirection.z));
        animator.SetBool("Ground", characterController.isGrounded);

    }
}
