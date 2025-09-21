using UnityEngine;
using UnityEngine.UI;

public class FailPanel : MonoBehaviour
{
    [SerializeField] private Button failButton;

    private void OnEnable()
    {
        failButton.onClick.AddListener(OnFailButtonClicked);
    }

    private void OnDisable()
    {
        failButton.onClick.RemoveListener(OnFailButtonClicked);
    }
 
    private void OnFailButtonClicked()
    {
        Time.timeScale = 1f;
        LevelManager.Instance.RestartLevel();
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
    }
}
