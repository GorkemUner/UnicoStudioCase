public class DefenceItemProjectile : DefenceItemBase
{
    private void Awake()
    {
        attackStrategy = new ProjectileAttackStrategy(attackGOPrefab);
    }
}