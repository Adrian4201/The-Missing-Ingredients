using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    /*
    public static MoneyManager Instance { get; private set; }

    [Header("Config")]
    [SerializeField] private int startingMoney = 100;
    [SerializeField] private bool usePlayerPrefs = true;

    public event Action<int> OnMoneyChanged;

    private int _money;
    private const string PlayerPrefsKey = "player_money";

    public int CurrentMoney => _money;

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Load();
    }

    public void AddMoney(int amount, string reason = null)
    {
        if (amount == 0) return;
        // update money
        _money += amount;
        // notify UI / listeners
        OnMoneyChanged?.Invoke(_money);
        Save();

        if (amount > 0)
            Debug.Log($"[MoneyManager] +{amount} ({reason}) -> {_money}");
        else
            Debug.Log($"[MoneyManager] {amount} ({reason}) -> {_money}");
    }

    // Attempts to spend; returns true on success
    public bool TrySpend(int amount)
    {
        if (amount <= 0) { Debug.LogWarning("[MoneyManager] TrySpend called with <= 0"); return true; }
        if (_money >= amount)
        {
            AddMoney(-amount, "Spend");
            return true;
        }
        // not enough money
        return false;
    }

    public void SetMoney(int amount)
    {
        _money = amount;
        OnMoneyChanged?.Invoke(_money);
        Save();
    }

    private void Save()
    {
        if (usePlayerPrefs)
        {
            PlayerPrefs.SetInt(PlayerPrefsKey, _money);
            PlayerPrefs.Save();
        }
        else
        {
            var sd = JsonUtility.ToJson(new SaveData { money = _money });
            File.WriteAllText(Path.Combine(Application.persistentDataPath, "save.json"), sd);
        }
    }

    private void Load()
    {
        if (usePlayerPrefs)
        {
            if (PlayerPrefs.HasKey(PlayerPrefsKey))
                _money = PlayerPrefs.GetInt(PlayerPrefsKey);
            else
                _money = startingMoney;
        }
        else
        {
            var path = Path.Combine(Application.persistentDataPath, "save.json");
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                var data = JsonUtility.FromJson<SaveData>(json);
                _money = data.money;
            }
            else _money = startingMoney;
        }

        OnMoneyChanged?.Invoke(_money);
    }

    [Serializable]
    private class SaveData { public int money; }
    */

}
