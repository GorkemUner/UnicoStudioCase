using UnityEngine;

public class DefenceAreaGrid : MonoBehaviour
{
    public bool IsEmpty => defenceItemBase == null;

    private DefenceItemBase defenceItemBase;
    public DefenceItemBase DefenceItemBase
    {
        get => defenceItemBase;
        set => defenceItemBase = value;
    }
}
