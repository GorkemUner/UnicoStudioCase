using UnityEngine;

public class DefenceMenuItemCreator : MonoBehaviour
{
    void Start()
    {
        SpawnDefenceItem();
    }

    public void SpawnDefenceItem()
    {
        foreach (var item in LevelManager.Instance.CurrentLevelData.defenceMenuItemsToSpawn)
        {
            Instantiate(item.defenceMenuItemGO, transform).GetComponent<DefenceMenuItem>().Amount = item.count;
        }
    }
}