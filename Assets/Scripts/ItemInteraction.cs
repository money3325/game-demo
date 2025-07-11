using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ItemInteraction : MonoBehaviour
{
    public string[][] dialogues; // 存储多组对话，每组对话包含多句文本
    public GameObject infoPanel; // 用于显示文本的面板
    public TextMeshProUGUI displayText; // 显示文本的 UI Text 组件
    private int currentDialogueIndex = 0; // 当前显示的对话组索引
    private int currentTextIndex = 0; // 当前显示的对话组内文本索引
    private int currentCharIndex = 0; // 当前显示字符的索引
    private bool isTextFullyDisplayed = false; // 文本是否已完全显示
    private bool isAllDialoguesFinished = false; // 所有对话是否已完成
    private Coroutine typingCoroutine; // 打字协程

    private void Start()
    {
        if (infoPanel == null)
        {
            Debug.LogError("infoPanel 未赋值，请在 Inspector 面板中设置。");
        }
        if (displayText == null)
        {
            Debug.LogError("displayText 未赋值，请在 Inspector 面板中设置。");
        }
        infoPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 检测鼠标左键点击
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                if (!infoPanel.activeSelf)
                {
                    if (isAllDialoguesFinished)
                    {
                        return; // 如果所有对话已完成，点击无反应
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
            Debug.LogError("dialogues 数组未初始化，请在 Inspector 面板中设置。");
            return;
        }
        if (currentDialogueIndex < 0 || currentDialogueIndex >= dialogues.Length)
        {
            Debug.LogError($"当前对话组索引 {currentDialogueIndex} 超出 dialogues 数组范围。");
            return;
        }
        if (dialogues[currentDialogueIndex] == null || dialogues[currentDialogueIndex].Length == 0)
        {
            Debug.LogError($"当前对话组 {currentDialogueIndex} 为空，请在 Inspector 面板中设置。");
            return;
        }
        if (currentTextIndex < 0 || currentTextIndex >= dialogues[currentDialogueIndex].Length)
        {
            Debug.LogError($"当前文本索引 {currentTextIndex} 超出当前对话组 {currentDialogueIndex} 的范围。");
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
            yield return new WaitForSeconds(0.1f); // 每个字符显示间隔 0.1 秒
        }
        isTextFullyDisplayed = true;
    }
}
