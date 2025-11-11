using UnityEngine;

namespace IslasApocalipsis.Player
{
    /// <summary>
    /// Handles climbing mechanics with stamina consumption
    /// </summary>
    public class ClimbingSystem : MonoBehaviour
    {
        [Header("Climbing Settings")]
        public float climbSpeed = 3f;
        public float climbJumpForce = 5f;
        public LayerMask climbableMask;
        public float climbCheckDistance = 1f;
        public float ledgeCheckDistance = 1.5f;

        [Header("References")]
        public Transform climbCheck;
        
        private PlayerController playerController;
        private PlayerStats stats;
        private CharacterController characterController;
        
        private bool isClimbing = false;
        private Vector3 climbNormal;
        private bool canClimb = false;

        public bool IsClimbing => isClimbing;

        private void Awake()
        {
            playerController = GetComponent<PlayerController>();
            stats = GetComponent<PlayerStats>();
            characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            CheckForClimbableSurface();
            
            if (Input.GetKeyDown(KeyCode.E) && canClimb && !isClimbing)
            {
                StartClimbing();
            }
            
            if (isClimbing)
            {
                HandleClimbing();
            }
        }

        private void CheckForClimbableSurface()
        {
            Vector3 checkPosition = climbCheck != null ? climbCheck.position : transform.position;
            RaycastHit hit;
            
            if (Physics.Raycast(checkPosition, transform.forward, out hit, climbCheckDistance, climbableMask))
            {
                canClimb = true;
                climbNormal = hit.normal;
            }
            else
            {
                canClimb = false;
                if (isClimbing)
                {
                    StopClimbing();
                }
            }
        }

        private void StartClimbing()
        {
            if (!stats.CanClimb()) return;
            
            isClimbing = true;
            characterController.enabled = false;
            
            // Align player to wall
            Vector3 targetRotation = Quaternion.LookRotation(-climbNormal).eulerAngles;
            transform.rotation = Quaternion.Euler(0f, targetRotation.y, 0f);
            
            Debug.Log("Started climbing");
        }

        private void HandleClimbing()
        {
            if (!stats.CanClimb())
            {
                StopClimbing();
                return;
            }

            // Consume stamina
            stats.ConsumeStamina(stats.climbStaminaCost * Time.deltaTime);

            // Get input
            float vertical = Input.GetAxisRaw("Vertical");
            float horizontal = Input.GetAxisRaw("Horizontal");

            // Calculate movement direction
            Vector3 climbDirection = Vector3.zero;
            
            if (vertical > 0) // Climb up
            {
                climbDirection += Vector3.up;
            }
            else if (vertical < 0) // Climb down
            {
                climbDirection += Vector3.down;
            }
            
            if (horizontal != 0) // Move sideways
            {
                Vector3 right = Vector3.Cross(Vector3.up, -climbNormal);
                climbDirection += right * horizontal;
            }

            // Move the player
            transform.position += climbDirection.normalized * climbSpeed * Time.deltaTime;

            // Jump off wall
            if (Input.GetButtonDown("Jump"))
            {
                JumpOffWall();
            }

            // Stop climbing
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
            {
                StopClimbing();
            }

            // Check for ledge (top of climbable surface)
            CheckForLedge();
        }

        private void CheckForLedge()
        {
            RaycastHit hit;
            Vector3 checkPos = transform.position + Vector3.up * 2f;
            
            if (!Physics.Raycast(checkPos, transform.forward, out hit, ledgeCheckDistance, climbableMask))
            {
                // Found a ledge, climb up
                if (Input.GetAxisRaw("Vertical") > 0)
                {
                    ClimbOverLedge();
                }
            }
        }

        private void ClimbOverLedge()
        {
            // Move player over the ledge
            Vector3 ledgePosition = transform.position + transform.forward * 1f + Vector3.up * 2f;
            transform.position = ledgePosition;
            StopClimbing();
        }

        private void JumpOffWall()
        {
            Vector3 jumpDirection = -climbNormal + Vector3.up;
            StopClimbing();
            
            // Apply jump force (this would need integration with PlayerController)
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(jumpDirection.normalized * climbJumpForce, ForceMode.Impulse);
            }
        }

        private void StopClimbing()
        {
            isClimbing = false;
            characterController.enabled = true;
            Debug.Log("Stopped climbing");
        }

        private void OnDrawGizmos()
        {
            if (climbCheck != null)
            {
                Gizmos.color = canClimb ? Color.green : Color.red;
                Gizmos.DrawRay(climbCheck.position, transform.forward * climbCheckDistance);
            }
        }
    }
}
