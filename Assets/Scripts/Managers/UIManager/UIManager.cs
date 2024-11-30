using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] Canvas pauseMenuCanvas;
    [SerializeField] Image crosshair;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        ToggleCursorCrosshair(false);
        if (gameOverCanvas != null)
        {
            HideGameOverCanvas();
        }
    }

    void Update()
    {
        if (crosshair.gameObject.activeInHierarchy)
        {
            crosshair.rectTransform.position = Input.mousePosition;
        }
    }

    public void ShowGameOverCanvas()
    {
        ToggleCursorCrosshair(true);
        gameOverCanvas.gameObject.SetActive(true);
    }
    public void HideGameOverCanvas()
    {
        ToggleCursorCrosshair(false);
        gameOverCanvas.gameObject.SetActive(false);
    }

    public void ShowPauseMenuCanvas()
    {
        ToggleCursorCrosshair(true);
        pauseMenuCanvas.gameObject.SetActive(true);
    }
    public void HidePauseMenuCanvas()
    {
        ToggleCursorCrosshair(false);
        pauseMenuCanvas.gameObject.SetActive(false);
    }

    public void ToggleCursorCrosshair(bool showCursor)
    {
        Cursor.visible = showCursor;
        crosshair.gameObject.SetActive(!showCursor);
    }

    public string GetWaveCompleteText()
    {
        int currentLevel = LevelLoader.LevelNumber + 1;
        return $"Wave {currentLevel} Complete!";
    }

    public string GetNextLevelText()
    {
        string currentLevel = LevelLoader.GetCurrentLevel();

        switch (currentLevel)
        {
            case LevelsEnum.LevelOne:
                return "Continue To Level 2";
            case LevelsEnum.LevelTwo:
                return "Continue To Level 3";
            case LevelsEnum.LevelThree:
                return "Continue To Level 4";
            case LevelsEnum.LevelFour:
                return "Continue To Final Level";
            default:
                return "";
        }
    }
}
