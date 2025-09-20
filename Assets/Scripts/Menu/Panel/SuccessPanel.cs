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

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
