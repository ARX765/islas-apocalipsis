using UnityEngine;

namespace IslasApocalipsis.Player
{
    /// <summary>
    /// Main player controller handling movement, jumping, and input
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        public float walkSpeed = 5f;
        public float runSpeed = 8f;
        public float jumpHeight = 2f;
        public float gravity = -20f;
        public float turnSmoothTime = 0.1f;

        [Header("Ground Check")]
        public Transform groundCheck;
        public float groundDistance = 0.4f;
        public LayerMask groundMask;

        [Header("References")]
        public Transform cameraTransform;

        private CharacterController controller;
        private PlayerStats stats;
        private ClimbingSystem climbingSystem;
        
        private Vector3 velocity;
        private bool isGrounded;
        private float turnSmoothVelocity;
        private bool isRunning;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            stats = GetComponent<PlayerStats>();
            climbingSystem = GetComponent<ClimbingSystem>();
            
            if (cameraTransform == null)
            {
                cameraTransform = Camera.main.transform;
            }
        }

        private void Update()
        {
            // Don't process input if climbing
            if (climbingSystem != null && climbingSystem.IsClimbing)
            {
                return;
            }

            CheckGrounded();
            HandleMovement();
            HandleJump();
            ApplyGravity();
        }

        private void CheckGrounded()
        {
            if (groundCheck != null)
            {
                isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            }
            else
            {
                isGrounded = controller.isGrounded;
            }

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f; // Small value to keep grounded
            }
        }

        private void HandleMovement()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            isRunning = Input.GetKey(KeyCode.LeftShift) && stats.CanSprint();

            if (direction.magnitude >= 0.1f)
            {
                // Calculate target angle based on camera
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                // Move in the direction we're facing
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                float currentSpeed = isRunning ? runSpeed : walkSpeed;
                
                controller.Move(moveDir.normalized * currentSpeed * Time.deltaTime);

                // Consume stamina when running
                if (isRunning)
                {
                    stats.ConsumeStamina(stats.sprintStaminaCost * Time.deltaTime);
                }
            }
        }

        private void HandleJump()
        {
            if (Input.GetButtonDown("Jump") && isGrounded && stats.CanJump())
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                stats.ConsumeStamina(stats.jumpStaminaCost);
            }
        }

        private void ApplyGravity()
        {
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }

        public bool IsGrounded() => isGrounded;
        public bool IsRunning() => isRunning;
    }
}
