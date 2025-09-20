using System.Collections.Generic;
using UnityEngine;

public class DefenceAreaManager : Singleton<DefenceAreaManager>
{
    public List<DefenceAreaGrid> defenceAreaGrids;

    public DefenceAreaGrid GetEmptyNearestDistanceToMyPos(Vector2 pos)
    {
        DefenceAreaGrid result = null;
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

    public DefenceAreaGrid GetNearestDistanceToMyPos(Vector2 pos)
    {
        DefenceAreaGrid result = null;
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