using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] Button playAgainButton;
    [SerializeField] Button quitButton;

    void OnEnable()
    {
        playAgainButton.onClick.AddListener(GameManager.Instance.RestartGame);
        quitButton.onClick.AddListener(GameManager.Instance.LoadMainMenuScene);
    }
}
