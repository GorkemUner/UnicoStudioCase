using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    private int damage;
    private int range;
    [SerializeField] private float speed = 350f;
    [SerializeField]private RectTransform rect;
    private Vector2 initPoint;
    [SerializeField] private Image img;

    public void Initialize(int damage, int range, Sprite sprite)
    {
        this.damage = damage;
        this.range = range;
        img.sprite = sprite;
    }
    float maxDistance;
    private void Start()
    {
        maxDistance = CalculateRange();
        initPoint = rect.anchoredPosition;
    }

    void FixedUpdate()
    {
        if(Vector2.Distance(initPoint,rect.anchoredPosition)<= maxDistance)
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        else
            Destroy(gameObject);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals(Tags.Enemy))
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private float CalculateRange()
    {
        return range * EnemyGrids.Instance.GetDistanceBetweenCellsHorizontal();

    }
}
