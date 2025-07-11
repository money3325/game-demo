using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class NewItemInteraction : MonoBehaviour
{
    public string[] dialogues; // ����Ի�����
    public GameObject dialogPanel; // �Ի������
    public TextMeshProUGUI dialogueText;
    public float letterDelay = 0.1f; // ÿ������ʾ���ӳ�ʱ��

    private int currentDialogueIndex = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;
    private void Start()
    {
        // ���ضԻ������
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
                    // ������ڴ��֣�ֹͣ��ǰ���ֹ���
                    StopCoroutine(typingCoroutine);
                    dialogueText.text = dialogues[currentDialogueIndex];
                    isTyping = false;
                }
                else
                {
                    // �����µĴ��ֹ���
                    dialogPanel.SetActive(true);
                    typingCoroutine = StartCoroutine(TypeDialogue(dialogues[currentDialogueIndex]));
                    currentDialogueIndex++;
                }
            }
            else
            {
                // ���жԻ���ʾ��ϣ����ضԻ���
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
