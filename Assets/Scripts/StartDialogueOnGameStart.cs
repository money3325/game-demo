using UnityEngine;
using Yarn.Unity;

public class StartDialogueOnGameStart : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public string startNode = "StartConversation";

    void Start()
    {
        if (dialogueRunner != null && dialogueRunner.IsDialogueRunning == false)
        {
            dialogueRunner.StartDialogue(startNode);
        }
    }
}
