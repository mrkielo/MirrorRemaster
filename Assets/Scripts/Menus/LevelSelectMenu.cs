using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectMenu : MonoBehaviour
{
    [SerializeField] WorldData worldData;
    [SerializeField] EventSystem eventSystem;
    [SerializeField] WorldSelectMenu worldSelectMenu;
    [SerializeField] TMP_Text titleTMP;
    [SerializeField] SaveSystem saveSystem;
    [SerializeField] List<LevelButton> buttons;
    [SerializeField] Button backButton;

    void Start()
    {
        backButton.onClick.AddListener(Back);
    }

    public void Back()
    {
        worldSelectMenu.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }


    public void SetWorld(Worlds world)
    {
        SetLevelsButtons(world);
        titleTMP.text = worldData.GetWorldName(world);
    }


    void SetLevelsButtons(Worlds world)
    {
        int from, to;
        bool isFirstSelected = false;
        (from, to) = worldData.GetFromTo(world);

        for (int i = from; i <= to; i++)
        {
            LevelButton button = buttons[i - from];

            button.Number = i;
            button.button.interactable = i <= saveSystem.saveData.unlockedLevel;
            if (i == saveSystem.saveData.unlockedLevel)
            {
                eventSystem.SetSelectedGameObject(button.gameObject);
                isFirstSelected = true;
            }

            button.button.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(button.Number);
            });


        }
        if (!isFirstSelected)
        {
            eventSystem.SetSelectedGameObject(backButton.gameObject);
        }
    }
}
