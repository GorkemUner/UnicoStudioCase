using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DefenceMenuItem : MonoBehaviour
{
    [SerializeField] private GameObject defenceItemGO;
    [SerializeField] private TextMeshProUGUI tmpText;
    [SerializeField] private Button btn;

    private int amount;
    public int Amount
    {
        get => amount;
        set
        {
            amount = value;
            tmpText.text = "Count: " + amount.ToString();
            if (amount == 0)
                btn.interactable = false;
        }
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameBattleStart.AddListener(OnGameBattleStart);
    }

    private void OnDisable()
    {
        GameManager.Instance?.OnGameBattleStart.RemoveListener(OnGameBattleStart);
    }

    private void Start()
    {
        CreateDefenceItemControl();
    }

    private void OnGameBattleStart()
    {
        btn.interactable = false;
    }

    public void CreateDefenceItemControl()
    {
        if (Amount <= 0)
            return;
        Instantiate(defenceItemGO, transform.position, Quaternion.identity, transform).GetComponent<DefenceItemBase>().DefenceMenuItem = this;
    }
}