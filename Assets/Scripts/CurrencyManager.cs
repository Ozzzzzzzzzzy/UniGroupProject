using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    public const string CurrencyData = "Fishing.Currency";
    [SerializeField] private TextMeshProUGUI CurrencyText;

    public int Currency = 0;

    private void Awake()
    {
        Currency = PlayerPrefs.GetInt(CurrencyData, 0);
        UpdateCurrencyText();
    }

    public void Add(int amount)
    {
        Currency += amount;
        PlayerPrefs.SetInt(CurrencyData, Currency);
        PlayerPrefs.Save();
        UpdateCurrencyText();   
    }

    public bool TrySpend(int amount)
    {
        if (Currency < amount)
            return false;

        Currency -= amount;
        PlayerPrefs.SetInt(CurrencyData, Currency);
        PlayerPrefs.Save();
        UpdateCurrencyText ();
        return true;
    }

    private void UpdateCurrencyText()
    { 
        CurrencyText.text = "Money: " + Currency;
    }
}
