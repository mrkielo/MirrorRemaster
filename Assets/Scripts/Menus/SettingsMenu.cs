using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] Button resetProgressButton;
    [SerializeField] SaveSystem saveSystem;

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
