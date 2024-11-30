using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Button resumeButton;
    [SerializeField] Button quitButton;

    void OnEnable()
    {
        resumeButton.onClick.AddListener(GameManager.Instance.ResumeGame);
        quitButton.onClick.AddListener(GameManager.Instance.LoadMainMenuScene);
    }
}
