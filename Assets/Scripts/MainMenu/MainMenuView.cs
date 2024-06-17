using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    public Button startGameButton;
    public Button testSceneButton;
    public Button exitButton;

    private MainMenuViewModel viewModel;

    void Start()
    {
        viewModel = new MainMenuViewModel();

        startGameButton.onClick.AddListener(viewModel.StartGame);
        testSceneButton.onClick.AddListener(viewModel.TestScene);
        exitButton.onClick.AddListener(viewModel.ExitGame);
    }
}
