using UnityEngine;

public class EnemyGrid : MonoBehaviour
{
    [SerializeField] private EnemyGrid nextEnemyGrid;
    [SerializeField] private RectTransform rect;
    public RectTransform Rect => rect;

    public EnemyGrid NextEnemyGrid => nextEnemyGrid;

    [SerializeField]private bool isEmpty = true;
    public bool IsEmpty
    {
        get => isEmpty;
        set => isEmpty = value;
    }
}