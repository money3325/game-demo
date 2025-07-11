using UnityEngine;

namespace Yarn.Unity.Example
{
    /// <summary>
    /// Attached to the non-player characters, and stores the name of the Yarn
    /// node that should be run when you talk to them.
    /// </summary>
    public class NPC : MonoBehaviour
    {
        public string characterName = "";

        public string talkToNode = "";


          // 碰撞开始事件
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log($"NPC {characterName} 与 {collision.gameObject.name} 发生碰撞");
        }

        // 碰撞持续事件
        private void OnCollisionStay2D(Collision2D collision)
        {
            Debug.Log($"NPC {characterName} 与 {collision.gameObject.name} 持续碰撞");
        }

        // 碰撞结束事件
        private void OnCollisionExit2D(Collision2D collision)
        {
            Debug.Log($"NPC {characterName} 与 {collision.gameObject.name} 结束碰撞");
        }

        // 触发开始事件
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"NPC {characterName} 进入了 {other.gameObject.name} 的触发器");
        }

        // 触发持续事件
     
          private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && 
            Input.GetKeyDown(KeyCode.Space) &&
            DialogueInputHandler.DialogueInteractionEnabled) // 新增检查
            Debug.Log($"NPC {characterName} 处于 {other.gameObject.name} 的触发器内");
        {
            // 触发对话逻辑
            StartDialogue();
        }
    }
     private void StartDialogue()
    {
        // 找到对话管理器并开始对话
        var dialogueRunner = FindObjectOfType<DialogueRunner>();
        if (dialogueRunner != null && !dialogueRunner.IsDialogueRunning)
        {
            dialogueRunner.StartDialogue("ItemDialogue");
        }
    }

        // 触发结束事件
        private void OnTriggerExit2D(Collider2D other)
        {
            Debug.Log($"NPC {characterName} 离开了 {other.gameObject.name} 的触发器");
        }
        
    }
    
}