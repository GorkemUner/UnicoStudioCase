using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class CanvasSortOrder : MonoBehaviour
{
    [SerializeField] private SortOrder sortOrder;
    [SerializeField] private Canvas canvas;

    private void OnEnable()
    {
        SetOrder(sortOrder);
    }

    public void SetOrder(SortOrder sortOrder)
    {

        this.sortOrder = sortOrder;
        canvas.sortingOrder = (int)sortOrder;
    }
}

public enum SortOrder
{
    Enemy = 1,
    DefenceItemOnMenuItem = 2,
    DefenceItemOnBattle = 3,
    DefenceItemOnHover = 4,
    AttackObjects = 5,
    FailSuccessPanel = 6
}
