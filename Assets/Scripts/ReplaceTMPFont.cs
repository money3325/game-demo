using UnityEngine;
using TMPro;

public class ReplaceTMPFont : MonoBehaviour
{
    [SerializeField]
    private TMP_FontAsset newFont; 

    void Start()
    {
        TMP_Text[] allTMPTexts = FindObjectsOfType<TMP_Text>();
        foreach (TMP_Text tmpText in allTMPTexts)
        {
            tmpText.font = newFont;
        }
    }
}