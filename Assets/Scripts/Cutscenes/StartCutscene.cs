using UnityEngine;

public class StartCutscene : MonoBehaviour
{
    void OnEnable()
    {
        Dialog.OnDialogComplete += HandleOnDialogComplete;
        LevelTransitionHandler.OnFadeOutLevelComplete += HandleOnFadeOutLevelComplete;
    }

    void OnDisable()
    {
        Dialog.OnDialogComplete -= HandleOnDialogComplete;
        LevelTransitionHandler.OnFadeOutLevelComplete -= HandleOnFadeOutLevelComplete;
    }

    void HandleOnDialogComplete()
    {
        LevelTransitionHandler.Instance.PlayLevelFadeOutAnimation();
    }

    void HandleOnFadeOutLevelComplete()
    {
        GameManager.Instance.StartGame();
    }
}