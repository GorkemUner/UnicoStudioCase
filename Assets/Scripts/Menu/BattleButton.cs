using UnityEngine;
using UnityEngine.UI;

public class BattleButton : MonoBehaviour
{
    [SerializeField] private Button battleBtn;

    private void OnEnable()
    {
        battleBtn.onClick.AddListener(OnBattleButtonClick);
    }

    private void OnDisable()
    {
        battleBtn.onClick.RemoveListener(OnBattleButtonClick);
    }

    public void OnBattleButtonClick()
    {
        battleBtn.enabled = false;
        GameManager.Instance.GameState = GameState.OnBattle;
    }
}