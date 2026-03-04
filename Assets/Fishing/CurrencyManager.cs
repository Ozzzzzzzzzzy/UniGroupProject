using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public const string CurrencyData = "Fishing.Currency";

    public int Currency = 0;

    private void Awake()
    {
        Currency = PlayerPrefs.GetInt(CurrencyData, 0);
    }

    public void Add(int amount)
    {
        Currency += amount;
        PlayerPrefs.SetInt(CurrencyData, Currency);
        PlayerPrefs.Save();
    }

    public bool TrySpend(int amount)
    {
        if (Currency < amount)
            return false;

        Currency -= amount;
        PlayerPrefs.SetInt(CurrencyData, Currency);
        PlayerPrefs.Save();
        return true;
    }
}
