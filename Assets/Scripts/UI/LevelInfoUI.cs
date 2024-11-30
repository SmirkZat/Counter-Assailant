using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfoUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelNumberText;
    [SerializeField] TextMeshProUGUI enemiesInWaveText;
    [SerializeField] TextMeshProUGUI levelCompletedText;

    LevelManager levelManager;

    void Awake()
    {
        levelManager = GetComponent<LevelManager>();
        levelCompletedText.gameObject.SetActive(false);
    }

    void Start()
    {
        levelNumberText.text = LevelLoader.GetCurrentLevel();
    }

    void OnEnable()
    {
        LevelManager.OnLevelCompleted += HandleLevelCompleteUI;
    }

    void OnDisable()
    {
        LevelManager.OnLevelCompleted -= HandleLevelCompleteUI;
    }

    void Update()
    {
        if (levelManager.EnemiesInWave <= 0)
        {
            enemiesInWaveText.text = "0";
        }
        else
        {
            enemiesInWaveText.text = levelManager.EnemiesInWave.ToString();
        }
    }

    void HandleLevelCompleteUI()
    {
        levelCompletedText.gameObject.SetActive(true);
        levelCompletedText.text = UIManager.Instance.GetWaveCompleteText();
    }
}
