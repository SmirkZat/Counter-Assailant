using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    void OnEnable()
    {
        LevelTransitionHandler.OnFadeOutLevelComplete += LoadCutscene;
    }

    void OnDisable()
    {
        LevelTransitionHandler.OnFadeOutLevelComplete -= LoadCutscene;
    }

    public void OnStartGame()
    {
        LevelTransitionHandler.Instance.PlayLevelFadeOutAnimation();
    }

    public void LoadCutscene()
    {
        GameManager.Instance.LoadStartCutscene();
    }
}
