using TMPro;
using UnityEngine;

public class XPManager : MonoBehaviour
{
    public static XPManager Instance;
    public TextMeshProUGUI xpText;
    private int currentXP = 120;

    void Awake()
    {
        Instance = this;
        UpdateXPDisplay();
    }

    public void AddXP(int amount)
    {
        currentXP += amount;
        UpdateXPDisplay();        
    }

    void UpdateXPDisplay()
    {
        xpText.text = $"XP: {currentXP}";
    }
}