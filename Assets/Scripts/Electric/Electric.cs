using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Electric : MonoBehaviour
{
    [SerializeField]private CanvasGroup cg;
    private int damage;
    [SerializeField] private BoxCollider2D coll;
    [SerializeField] private RectTransform rectTr;
    [SerializeField] private Image img;

    private float disableCollEffectDuration = 1f;

    public void Initialize(int damage, RectTransform initPosRect, int range, Sprite sprite)
    {
        this.damage = damage;
        rectTr.anchoredPosition = new Vector2(0, rectTr.anchoredPosition.y);
        rectTr.position = new Vector2(rectTr.position.x, initPosRect.position.y);
        rectTr.sizeDelta = new Vector2(rectTr.sizeDelta.x,  range * EnemyAreaManager.Instance.GetDistanceBetweenCellsHorizontal());
        img.sprite = sprite;

        UpdateCollider();
        StartCoroutine(DisableColliderEffect(disableCollEffectDuration));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals(Tags.Enemy))
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    void UpdateCollider()
    {
        Vector2 size = rectTr.rect.size;
        coll.size = size;
        coll.offset = rectTr.rect.center;
    }

    private IEnumerator DisableColliderEffect(float duration)
    {
        yield return new WaitForFixedUpdate();
        coll.enabled = false;
        rectTr.DOKill();
        rectTr.DOShakeAnchorPos(
            duration: 1f,
           strength: new Vector2(15f, 15f),
            vibrato: 100,
            randomness: 90f,
            snapping: false,
            fadeOut: false
        );
        cg.DOKill();
        cg.DOFade(0, duration);
        yield return new WaitForSeconds(duration);

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        rectTr.DOKill();
        cg.DOKill();
    }
}