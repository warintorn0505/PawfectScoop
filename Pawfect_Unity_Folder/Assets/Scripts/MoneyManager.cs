// Assets/Scripts/MoneyManager.cs
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;

    public int currentCoins   = 0;
    public int customerServed = 0;

    [Header("UI")]
    public TextMeshProUGUI moneyText;

    void Awake()
{
    if (Instance == null)
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    else if (Instance != this)
    {
        // copy ค่าสำคัญก่อนทำลาย
        Destroy(gameObject);
    }
}

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainScene")
        {
            // Reset coins และหา UI ใหม่
            currentCoins   = 0;
            customerServed = 0;
            moneyText = GameObject.Find("MoneyText")?.GetComponent<TextMeshProUGUI>();
            UpdateUI();
        }
    }

    void Start()
    {
        currentCoins   = 0;
        customerServed = 0;
        UpdateUI();
    }

    public void AddCoins(int amount)
    {
        currentCoins = Mathf.Max(0, currentCoins + amount);
        if (amount > 0) customerServed++;
        Debug.Log("Coins: " + currentCoins + " | Served: " + customerServed);
        UpdateUI();
    }

    void UpdateUI()
    {
        if (moneyText != null)
            moneyText.text = currentCoins + " / " +
                LevelManager.Instance.levelGoals[
                    Mathf.Min(LevelManager.Instance.currentLevel - 1,
                    LevelManager.Instance.levelGoals.Length - 1)];
        else
            Debug.LogWarning("MoneyText not found!");
    }
}