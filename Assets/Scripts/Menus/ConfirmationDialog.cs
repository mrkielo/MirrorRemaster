using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConfirmationDialog : MonoBehaviour
{
    public static void Show(string dialogMessage, System.Action actionOnConfirm)
    {
        instance.storedActionOnConfirm = actionOnConfirm;
        instance.dialogText.text = dialogMessage;
        instance.gameObject.SetActive(true);
        instance.lastSelected = EventSystem.current.currentSelectedGameObject;
        EventSystem.current.SetSelectedGameObject(instance.confirmationButton.gameObject);
    }
    private System.Action storedActionOnConfirm;
    private static ConfirmationDialog instance;

    [SerializeField] TMP_Text dialogText;
    [SerializeField] Button confirmationButton;
    [SerializeField] Button cancelButton;
    GameObject lastSelected;


    void Awake()
    {
        instance = this;
        gameObject.SetActive(false);

        confirmationButton.onClick.AddListener(OnConfirmButton);
        cancelButton.onClick.AddListener(OnCancelButton);
    }
    public void OnConfirmButton()
    {
        if (storedActionOnConfirm != null)
        {
            storedActionOnConfirm();
            storedActionOnConfirm = null;
            gameObject.SetActive(false);
            EventSystem.current.SetSelectedGameObject(lastSelected);
        }
    }
    public void OnCancelButton()
    {
        storedActionOnConfirm = null;
        gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(lastSelected);
    }
}