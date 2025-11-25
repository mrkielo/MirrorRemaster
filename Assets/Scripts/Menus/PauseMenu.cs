using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Button resumeButton;
    [SerializeField] Button quitButton;
    [HideInInspector] public InputSystem_Actions inputActions;

    void Start()
    {
        resumeButton.onClick.AddListener(Disable);
        quitButton.onClick.AddListener(Quit);
    }


    public void SubscribeToEvents()
    {
        inputActions.Player.Pause.performed += Toggle;
        inputActions.UI.Pause.performed += Toggle;
        inputActions.UI.Cancel.performed += Toggle;
    }


    public void Toggle(InputAction.CallbackContext ctx)
    {
        if (gameObject.activeSelf) Disable();
        else Enable();
    }

    public void Enable()
    {
        inputActions.Player.Disable();
        inputActions.UI.Enable();
        gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(resumeButton.gameObject);
        Time.timeScale = 0f;
    }

    public void Disable()
    {
        inputActions.Player.Enable();
        inputActions.UI.Disable();
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void Quit()
    {
        ConfirmationDialog.Show("Are you sure? Progress will be saved", () =>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        });
    }


}
