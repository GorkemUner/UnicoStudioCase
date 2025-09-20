using UnityEngine;

public class ProjectileAttackStrategy : IAttackStrategy
{
    private GameObject projectilePrefab;

    public ProjectileAttackStrategy(GameObject prefab)
    {
        projectilePrefab = prefab;
    }

    public void Attack(RectTransform tr, int damage, int range, Sprite sprite)
    {
        Object.Instantiate(projectilePrefab, tr).GetComponent<Projectile>().Initialize(damage, range, sprite);
    }
}