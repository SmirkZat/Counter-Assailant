using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static Action OnLevelCompleted;
    public int EnemiesInWave => enemiesInWave;

    [SerializeField] int enemiesInWave = 10;

    public static string LevelNumber;

    void Awake()
    {
        if (LevelNumber == null)
        {
            LevelNumber = LevelLoader.GetCurrentLevel();
        }
    }

    void OnEnable()
    {
        OnLevelCompleted += SetLevelCompleteState;
    }

    void OnDisable()
    {
        OnLevelCompleted -= SetLevelCompleteState;
    }

    void SetLevelCompleteState()
    {
        GameManager.Instance.SetGameState(GameManager.GameState.LevelComplete);
    }

    public void DecreaseEnemiesInWave()
    {
        enemiesInWave--;

        if (enemiesInWave <= 0)
        {
            Debug.Log("Level completed");
            OnLevelCompleted?.Invoke();
        }
    }
}
