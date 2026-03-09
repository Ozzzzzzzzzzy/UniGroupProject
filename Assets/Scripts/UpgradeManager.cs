using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public const string UpgradeData = "Fishing.Upgrade";

    public const int BaseUpgradeCost = 100;
    public const float MultiplierGrowth = 1.30f;
    public const float CostGrowth = 1.55f;

    [SerializeField] private int baitUpgradeLevel = 1;
    public int BaitUpgradeLevel => baitUpgradeLevel;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(UpgradeData))
        {
            PlayerPrefs.SetInt(UpgradeData, baitUpgradeLevel);
            PlayerPrefs.Save();
        }

        baitUpgradeLevel = PlayerPrefs.GetInt(UpgradeData, baitUpgradeLevel);
    }

    public void UpgradeBait()
    {
        baitUpgradeLevel++;
        PlayerPrefs.SetInt(UpgradeData, baitUpgradeLevel);
        PlayerPrefs.Save();
    }

    public static int LoadBaitUpgradeLevel()
    {
        return PlayerPrefs.GetInt(UpgradeData, 1);
    }

    public static float GetBaitMultiplier(int level)
    {
        level = Mathf.Max(1, level);
        return Mathf.Pow(MultiplierGrowth, level - 1);
    }

    public static int GetBaitUpgradeCost(int currentLevel)
    {
        currentLevel = Mathf.Max(1, currentLevel);
        return Mathf.RoundToInt(BaseUpgradeCost * Mathf.Pow(CostGrowth, currentLevel - 1));
    }



    // debugging. ngl i have no clue how this works, i used copilot ngl





    [ContextMenu("Debug/Log Bait Upgrade (Prefs + Runtime)")]
    private void DebugLogBaitUpgrade()
    {
        int saved = PlayerPrefs.GetInt(UpgradeData, -999);
        Debug.Log($"[UpgradeManager] RuntimeLevel={baitUpgradeLevel}, SavedLevel={saved}, HasKey={PlayerPrefs.HasKey(UpgradeData)}");
    }

    [ContextMenu("Debug/Reset Bait Upgrade to 1 (Prefs)")]
    private void DebugResetBaitUpgradeTo1()
    {
        baitUpgradeLevel = 1;
        PlayerPrefs.SetInt(UpgradeData, baitUpgradeLevel);
        PlayerPrefs.Save();
        Debug.Log("[UpgradeManager] Reset bait upgrade to level 1.");
    }

    [ContextMenu("Debug/Overwrite Prefs with Inspector Value")]
    private void DebugOverwritePrefsWithInspectorValue()
    {
        PlayerPrefs.SetInt(UpgradeData, baitUpgradeLevel);
        PlayerPrefs.Save();
        Debug.Log($"[UpgradeManager] Overwrote prefs with inspector/runtime level: {baitUpgradeLevel}");
    }

    [ContextMenu("Debug/Delete Bait Upgrade Key (Prefs)")]
    private void DebugDeleteBaitUpgradeKey()
    {
        PlayerPrefs.DeleteKey(UpgradeData);
        PlayerPrefs.Save();
        Debug.Log("[UpgradeManager] Deleted PlayerPrefs key Fishing.Upgrade.");
    }
}


