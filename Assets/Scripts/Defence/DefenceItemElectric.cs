public class DefenceItemElectric : DefenceItemBase
{
    private void Awake()
    {
        attackStrategy = new ElectricAttackStrategy(attackGOPrefab);
    }
}