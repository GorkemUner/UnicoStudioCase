using TMPro;
using UnityEngine;

public class LevelPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;

    void Start()
    {
        levelText.text = "Level:" + (LevelManager.Instance.Level + 1).ToString();
    }
}
