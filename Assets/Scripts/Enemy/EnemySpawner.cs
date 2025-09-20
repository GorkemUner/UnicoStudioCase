using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float enemySpawnInterval;

    List<EnemyData> enemyDataToSpawnList = new List<EnemyData>();

    private int enemyTotalDieCountMustBeSuccessLevel;
    public int EnemyTotalDieCountMustBeSuccessLevel
    {
        get => enemyTotalDieCountMustBeSuccessLevel;
        set => enemyTotalDieCountMustBeSuccessLevel = value;
    }

    private int currentTotalDie;
    public int CurrentTotalDie
    {
        get => currentTotalDie;
        set => currentTotalDie = value;
    }

    private void Awake()
    {
        FillAndShuffleSpawnEnemies();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameBattleStart.AddListener(BeginSpawn);
    }

    private void OnDisable()
    {
        GameManager.Instance?.OnGameBattleStart.RemoveListener(BeginSpawn);
    }


    private void BeginSpawn()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private void FillAndShuffleSpawnEnemies()
    {
        foreach (var item in LevelManager.Instance.CurrentLevelData.enemiesToSpawn)
        {
            for (int i = 0; i < item.count; i++)
            {
                enemyDataToSpawnList.Add(item.enemyData);
            }
        }

        EnemyTotalDieCountMustBeSuccessLevel = enemyDataToSpawnList.Count;
        MathUtils.Shuffle(enemyDataToSpawnList);
    }

    private IEnumerator SpawnCoroutine()
    {
        foreach (var enemyData in enemyDataToSpawnList)
        {
            var temp = EnemyGrids.Instance.GetRandomEmptyGridAtFirstLine();
            if (temp == null)
            {
                yield return new WaitUntil(EnemyGrids.Instance.IsAnyEmptyFirstLineToSpawnEnemy);
                temp = EnemyGrids.Instance.GetRandomEmptyGridAtFirstLine();
            }
   
            temp.IsEmpty = false;
            var enemy = Instantiate(enemyPrefab, temp.transform).GetComponent<Enemy>();
            enemy.Init(temp, this, enemyData);
            yield return new WaitForSeconds(enemySpawnInterval);
        }
    }
}