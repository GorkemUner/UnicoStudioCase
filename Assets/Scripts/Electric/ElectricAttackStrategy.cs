using UnityEngine;

public class ElectricAttackStrategy : IAttackStrategy
{
    private GameObject electricPrefab;

    public ElectricAttackStrategy(GameObject prefab)
    {
        electricPrefab = prefab;
    }

    public void Attack(RectTransform tr, int damage, int range, Sprite sprite)
    {
        Object.Instantiate(electricPrefab, LevelManager.Instance.LevelInstance.transform).GetComponent<Electric>().Initialize(damage, tr, range, sprite);
    }
}