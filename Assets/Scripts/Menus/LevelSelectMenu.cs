using System.Collections.Generic;
using UnityEngine;

public class LevelSelectMenu : MonoBehaviour
{
    WorldData worldData;
    [SerializeField] SaveSystem saveSystem;
    [SerializeField] List<LevelButton> buttons;



    public void CreateLevelsButtons(Worlds world)
    {
        int from, to;
        (from, to) = worldData.GetFromTo(world);

        for (int i = from; i <= to; i++)
        {
            int buttonIndex = i - from;
            buttons[buttonIndex].Number = i;
            if (i > saveSystem.saveData.unlockedLevel)
            {
                buttons[buttonIndex].button.interactable = false;
            }
        }
    }
}
