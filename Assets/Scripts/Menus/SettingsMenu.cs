using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] SaveSystem saveSystem;
    [Header("UI Elements")]
    [SerializeField] Button resetProgressButton;

    void Start()
    {
        resetProgressButton.onClick.AddListener(OnResetProgress);
    }

    void OnResetProgress()
    {
        ConfirmationDialog.Show("Are you sure you want reset all your progress?", () =>
        {
            saveSystem.DeleteData();
            saveSystem.LoadData();
        });

    }

}
