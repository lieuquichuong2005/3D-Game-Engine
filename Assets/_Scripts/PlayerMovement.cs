using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private CharacterController playerController;
    [SerializeField] private Animator animator;

    private int moveHash;
    private int attackHash;
    private Vector3 velocity;

    void Start()
    {
        moveHash = Animator.StringToHash("Move");
        attackHash = Animator.StringToHash("Attack");
    }

    void Update()
    {
        HandleMovement();
        HandleAttack();
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        Vector3 move = new Vector3(moveX, 0, moveZ).normalized;

        Vector3 horizontalVelocity = move * moveSpeed;

        if (!playerController.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime; 
        }
        else
        {
            velocity.y = 0; 
        }

        velocity.x = horizontalVelocity.x;
        velocity.z = horizontalVelocity.z;

        if (move != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(move);
        }

        animator.SetFloat(moveHash, horizontalVelocity.magnitude);

        playerController.Move(velocity * Time.deltaTime);
    }

    private void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger(attackHash);
        }
    }
    void HandleJump()
    {
        if (playerController.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }
    }
}