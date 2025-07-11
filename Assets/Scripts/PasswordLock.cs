using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class PasswordLock : MonoBehaviour
{
    public string correctPassword = "12345"; // 修改为五位数密码
    public InputField passwordInput;
    public GameObject passwordPanel;
    public Warp1 warpScript;
    public Transform playerTransform;
    public DialogueRunner dialogueRunner; // 引用 Yarn Spinner 的 DialogueRunner

    private void Start()
    {
        passwordPanel.SetActive(false);
        passwordInput.characterLimit = 5; // 设置输入框最大输入长度为 5
    }

    // 处理数字按钮点击事件
    public void OnNumberButtonClick(string number)
    {
        if (passwordInput.text.Length < 5) // 确保输入不超过 5 位
        {
            passwordInput.text += number;
        }
    }

    // 处理重置按钮点击事件
    public void OnResetButtonClick()
    {
        passwordInput.text = "";
    }

    // 处理返回按钮点击事件
    public void OnBackButtonClick()
    {
        passwordPanel.SetActive(false);
        OnResetButtonClick(); // 清空输入框
    }

    public void CheckPassword()
    {
        if (passwordInput.text == correctPassword)
        {
            passwordPanel.SetActive(false);
            warpScript.EnableWarp();
            if (playerTransform != null && warpScript.warpTarget != null)
            {
                playerTransform.position = warpScript.warpTarget.position;
            }
        }
        else
        {
            // 密码错误，启动 Yarn 对话
            if (dialogueRunner != null)
            {
                dialogueRunner.StartDialogue("PasswordError");
            }
            OnResetButtonClick(); // 密码错误时重置输入框
        }
    }
}    