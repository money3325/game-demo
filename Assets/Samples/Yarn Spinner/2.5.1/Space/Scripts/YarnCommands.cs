using UnityEngine;
using Yarn.Unity;

public class YarnCommands : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner; // 引用 DialogueRunner
    public GameObject passwordPanel; // 密码锁面板

    private void Awake()
    {
        // 注册 Yarn 命令：ShowPasswordLock（无参数）
        dialogueRunner.AddCommandHandler("ShowPasswordLock", ShowPasswordLock);
    }

    // 命令处理函数（无参数，与 Yarn 脚本中的 <<ShowPasswordLock>> 对应）
    private void ShowPasswordLock()
    {
        if (passwordPanel != null)
        {
            passwordPanel.SetActive(true); // 显示密码锁面板
        }
        else
        {
            Debug.LogError("Password panel reference is not set in YarnCommands!");
        }
    }
}
