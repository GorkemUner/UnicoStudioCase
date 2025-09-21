using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image img;

    private EnemyData data;

    private bool isDead = false;

    public EnemyData Data
    {
        get => data;
        set => data = value;
    }

    private EnemyGrid enemyGrid;
    public EnemyGrid EnemyGrid
    {
        get => enemyGrid;
        set => enemyGrid = value;
    }

    private EnemySpawner enemySpawner;
    public EnemySpawner EnemySpawner
    {
        get => enemySpawner;
        set => enemySpawner = value;
    }

    private int currentHealth;
    public int CurrentHealth
    {
        get => currentHealth;
        set
        {
            currentHealth = value;
            healthText.text = currentHealth.ToString();
        }
    }

    private void Start()
    {
        CurrentHealth = Data.health;
        img.sprite = Data.enemySprite;
        StartCoroutine(MoveCoroutine());
    }

    public void Init(EnemyGrid enemyGrid, EnemySpawner enemySpawner, EnemyData data)
    {
        this.enemyGrid = enemyGrid;
        this.enemySpawner = enemySpawner;
        Data = data;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0 && !isDead) //Multiple electric defense items can hit in the same frame! so isDead check is necessary.
            Die();
    }

    private IEnumerator MoveCoroutine()
    {
        while (enemyGrid.NextEnemyGrid != null)
        {
            yield return new WaitForSeconds(Data.moveDuration);
            yield return new WaitUntil(() => enemyGrid.NextEnemyGrid.IsEmpty);
            Move();
        }

        yield return new WaitForSeconds(Data.moveDuration);
        GameManager.Instance.GameState = GameState.GameFail;
    }

    public virtual void Move()
    {
        transform.position = enemyGrid.NextEnemyGrid.transform.position;
        EnemyGrid.IsEmpty = true;
        EnemyGrid = EnemyGrid.NextEnemyGrid;
        EnemyGrid.IsEmpty = false;
    }

    protected virtual void Die()
    {
        isDead = true;
        EnemyGrid.IsEmpty = true;
        EnemySpawner.CurrentTotalDie++;
        if (EnemySpawner.CurrentTotalDie == EnemySpawner.EnemyTotalDieCountMustBeSuccessLevel)
            GameManager.Instance.GameState = GameState.GameSuccess;
        Destroy(gameObject);
    }
}