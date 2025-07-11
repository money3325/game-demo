using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class NewItemInteraction : MonoBehaviour
{
    public string[] dialogues; // 多组对话内容
    public GameObject dialogPanel; // 对话框面板
    public TextMeshProUGUI dialogueText;
    public float letterDelay = 0.1f; // 每个字显示的延迟时间

    private int currentDialogueIndex = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;
    private void Start()
    {
        // 隐藏对话框面板
        dialogPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (currentDialogueIndex < dialogues.Length)
            {
                if (isTyping)
                {
                    // 如果正在打字，停止当前打字过程
                    StopCoroutine(typingCoroutine);
                    dialogueText.text = dialogues[currentDialogueIndex];
                    isTyping = false;
                }
                else
                {
                    // 开启新的打字过程
                    dialogPanel.SetActive(true);
                    typingCoroutine = StartCoroutine(TypeDialogue(dialogues[currentDialogueIndex]));
                    currentDialogueIndex++;
                }
            }
            else
            {
                // 所有对话显示完毕，隐藏对话框
                dialogPanel.SetActive(false);
                currentDialogueIndex = 0;
            }
        }
    }

    IEnumerator TypeDialogue(string dialogue)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(letterDelay);
        }
        isTyping = false;
    }
}
