using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private List<GameObject> levelSets = new List<GameObject>();
    [SerializeField] private List<LevelData> levelData = new List<LevelData>();

    private GameObject levelInstance;
    public GameObject LevelInstance => levelInstance;
    public LevelData CurrentLevelData => levelData[Level];

    public List<GameObject> Levels => levelSets;

    public int Level
    {
        get => PlayerPrefsManager.Instance.PlayerData.Level;
        set => PlayerPrefsManager.Instance.PlayerData.Level = (value < Levels.Count && value > 0) ? value : Mathf.Clamp(value, 0, Levels.Count - 1);
    }

    private void Start()
    {
        InitLevel(Level);
    }

    private void InitLevel(int levelIndex)
    {
        levelInstance = Instantiate(Levels[levelIndex]);

        if (!levelInstance.activeSelf)
            levelInstance.SetActive(true);
    }

    private void DestroyCurrentLevel()
    {
        if (levelInstance)
        {
            Destroy(levelInstance);
        }
    }

    public void RestartLevel()
    {
        DestroyCurrentLevel();
        InitLevel(Level);
    }

    public void NextLevel()
    {
        Level++;
        RestartLevel();
    }

    public void PreLevel()
    {
        Level--;
        RestartLevel();
    }
}
