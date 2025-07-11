using UnityEngine;
using Yarn.Unity;

public class DialogueInputHandler : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private KeyCode advanceKey = KeyCode.Space;
    
    // 新增：控制全局交互状态
    public static bool DialogueInteractionEnabled = true;
    
    private bool isDialogueRunning = false;
    private bool waitingForNextInteraction = false;

    private void Start()
    {
        if (dialogueRunner != null)
        {
            dialogueRunner.onDialogueStart.AddListener(OnDialogueStart);
            dialogueRunner.onDialogueComplete.AddListener(OnDialogueComplete);
        }
    }

    private void Update()
    {
        // 仅在对话交互启用时处理输入
        if (DialogueInteractionEnabled && Input.GetKeyDown(advanceKey))
        {
            if (dialogueRunner.IsDialogueRunning)
            {
                dialogueRunner.OnViewRequestedInterrupt();
            }
            else if (waitingForNextInteraction)
            {
                // 对话结束后，等待用户再次按下空格
                waitingForNextInteraction = false;
                DialogueInteractionEnabled = true; // 重新启用全局交互
            }
        }
    }

    private void OnDialogueStart()
    {
        isDialogueRunning = true;
        waitingForNextInteraction = false;
    }

    private void OnDialogueComplete()
    {
        isDialogueRunning = false;
        waitingForNextInteraction = true;
        DialogueInteractionEnabled = false; // 禁用全局交互，防止触发区域重复响应
    }
}