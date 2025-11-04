using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    [SerializeField] GameObject levelsList;
    [SerializeField] Button levelSelectBackButton;
    [SerializeField] Button settingsBackButton;
    [SerializeField] EventSystem eventSystem;
    [SerializeField] GameObject settingsFirstSelected;
    [SerializeField] GameObject levelsFirstSelected;

    void Start()
    {
        levelSelectButton.onClick.AddListener(EnableLevelSelect);
        settingsButton.onClick.AddListener(EnableSettings);
        levelSelectBackButton.onClick.AddListener(EnableMainMenu);
        settingsBackButton.onClick.AddListener(EnableMainMenu);
        playButton.onClick.AddListener(Play);

        EnableMainMenu();

    }
    void OnEnable()
    {
        RefreshPlayButton();
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
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        levelSelectMenu.SetActive(true);
        eventSystem.SetSelectedGameObject(levelsFirstSelected);
    }
    void EnableSettings()
    {
        mainMenu.SetActive(false);
        levelSelectMenu.SetActive(false);
        settingsMenu.SetActive(true);
        eventSystem.SetSelectedGameObject(settingsFirstSelected);
    }
    void EnableMainMenu()
    {
        settingsMenu.SetActive(false);
        levelSelectMenu.SetActive(false);
        mainMenu.SetActive(true);
        eventSystem.SetSelectedGameObject(playButton.gameObject);
    }
}
