using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGrids : Singleton<EnemyGrids>
{
    [SerializeField] private List<EnemyGrid> firstLineEnemyGrids;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;

    private List<EnemyGrid> GetEmptyGridsAtFirstLine()
    {
        List<EnemyGrid> result = new List<EnemyGrid>();
        foreach (var grid in firstLineEnemyGrids)
        {
            if (grid.IsEmpty)
                result.Add(grid);
        }

        return result;
    }

    public EnemyGrid GetRandomEmptyGridAtFirstLine()
    {
        var emptyGrids = GetEmptyGridsAtFirstLine();
        if (emptyGrids.Count == 0)
            return null;

        int randEnemyGrid = Random.Range(0, emptyGrids.Count);
        return emptyGrids[randEnemyGrid];
    }

    public bool IsAnyEmptyFirstLineToSpawnEnemy()
    {
        return GetEmptyGridsAtFirstLine().Count != 0;
    }

    public float GetDistanceBetweenCellsHorizontal()
    {
        return gridLayoutGroup.cellSize.y + gridLayoutGroup.spacing.y; 
    }
}