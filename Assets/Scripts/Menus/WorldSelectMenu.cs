using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WorldSelectMenu : MonoBehaviour
{
   [SerializeField] WorldData worldData;
   [SerializeField] LevelSelectMenu levelSelectMenu;
   [SerializeField] List<Button> worldButtons;
   [SerializeField] EventSystem eventSystem;
   GameObject lastSelectedButton;

   void OnEnable()
   {
      eventSystem.SetSelectedGameObject(lastSelectedButton == null ? worldButtons[0].gameObject : lastSelectedButton);
   }



   public void OpenLevelMenu(int world)
   {
      lastSelectedButton = worldButtons[world - 1].gameObject;
      gameObject.SetActive(false);
      levelSelectMenu.gameObject.SetActive(true);
      levelSelectMenu.SetWorld(WorldData.IntToWorld(world));
   }





}
