using UnityEngine;
using UnityEngine.Events;

namespace IslasApocalipsis.Puzzles
{
    /// <summary>
    /// Base class for puzzle elements
    /// </summary>
    public abstract class PuzzleElement : MonoBehaviour
    {
        [Header("Puzzle Settings")]
        public bool isActivated = false;
        public bool canBeReset = true;

        [Header("Events")]
        public UnityEvent onActivated;
        public UnityEvent onDeactivated;

        public abstract void Activate();
        public abstract void Deactivate();

        protected void TriggerActivation()
        {
            if (!isActivated)
            {
                isActivated = true;
                onActivated?.Invoke();
                Debug.Log($"{gameObject.name} activated");
            }
        }

        protected void TriggerDeactivation()
        {
            if (isActivated && canBeReset)
            {
                isActivated = false;
                onDeactivated?.Invoke();
                Debug.Log($"{gameObject.name} deactivated");
            }
        }
    }

    /// <summary>
    /// Pressure plate puzzle element
    /// </summary>
    public class PressurePlate : PuzzleElement
    {
        [Header("Pressure Plate Settings")]
        public float requiredWeight = 50f;
        public bool staysPressed = false;

        private float currentWeight = 0f;

        private void OnTriggerEnter(Collider other)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                currentWeight += rb.mass;
                CheckActivation();
            }
            else
            {
                // Player or object without rigidbody
                currentWeight += 70f; // Default player weight
                CheckActivation();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (staysPressed) return;

            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                currentWeight -= rb.mass;
            }
            else
            {
                currentWeight -= 70f;
            }
            currentWeight = Mathf.Max(0, currentWeight);
            CheckActivation();
        }

        private void CheckActivation()
        {
            if (currentWeight >= requiredWeight)
            {
                Activate();
            }
            else
            {
                Deactivate();
            }
        }

        public override void Activate()
        {
            TriggerActivation();
        }

        public override void Deactivate()
        {
            TriggerDeactivation();
        }
    }

    /// <summary>
    /// Lever puzzle element
    /// </summary>
    public class Lever : PuzzleElement
    {
        [Header("Lever Settings")]
        public bool requiresInteraction = true;
        public float activationAngle = 45f;

        private bool playerNearby = false;

        private void Update()
        {
            if (requiresInteraction && playerNearby && Input.GetKeyDown(KeyCode.E))
            {
                Toggle();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerNearby = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerNearby = false;
            }
        }

        public void Toggle()
        {
            if (isActivated)
            {
                Deactivate();
            }
            else
            {
                Activate();
            }
        }

        public override void Activate()
        {
            TriggerActivation();
            // Rotate lever
            transform.localRotation = Quaternion.Euler(activationAngle, 0, 0);
        }

        public override void Deactivate()
        {
            TriggerDeactivation();
            // Reset lever
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    /// <summary>
    /// Puzzle door that opens when conditions are met
    /// </summary>
    public class PuzzleDoor : MonoBehaviour
    {
        [Header("Door Settings")]
        public PuzzleElement[] requiredElements;
        public bool requireAllElements = true;
        public float openHeight = 5f;
        public float openSpeed = 2f;

        private bool isOpen = false;
        private Vector3 closedPosition;
        private Vector3 openPosition;

        private void Start()
        {
            closedPosition = transform.position;
            openPosition = closedPosition + Vector3.up * openHeight;
        }

        private void Update()
        {
            CheckPuzzleConditions();
            AnimateDoor();
        }

        private void CheckPuzzleConditions()
        {
            if (requiredElements.Length == 0) return;

            if (requireAllElements)
            {
                // All elements must be activated
                bool allActivated = true;
                foreach (var element in requiredElements)
                {
                    if (element != null && !element.isActivated)
                    {
                        allActivated = false;
                        break;
                    }
                }
                
                if (allActivated && !isOpen)
                {
                    OpenDoor();
                }
                else if (!allActivated && isOpen)
                {
                    CloseDoor();
                }
            }
            else
            {
                // At least one element must be activated
                bool anyActivated = false;
                foreach (var element in requiredElements)
                {
                    if (element != null && element.isActivated)
                    {
                        anyActivated = true;
                        break;
                    }
                }
                
                if (anyActivated && !isOpen)
                {
                    OpenDoor();
                }
                else if (!anyActivated && isOpen)
                {
                    CloseDoor();
                }
            }
        }

        private void AnimateDoor()
        {
            Vector3 targetPosition = isOpen ? openPosition : closedPosition;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * openSpeed);
        }

        private void OpenDoor()
        {
            isOpen = true;
            Debug.Log("Door opened!");
        }

        private void CloseDoor()
        {
            isOpen = false;
            Debug.Log("Door closed!");
        }
    }

    /// <summary>
    /// Crystal puzzle element that requires magic
    /// </summary>
    public class MagicCrystal : PuzzleElement
    {
        [Header("Magic Crystal Settings")]
        public Items.ElementType requiredElement;
        public float chargeTime = 3f;
        public Material activatedMaterial;

        private float currentCharge = 0f;
        private Renderer crystalRenderer;
        private Material originalMaterial;

        private void Awake()
        {
            crystalRenderer = GetComponent<Renderer>();
            if (crystalRenderer != null)
            {
                originalMaterial = crystalRenderer.material;
            }
        }

        private void Update()
        {
            if (isActivated) return;

            // Discharge over time if not being charged
            if (currentCharge > 0)
            {
                currentCharge -= Time.deltaTime;
                currentCharge = Mathf.Max(0, currentCharge);
            }
        }

        public void ChargeWithMagic(Items.ElementType element, float amount)
        {
            if (element == requiredElement || requiredElement == Items.ElementType.None)
            {
                currentCharge += amount;
                
                if (currentCharge >= chargeTime)
                {
                    Activate();
                }
            }
        }

        public override void Activate()
        {
            TriggerActivation();
            if (crystalRenderer != null && activatedMaterial != null)
            {
                crystalRenderer.material = activatedMaterial;
            }
        }

        public override void Deactivate()
        {
            TriggerDeactivation();
            currentCharge = 0f;
            if (crystalRenderer != null && originalMaterial != null)
            {
                crystalRenderer.material = originalMaterial;
            }
        }
    }

    /// <summary>
    /// Block pushing puzzle element
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class PushableBlock : MonoBehaviour
    {
        [Header("Block Settings")]
        public float pushForce = 5f;
        public bool canOnlyPushForward = true;

        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        }

        public void Push(Vector3 direction)
        {
            if (canOnlyPushForward)
            {
                // Only allow pushing in cardinal directions
                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.z))
                {
                    direction = new Vector3(Mathf.Sign(direction.x), 0, 0);
                }
                else
                {
                    direction = new Vector3(0, 0, Mathf.Sign(direction.z));
                }
            }

            rb.AddForce(direction.normalized * pushForce, ForceMode.Impulse);
        }
    }
}
