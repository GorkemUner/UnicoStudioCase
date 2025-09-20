using UnityEngine;

public interface IAttackStrategy
{
    void Attack(RectTransform tr, int damage, int range, Sprite sprite = null);
}
