using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public const string UpgradeData = "Fishing.Upgrade";

    [SerializeField] private int baitUpgradeLevel = 3;
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
}


