using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class DefenceItemBase : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private RectTransform rectTr;
    [SerializeField] private RectTransform AttackGOStartRect;
    [SerializeField] private GraphicRaycaster graphicRaycaster;
    [SerializeField] private DefenceItemData defenceItemData;
    [SerializeField] private GameObject timerGO;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private CanvasSortOrder canvasSortOrder;
    [SerializeField] private Sprite sprite;
    [SerializeField] protected GameObject attackGOPrefab;

    private Place place = Place.Menu;
    private DefenceAreaGrid currentDefenceAreaGrid;
    private Place previousPlace;
    private float moveDuration = .3f;

    protected IAttackStrategy attackStrategy;

    private float timer;
    private float Timer
    {
        get => timer;
        set
        {
            timer = value;
            timerText.text = Mathf.CeilToInt(timer).ToString();
        } 
    }

    private DefenceMenuItem defenceMenuItem;
    public DefenceMenuItem DefenceMenuItem
    {
        get=> defenceMenuItem;
        set => defenceMenuItem = value;
    }
    public Place Place
    {
        get => place;
        set
        {
            place = value;
            switch (place)
            {
                case Place.Menu:
                    canvasSortOrder.SetOrder(SortOrder.DefenceItemOnMenuItem);
                    break;
                case Place.BattleArea:
                    canvasSortOrder.SetOrder(SortOrder.DefenceItemOnBattle);
                    break;
                case Place.OnHover:
                    canvasSortOrder.SetOrder(SortOrder.DefenceItemOnHover);
                    break;
            }

        }
    }

    private void Start()
    {
        Timer = defenceItemData.interval;
    }

    protected void Fire()
    {
        attackStrategy.Attack(AttackGOStartRect, defenceItemData.damage, defenceItemData.range, sprite);
    }
    private void OnEnable()
    {
        GameManager.Instance.OnGameBattleStart.AddListener(OnGameBattleStart);
    }

    private void OnDisable()
    {
        GameManager.Instance?.OnGameBattleStart.RemoveListener(OnGameBattleStart);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (GameManager.Instance.GameState == GameState.OnBattle)
            return;

        if (Place == Place.OnHover)
            transform.position = eventData.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GameManager.Instance.GameState == GameState.OnBattle)
            return;

        previousPlace = Place;

        if (Place == Place.BattleArea)
            currentDefenceAreaGrid.DefenceItemBase = null;

        if (Place == Place.Menu)
            DefenceMenuItem.Amount--;

        Place = Place.OnHover;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (GameManager.Instance.GameState == GameState.OnBattle)
            return;

        DefenceAreaGrid nearestDefenceAreaGrid;

        if (previousPlace == Place.BattleArea)
            nearestDefenceAreaGrid = DefenceAreaManager.Instance.GetNearestDistanceToMyPos(transform.position);
        else
        {
            nearestDefenceAreaGrid = DefenceAreaManager.Instance.GetEmptyNearestDistanceToMyPos(transform.position);
            if (nearestDefenceAreaGrid == null)
            {
                rectTr.anchoredPosition = Vector2.zero;
                DefenceMenuItem.Amount++;
                Place = Place.Menu;
                return;
            }
        }

        if (!nearestDefenceAreaGrid.IsEmpty && previousPlace != Place.Menu)
        {
            Swap(nearestDefenceAreaGrid);
            return;
        }

        nearestDefenceAreaGrid.DefenceItemBase = this;
        currentDefenceAreaGrid = nearestDefenceAreaGrid;

        graphicRaycaster.enabled = false;
        rectTr.DOKill();
        rectTr.DOMove(nearestDefenceAreaGrid.transform.position, moveDuration)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                graphicRaycaster.enabled = true;
            });

        Place = Place.BattleArea;
        timerGO.SetActive(true);

        if (previousPlace == Place.Menu)
            DefenceMenuItem.CreateDefenceItemControl();
    }

    protected void Swap(DefenceAreaGrid nearestDefenceAreaGrid)
    {
        var otherItem = nearestDefenceAreaGrid.DefenceItemBase;
        var otherGrid = nearestDefenceAreaGrid;
        var myItem = this;
        var myGrid = currentDefenceAreaGrid;

        otherItem.graphicRaycaster.enabled = false;
        myItem.graphicRaycaster.enabled = false;

        otherItem.currentDefenceAreaGrid = myGrid;
        otherGrid.DefenceItemBase = myItem;

        myItem.currentDefenceAreaGrid = otherGrid;
        myGrid.DefenceItemBase = otherItem;

        otherItem.rectTr.DOKill();
        Ease easeType = Ease.InOutQuad;
        otherItem.rectTr.DOMove(myGrid.transform.position, moveDuration)
            .SetEase(easeType)
            .OnComplete(() =>
            {
                otherItem.graphicRaycaster.enabled = true;
            });

        myItem.rectTr.DOKill();
        myItem.rectTr.DOMove(otherGrid.transform.position, moveDuration)
            .SetEase(easeType)
            .OnComplete(() =>
            {
                myItem.graphicRaycaster.enabled = true;
            });

        Place = Place.BattleArea;
        return;
    }

    private void OnGameBattleStart()
    {
        if (Place == Place.Menu)
            graphicRaycaster.enabled = false;
        StartCoroutine(AttackGOSpawnerCoroutine());
    }

    private IEnumerator AttackGOSpawnerCoroutine()
    {
        while ((GameManager.Instance.GameState == GameState.OnBattle) && (Place == Place.BattleArea))
        {
            Timer -= Time.deltaTime;

            if (Timer <= 0f)
            {
                Timer = defenceItemData.interval;
                Fire();
            }

            yield return null;
        }
    }
}

public enum Place
{
    OnHover,
    BattleArea,
    Menu
}

[System.Serializable]
public struct DefenceItemData
{
    public int damage;
    public int range;
    public float interval;
}