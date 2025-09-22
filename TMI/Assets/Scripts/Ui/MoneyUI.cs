using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText = null;
    [SerializeField] private Image coinIcon = null; // optional, in case you want to swap icons

    private void Start()
    {
        if (MoneyManager.Instance != null)
        {
            MoneyManager.Instance.OnMoneyChanged += UpdateUI;
            Debug.Log("Current money at start: " + MoneyManager.Instance.CurrentMoney);
            UpdateUI(MoneyManager.Instance.CurrentMoney);
        }
    }

    private void OnDestroy()
    {
        if (MoneyManager.Instance != null)
            MoneyManager.Instance.OnMoneyChanged -= UpdateUI;
    }

    private void UpdateUI(int money)
    {
        if (moneyText != null)
            moneyText.text = money.ToString("N0"); // shows 1,234 style
    }
    private void Awake()
    {
        Debug.Log("MoneyUI Awake called");
    }

}
