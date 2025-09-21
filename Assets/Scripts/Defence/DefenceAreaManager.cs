using System.Collections.Generic;
using UnityEngine;

public class DefenceAreaManager : Singleton<DefenceAreaManager>
{
    [SerializeField]private List<DefenceGrid> defenceAreaGrids;

    public DefenceGrid GetEmptyNearestDistanceToMyPos(Vector2 pos)
    {
        DefenceGrid result = null;
        float minDist = float.MaxValue;

        foreach (var item in defenceAreaGrids)
        {
            if (item.IsEmpty && (Vector2.Distance(pos, item.transform.position) < minDist))
            {
                minDist = Vector2.Distance(pos, item.transform.position);
                result = item;
            }
        }

        return result;
    }

    public DefenceGrid GetNearestDistanceToMyPos(Vector2 pos)
    {
        DefenceGrid result = null;
        float minDist = float.MaxValue;

        foreach (var item in defenceAreaGrids)
        {
            if ((Vector2.Distance(pos, item.transform.position) < minDist))
            {
                minDist = Vector2.Distance(pos, item.transform.position);
                result = item;
            }
        }

        return result;
    }
}