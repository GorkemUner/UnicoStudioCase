using UnityEngine;
using UnityEngine.UI;

public class SuccessPanel : MonoBehaviour
{
    [SerializeField] private Button successButton;

    private void OnEnable()
    {
        successButton.onClick.AddListener(OnSuccessButtonClicked);
    }

    private void OnDisable()
    {
        successButton.onClick.RemoveListener(OnSuccessButtonClicked);
    }

    private void OnSuccessButtonClicked()
    {
        Time.timeScale = 1f;
        LevelManager.Instance.NextLevel();
    }

    void Start()
    {
        Time.timeScale = 0f;
    }
}
