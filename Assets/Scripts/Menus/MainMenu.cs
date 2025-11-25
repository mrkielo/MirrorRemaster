using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    [SerializeField] SaveSystem saveSystem;

    [Header("UI Elements")]
    [SerializeField] Button playButton;
    [SerializeField] Button levelSelectButton;
    [SerializeField] Button settingsButton;
    [SerializeField] Button quitButton;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject levelSelectMenu;
    [SerializeField] GameObject settingsMenu;

    [SerializeField] Button levelSelectBackButton;
    [SerializeField] Button settingsBackButton;
    [SerializeField] EventSystem eventSystem;
    [SerializeField] GameObject settingsFirstSelected;
    [SerializeField] GameObject levelsFirstSelected;

    InputSystem_Actions inputActions;

    void Start()
    {
        levelSelectButton.onClick.AddListener(EnableLevelSelect);
        settingsButton.onClick.AddListener(EnableSettings);
        levelSelectBackButton.onClick.AddListener(EnableMainMenu);
        settingsBackButton.onClick.AddListener(EnableMainMenu);
        playButton.onClick.AddListener(Play);

        EnableMainMenu();

    }

    void Awake()
    {
        inputActions = new InputSystem_Actions();
    }
    void OnEnable()
    {
        Invoke("RefreshPlayButton", 0.5f);
        inputActions.Player.Disable();
        inputActions.UI.Enable();

    }

    void Play()
    {
        SceneManager.LoadScene(saveSystem.saveData.unlockedLevel);
    }

    void RefreshPlayButton()
    {
        TMP_Text playButtonText = playButton.GetComponentInChildren<TMP_Text>();
        if (saveSystem.saveData.unlockedLevel > 1)
        {
            playButtonText.text = "Continue";
        }
        else
        {
            playButtonText.text = "New Game";
        }
    }



    void EnableLevelSelect()
    {
        inputActions.UI.Cancel.performed += BackToMainMenu;
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        levelSelectMenu.GetComponentInChildren<WorldSelectMenu>().inputActions = inputActions;
        levelSelectMenu.SetActive(true);
        eventSystem.SetSelectedGameObject(levelsFirstSelected);
    }
    void EnableSettings()
    {
        inputActions.UI.Cancel.performed += BackToMainMenu;
        mainMenu.SetActive(false);
        levelSelectMenu.SetActive(false);
        settingsMenu.SetActive(true);
        eventSystem.SetSelectedGameObject(settingsFirstSelected);
    }
    void EnableMainMenu()
    {
        inputActions.UI.Cancel.performed -= BackToMainMenu;
        settingsMenu.SetActive(false);
        levelSelectMenu.SetActive(false);
        mainMenu.SetActive(true);
        eventSystem.SetSelectedGameObject(playButton.gameObject);
    }


    void BackToMainMenu(InputAction.CallbackContext ctx)
    {
        EnableMainMenu();
    }

    void OnDestroy()
    {
        inputActions.UI.Disable();
    }

}
