// Assets/Scripts/LevelManager.cs
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public int currentLevel = 1;
    public int[] levelGoals = { 100, 200, 350, 500 };

    [Header("Timer")]
    public float[] levelDurations = { 120f, 100f, 90f, 80f };
    private float timeLeft;
    private bool ended = false;

    [Header("UI")]
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI timerText;

    void Awake()
{
    if (Instance == null)
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    else if (Instance != this)
    {
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
        ended    = false;
        int index = Mathf.Min(currentLevel - 1, levelDurations.Length - 1);
        timeLeft  = levelDurations[index];

        // หา UI ใหม่ใน scene
        levelText = GameObject.Find("LevelText")?.GetComponent<TextMeshProUGUI>();
        timerText = GameObject.Find("TimerText")?.GetComponent<TextMeshProUGUI>();

        UpdateUI();
        Debug.Log("Scene loaded! Level: " + currentLevel);
    }
}

void Start()
{
    // ถ้าเป็น instance แรก (เพิ่งสร้าง) ให้ setup ครั้งแรก
    if (Instance == this)
    {
        ended    = false;
        int index = Mathf.Min(currentLevel - 1, levelDurations.Length - 1);
        timeLeft  = levelDurations[index];
        UpdateUI();
    }
}

    void Update()
    {
        if (ended) return;

        timeLeft -= Time.deltaTime;
        timeLeft  = Mathf.Max(0, timeLeft);

        if (timerText != null)
            timerText.text = Mathf.CeilToInt(timeLeft).ToString();

        if (timeLeft <= 0f)
            EndLevel();
    }

    public void CheckGoal(int coins) { }

    void EndLevel()
    {
        if (ended) return;
        ended = true;
        SceneManager.LoadScene("ResultScene");
    }

    public void GoNextLevel()
{
    currentLevel++;  // ← เพิ่ม level
    if (currentLevel > levelGoals.Length)
        SceneManager.LoadScene("WinScene");
    else
        SceneManager.LoadScene("MainScene");
}

    public void ResetGame()
    {
        currentLevel = 1;
    }

    void UpdateUI()
    {
        if (levelText != null)
            levelText.text = "Level " + currentLevel;
        else
            Debug.LogWarning("LevelText not found!");
    }
}