using UnityEngine;
using Yarn.Unity;
using System.Collections.Generic;

// 可序列化的面板配置条目（模拟字典的键值对）
[System.Serializable]
public struct PanelEntry
{
    public string panelName;    // 面板名称（键）
    public GameObject panel;    // 面板对象（值）
}

public class YarnPanelController : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;
    
    // 使用可序列化的列表替代字典（Inspector中可直接编辑）
    [SerializeField] private List<PanelEntry> 可控面板 = new List<PanelEntry>();

    private void Awake()
    {
        // 注册Yarn命令
        dialogueRunner.AddCommandHandler<string, bool>("SetPanelActive", HandleSetPanelActive);
        dialogueRunner.AddCommandHandler<string>("ShowPanel", panelName => HandleSetPanelActive(panelName, true));
        dialogueRunner.AddCommandHandler<string>("HidePanel", panelName => HandleSetPanelActive(panelName, false));
    }

    private void HandleSetPanelActive(string panelName, bool isActive)
    {
        // 遍历列表查找目标面板
        foreach (var entry in 可控面板)
        {
            if (entry.panelName == panelName)
            {
                if (entry.panel != null)
                {
                    entry.panel.SetActive(isActive);
                    Debug.Log($"已设置面板 [{panelName}] 状态：{(isActive ? "显示" : "隐藏")}");
                    return; // 找到后直接返回
                }
                else
                {
                    Debug.LogError($"面板 [{panelName}] 的引用为空！请检查Inspector配置");
                    return;
                }
            }
        }

        // 未找到面板的提示
        Debug.LogError($"未找到名为 [{panelName}] 的面板配置！请检查YarnPanelController的[可控面板]列表");
    }

    // 动态添加面板（需调整参数类型）
    public void AddPanelToControl(string panelName, GameObject panelObject)
    {
        // 检查是否已存在同名面板
        foreach (var entry in 可控面板)
        {
            if (entry.panelName == panelName)
            {
                Debug.LogWarning($"已存在名为 [{panelName}] 的面板配置，新配置将覆盖旧配置");
                可控面板.Remove(entry); // 移除旧条目
                break;
            }
        }

        // 添加新条目
        可控面板.Add(new PanelEntry { panelName = panelName, panel = panelObject });
    }
}
    
