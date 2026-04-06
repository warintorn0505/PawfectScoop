using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void LoadCreditScene()
    {
        SceneManager.LoadScene("CreditScene");
    }

    public void LoadTutorialScene()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}