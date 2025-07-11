using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
#if USE_INPUTSYSTEM && ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace Yarn.Unity.Example
{
    public class Player : MonoBehaviour
    {
        public static Player Instance;
        public float speed;
        private Rigidbody2D rb;
        private Animator animator;

        public float interactionRadius = 2.0f;
        public float movementFromButtons { get; set; }
        private DialogueAdvanceInput dialogueInput;

        private float horizontal;
        private float vertical;
        private UnityEngine.Vector2 movementVector;
        private UnityEngine.Vector2 lastMoveDirection;

        private List<NPC> nearbyNPCs = new List<NPC>();
        private bool isInteracting = false; // 新增变量，标记是否正在交互


      
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            dialogueInput = FindObjectOfType<DialogueAdvanceInput>();
            dialogueInput.enabled = false;

            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.drag = 0f;
            rb.angularDrag = 0f;
            rb.gravityScale = 0f;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.matrix = UnityEngine.Matrix4x4.TRS(transform.position, UnityEngine.Quaternion.identity, new UnityEngine.Vector3(1, 1, 0));
            Gizmos.DrawWireSphere(UnityEngine.Vector3.zero, interactionRadius);
        }


        void Update()
        {
            UpdateAnimationParameters();
            if (FindObjectOfType<DialogueRunner>().IsDialogueRunning == true)
            {
                isInteracting = true; // 对话正在进行，标记为正在交互
                return;
            }
            else
            {
                isInteracting = false; // 对话结束，标记为不在交互
            }

            if (dialogueInput.enabled)
            {
                dialogueInput.enabled = false;
            }

            #if ENABLE_LEGACY_INPUT_MANAGER
            if (nearbyNPCs.Count > 0 && Input.GetKeyUp(KeyCode.Space))
            {
                foreach (NPC npc in nearbyNPCs)
                {
                    CheckForNearbyNPC(npc);
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("MainMenu");
            }
            #endif
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            NPC npc = other.GetComponent<NPC>();
            if (npc != null)
            {
                nearbyNPCs.Add(npc);
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            NPC npc = other.GetComponent<NPC>();
            if (npc != null)
            {
                nearbyNPCs.Remove(npc);
            }
        }

        public void CheckForNearbyNPC(NPC target)
        {
            if (string.IsNullOrEmpty(target.talkToNode) == false)
            {
                FindObjectOfType<DialogueRunner>().StartDialogue(target.talkToNode);
                dialogueInput.enabled = true;
                isInteracting = true; // 开始交互，标记为正在交互
            }
        }

        void FixedUpdate()
        {
            if (isInteracting) // 正在交互，不进行移动
            {
                rb.velocity = UnityEngine.Vector2.zero;
                return;
            }

            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");

            movementVector = new UnityEngine.Vector2(horizontal, vertical).normalized;

            rb.velocity = movementVector * speed;
        }

        private void UpdateAnimationParameters()
        {
            bool isMoving = movementVector != UnityEngine.Vector2.zero;

            animator.SetBool("isMoving", isMoving);

            if (isMoving)
            {
                animator.SetFloat("X", movementVector.x);
                animator.SetFloat("Y", movementVector.y);

                lastMoveDirection = movementVector;
            }
            else
            {
                animator.SetFloat("X", lastMoveDirection.x);
                animator.SetFloat("Y", lastMoveDirection.y);
            }
        }
    }
}
