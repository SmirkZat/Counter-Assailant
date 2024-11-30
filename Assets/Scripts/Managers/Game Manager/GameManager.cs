using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Playing,
        Pause,
        GameOver,
        LevelComplete
    }

    public static GameManager Instance;
    public InventoryManager Inventory => inventoryManager;
    public AmmoManager AmmoManager => ammoManager;
    public AudioManager AudioManager => audioManager;

    [SerializeField] InventoryManager inventoryManager;
    [SerializeField] AmmoManager ammoManager;
    [SerializeField] AudioManager audioManager;

    public GameState State { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Init();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && State != GameState.GameOver)
        {
            if (State == GameState.Playing)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    void Init()
    {
        State = GameState.Playing;
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    public void SetGameState(GameState newState)
    {
        State = newState;
    }

    public void LoadNextLevel()
    {
        SetGameState(GameState.Playing);
        LevelLoader.LoadNextLevel();
    }

    public void GoToStore()
    {
        LevelLoader.LoadStoreLevel();
    }

    void PauseGame()
    {
        SetGameState(GameState.Pause);
        Time.timeScale = 0;
        UIManager.Instance.ShowPauseMenuCanvas();
    }

    public void ResumeGame()
    {
        SetGameState(GameState.Playing);
        Time.timeScale = 1;
        UIManager.Instance.HidePauseMenuCanvas();
    }

    void ResetPersistance()
    {
        if (ScenePersist.Instance)
        {
            ScenePersist.Instance?.ResetScenePersist();
        }
    }

    public void GameOver()
    {
        SetGameState(GameState.GameOver);
        UIManager.Instance.ShowGameOverCanvas();
    }

    public void RestartGame()
    {
        ResetPersistance();
        StartGame();
    }

    public void LoadMainMenuScene()
    {
        ResetPersistance();
        LevelLoader.LoadMainMenuScene();
    }
    public void LoadStartCutscene()
    {
        LevelLoader.LoadStartCutscene();
    }
    public void LoadEndCutscene()
    {
        ResetPersistance();
        LevelLoader.LoadEndCutscene();
    }

    public void LoadCredits()
    {
        LevelLoader.LoadCredits();
    }

    public void StartGame()
    {
        LevelLoader.StartFromFirstLevel();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
