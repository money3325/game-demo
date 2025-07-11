using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ItemInteraction : MonoBehaviour
{
    public string[][] dialogues; // �洢����Ի���ÿ��Ի���������ı�
    public GameObject infoPanel; // ������ʾ�ı������
    public TextMeshProUGUI displayText; // ��ʾ�ı��� UI Text ���
    private int currentDialogueIndex = 0; // ��ǰ��ʾ�ĶԻ�������
    private int currentTextIndex = 0; // ��ǰ��ʾ�ĶԻ������ı�����
    private int currentCharIndex = 0; // ��ǰ��ʾ�ַ�������
    private bool isTextFullyDisplayed = false; // �ı��Ƿ�����ȫ��ʾ
    private bool isAllDialoguesFinished = false; // ���жԻ��Ƿ������
    private Coroutine typingCoroutine; // ����Э��

    private void Start()
    {
        if (infoPanel == null)
        {
            Debug.LogError("infoPanel δ��ֵ������ Inspector ��������á�");
        }
        if (displayText == null)
        {
            Debug.LogError("displayText δ��ֵ������ Inspector ��������á�");
        }
        infoPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ������������
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                if (!infoPanel.activeSelf)
                {
                    if (isAllDialoguesFinished)
                    {
                        return; // ������жԻ�����ɣ�����޷�Ӧ
                    }
                    currentDialogueIndex = 0;
                    currentTextIndex = 0;
                    ShowInteractionText();
                }
                else if (isTextFullyDisplayed)
                {
                    if (currentTextIndex < dialogues[currentDialogueIndex].Length - 1)
                    {
                        currentTextIndex++;
                        ShowInteractionText();
                    }
                    else if (currentDialogueIndex < dialogues.Length - 1)
                    {
                        currentDialogueIndex++;
                        currentTextIndex = 0;
                        ShowInteractionText();
                    }
                    else
                    {
                        isAllDialoguesFinished = true;
                        HideInteractionText();
                    }
                }
                else
                {
                    if (typingCoroutine != null)
                    {
                        StopCoroutine(typingCoroutine);
                    }
                    displayText.text = dialogues[currentDialogueIndex][currentTextIndex];
                    isTextFullyDisplayed = true;
                }
            }
            else if (infoPanel.activeSelf && isTextFullyDisplayed)
            {
                HideInteractionText();
            }
        }
    }

    private void ShowInteractionText()
    {
        if (dialogues == null || dialogues.Length == 0)
        {
            Debug.LogError("dialogues ����δ��ʼ�������� Inspector ��������á�");
            return;
        }
        if (currentDialogueIndex < 0 || currentDialogueIndex >= dialogues.Length)
        {
            Debug.LogError($"��ǰ�Ի������� {currentDialogueIndex} ���� dialogues ���鷶Χ��");
            return;
        }
        if (dialogues[currentDialogueIndex] == null || dialogues[currentDialogueIndex].Length == 0)
        {
            Debug.LogError($"��ǰ�Ի��� {currentDialogueIndex} Ϊ�գ����� Inspector ��������á�");
            return;
        }
        if (currentTextIndex < 0 || currentTextIndex >= dialogues[currentDialogueIndex].Length)
        {
            Debug.LogError($"��ǰ�ı����� {currentTextIndex} ������ǰ�Ի��� {currentDialogueIndex} �ķ�Χ��");
            return;
        }

        infoPanel.SetActive(true);
        currentCharIndex = 0;
        isTextFullyDisplayed = false;
        displayText.text = "";
        typingCoroutine = StartCoroutine(TypeText(dialogues[currentDialogueIndex][currentTextIndex]));
    }

    private void HideInteractionText()
    {
        infoPanel.SetActive(false);
        isTextFullyDisplayed = false;
    }

    private IEnumerator TypeText(string text)
    {
        currentCharIndex = 0;
        while (currentCharIndex < text.Length)
        {
            displayText.text += text[currentCharIndex];
            currentCharIndex++;
            yield return new WaitForSeconds(0.1f); // ÿ���ַ���ʾ��� 0.1 ��
        }
        isTextFullyDisplayed = true;
    }
}
