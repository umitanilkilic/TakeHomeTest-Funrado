using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuViewModel
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void TestScene()
    {
        SceneManager.LoadScene("TestScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
