// Assets/Scripts/ResultSceneManager.cs
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ResultSceneManager : MonoBehaviour
{
    [Header("UI Text")]
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI earnedCoinText;
    public TextMeshProUGUI customerServedText;

    [Header("Stars")]
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    [Header("Buttons")]
    public GameObject nextButton;
    public GameObject retryButton;
    public GameObject homeButton;

    void Start()
    {
        if (LevelManager.Instance == null || MoneyManager.Instance == null)
        {
            Debug.LogWarning("LevelManager หรือ MoneyManager ไม่มี Instance!");

            if (levelText != null) levelText.text = "Level 1";
            if (earnedCoinText != null) earnedCoinText.text = "0 / 100";
            if (customerServedText != null) customerServedText.text = "0";

            SetStars(0);

            if (nextButton != null) nextButton.SetActive(false);
            if (retryButton != null) retryButton.SetActive(true);
            if (homeButton != null) homeButton.SetActive(true);
            return;
        }

        int level = LevelManager.Instance.currentLevel;
        int coins = MoneyManager.Instance.currentCoins;
        int served = MoneyManager.Instance.customerServed;
        int goal = LevelManager.Instance.levelGoals[
            Mathf.Min(level - 1, LevelManager.Instance.levelGoals.Length - 1)];

        int stars = CalculateStars(coins, goal);
        bool passed = stars > 0;

        if (levelText != null)
            levelText.text = "Level " + level;

        if (earnedCoinText != null)
            earnedCoinText.text = coins + " / " + goal;

        if (customerServedText != null)
            customerServedText.text = served.ToString();

        SetStars(stars);

        if (nextButton != null) nextButton.SetActive(passed);
        if (retryButton != null) retryButton.SetActive(!passed);
        if (homeButton != null) homeButton.SetActive(true);
    }

    int CalculateStars(int coins, int goal)
    {
        if (coins < goal) return 0;

        float ratio = (float)coins / goal;

        if (ratio >= 1.5f) return 3;
        if (ratio >= 1.25f) return 2;
        return 1;
    }

    void SetStars(int count)
    {
        if (star1 != null) star1.SetActive(count >= 1);
        if (star2 != null) star2.SetActive(count >= 2);
        if (star3 != null) star3.SetActive(count >= 3);
    }

    public void OnNextPressed()
    {
        MoneyManager.Instance.currentCoins = 0;
        MoneyManager.Instance.customerServed = 0;

        if (LevelManager.Instance.currentLevel == 4)
        {
            SceneManager.LoadScene("WinScene");
        }
        else
        {
            LevelManager.Instance.GoNextLevel();
        }
    }

    public void OnRetryPressed()
    {
        MoneyManager.Instance.currentCoins = 0;
        MoneyManager.Instance.customerServed = 0;
        SceneManager.LoadScene("MainScene");
    }

    public void OnHomePressed()
    {
        if (LevelManager.Instance != null)
            LevelManager.Instance.ResetGame();

        MoneyManager.Instance.currentCoins = 0;
        MoneyManager.Instance.customerServed = 0;
        SceneManager.LoadScene("StartScene");
    }
}