using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawnData
{
    public int count;
    public EnemyData enemyData;
}

[System.Serializable]
public class DefenceItemSpawnData
{
    public int count;
    public GameObject defenceMenuItemGO;
}

[CreateAssetMenu(menuName = "BoardDefence/EnemyData")]
public class EnemyData : ScriptableObject
{
    public Sprite enemySprite;
    public int health;
    public float moveDuration;
}

[CreateAssetMenu(menuName = "BoardDefence/LevelData")]
public class LevelData : ScriptableObject
{
    public List<DefenceItemSpawnData> defenceMenuItemsToSpawn;
    public List<EnemySpawnData> enemiesToSpawn;
}