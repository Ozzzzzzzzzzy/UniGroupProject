using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public const string UpgradeData = "Fishing.Upgrade";
    public const string TimeUpgradeData = "Fishing.TimeUpgrade";

    public const int BaseUpgradeCost = 100;
    public const float MultiplierGrowth = 1.30f;
    public const float CostGrowth = 1.55f;

    private const int SecondsPerTimeUpgrade = 2;

    [SerializeField] private int baitUpgradeLevel = 1;
    public int BaitUpgradeLevel => baitUpgradeLevel;

    [SerializeField] private int timeUpgradeLevel = 1;
    public int TimeUpgradeLevel => timeUpgradeLevel;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(UpgradeData))
        {
            PlayerPrefs.SetInt(UpgradeData, baitUpgradeLevel);
            PlayerPrefs.Save();
        }

        baitUpgradeLevel = PlayerPrefs.GetInt(UpgradeData, baitUpgradeLevel);

        if (!PlayerPrefs.HasKey(TimeUpgradeData))
        {
            PlayerPrefs.SetInt(TimeUpgradeData, timeUpgradeLevel);
            PlayerPrefs.Save();
        }

        timeUpgradeLevel = PlayerPrefs.GetInt(TimeUpgradeData, timeUpgradeLevel);
    }

    public void UpgradeBait()
    {
        baitUpgradeLevel++;
        PlayerPrefs.SetInt(UpgradeData, baitUpgradeLevel);
        PlayerPrefs.Save();
    }

    public void UpgradeTime()
    {
        timeUpgradeLevel++;
        PlayerPrefs.SetInt(TimeUpgradeData, timeUpgradeLevel);
        PlayerPrefs.Save();
    }

    public static int LoadBaitUpgradeLevel()
    {
        return PlayerPrefs.GetInt(UpgradeData, 1);
    }

    public static int LoadTimeUpgradeLevel()
    {
        return PlayerPrefs.GetInt(TimeUpgradeData, 1);
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

    public static int GetTimeUpgradeCost(int currentLevel)
    {
        currentLevel = Mathf.Max(1, currentLevel);
        return Mathf.RoundToInt(BaseUpgradeCost * Mathf.Pow(CostGrowth, currentLevel - 1));
    }

    public static int GetExtraFishingSeconds(int timeUpgradeLevel)
    {
        timeUpgradeLevel = Mathf.Max(1, timeUpgradeLevel);
        return (timeUpgradeLevel - 1) * SecondsPerTimeUpgrade;
    }

    [ContextMenu("Debug/Log Upgrades (Prefs + Runtime)")]
    private void DebugLogUpgrades()
    {
        int baitSaved = PlayerPrefs.GetInt(UpgradeData, -999);
        int timeSaved = PlayerPrefs.GetInt(TimeUpgradeData, -999);
        Debug.Log($"[UpgradeManager] Bait Runtime={baitUpgradeLevel}, Saved={baitSaved} | Time Runtime={timeUpgradeLevel}, Saved={timeSaved}");
    }

    [ContextMenu("Debug/Reset Bait Upgrade to 1 (Prefs)")]
    private void DebugResetBaitUpgradeTo1()
    {
        baitUpgradeLevel = 1;
        PlayerPrefs.SetInt(UpgradeData, baitUpgradeLevel);
        PlayerPrefs.Save();
        Debug.Log("[UpgradeManager] Reset bait upgrade to level 1.");
    }

    [ContextMenu("Debug/Reset Time Upgrade to 1 (Prefs)")]
    private void DebugResetTimeUpgradeTo1()
    {
        timeUpgradeLevel = 1;
        PlayerPrefs.SetInt(TimeUpgradeData, timeUpgradeLevel);
        PlayerPrefs.Save();
        Debug.Log("[UpgradeManager] Reset time upgrade to level 1.");
    }

    [ContextMenu("Debug/Delete Upgrade Keys (Prefs)")]
    private void DebugDeleteUpgradeKeys()
    {
        PlayerPrefs.DeleteKey(UpgradeData);
        PlayerPrefs.DeleteKey(TimeUpgradeData);
        PlayerPrefs.Save();
        Debug.Log("[UpgradeManager] Deleted PlayerPrefs keys for upgrades.");
    }
}


