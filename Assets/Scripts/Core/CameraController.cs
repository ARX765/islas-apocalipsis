using UnityEngine;

namespace IslasApocalipsis.Core
{
    /// <summary>
    /// Third-person camera controller
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        [Header("Target")]
        public Transform target; // The player
        public Vector3 offset = new Vector3(0, 5, -10);

        [Header("Camera Settings")]
        public float mouseSensitivity = 100f;
        public float scrollSensitivity = 10f;
        public float minDistance = 3f;
        public float maxDistance = 15f;
        public float currentDistance = 10f;

        [Header("Rotation Settings")]
        public float minVerticalAngle = -30f;
        public float maxVerticalAngle = 80f;
        public float rotationSpeed = 5f;

        [Header("Collision")]
        public LayerMask collisionMask;
        public float collisionOffset = 0.3f;

        [Header("Lock-on")]
        public bool lockOnEnabled = false;
        public Transform lockOnTarget;

        private float currentX = 0f;
        private float currentY = 20f;
        private Combat.CombatSystem combatSystem;

        private void Start()
        {
            if (target == null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    target = player.transform;
                    combatSystem = player.GetComponent<Combat.CombatSystem>();
                }
            }

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void LateUpdate()
        {
            if (target == null) return;

            HandleInput();
            
            if (lockOnEnabled && lockOnTarget != null)
            {
                LockOnCamera();
            }
            else
            {
                FreeCamera();
            }

            CheckLockOnTarget();
        }

        private void HandleInput()
        {
            // Mouse look
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            currentX += mouseX;
            currentY -= mouseY;
            currentY = Mathf.Clamp(currentY, minVerticalAngle, maxVerticalAngle);

            // Zoom
            float scroll = Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity;
            currentDistance -= scroll;
            currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);
        }

        private void FreeCamera()
        {
            // Calculate desired position
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            Vector3 direction = rotation * Vector3.back;
            Vector3 desiredPosition = target.position + direction * currentDistance;

            // Check for collisions
            RaycastHit hit;
            if (Physics.Linecast(target.position, desiredPosition, out hit, collisionMask))
            {
                desiredPosition = hit.point + hit.normal * collisionOffset;
            }

            // Apply position and rotation
            transform.position = desiredPosition;
            transform.LookAt(target.position + Vector3.up * 1.5f);
        }

        private void LockOnCamera()
        {
            if (lockOnTarget == null) return;

            // Position camera to include both target and lock-on target
            Vector3 midPoint = (target.position + lockOnTarget.position) / 2f;
            Vector3 directionToTarget = (target.position - lockOnTarget.position).normalized;
            Vector3 cameraPos = midPoint - directionToTarget * currentDistance + Vector3.up * 3f;

            transform.position = Vector3.Lerp(transform.position, cameraPos, Time.deltaTime * rotationSpeed);
            transform.LookAt(midPoint + Vector3.up * 1.5f);
        }

        private void CheckLockOnTarget()
        {
            if (combatSystem != null)
            {
                lockOnEnabled = combatSystem.isLockedOn;
                lockOnTarget = combatSystem.GetCurrentTarget();
            }
        }

        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
        }
    }
}
