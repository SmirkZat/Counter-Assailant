using UnityEngine.SceneManagement;

public static class LevelLoader
{
    static readonly string[] GamePlayLevels = { LevelsEnum.LevelOne, LevelsEnum.LevelTwo, LevelsEnum.LevelThree, LevelsEnum.LevelFour, LevelsEnum.BossLevel };
    static int currentLevelIndex = 0;

    public static int LevelNumber => currentLevelIndex;

    public static void LoadNextLevel()
    {
        currentLevelIndex++;

        if (currentLevelIndex < GamePlayLevels.Length)
        {
            SceneManager.LoadScene(GamePlayLevels[currentLevelIndex]);
        }
        else
        {
            SceneManager.LoadScene(LevelsEnum.BossLevel);
        }
    }

    public static void LoadMainMenuScene()
    {
        SceneManager.LoadScene(LevelsEnum.MainMenu);
    }

    public static void LoadStoreLevel()
    {
        SceneManager.LoadScene(LevelsEnum.Store);
    }

    public static void LoadStartCutscene()
    {
        SceneManager.LoadScene(LevelsEnum.StartCutscene);
    }

    public static void LoadEndCutscene()
    {
        SceneManager.LoadScene(LevelsEnum.EndCutSecene);
    }

    public static void LoadCredits()
    {
        SceneManager.LoadScene(LevelsEnum.CreditsScene);
    }

    public static void StartFromFirstLevel()
    {
        currentLevelIndex = 0;
        SceneManager.LoadScene(GamePlayLevels[currentLevelIndex]);
    }

    public static string GetCurrentLevel()
    {
        return GamePlayLevels[currentLevelIndex];
    }
}
