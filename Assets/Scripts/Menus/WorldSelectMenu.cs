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
   public InputSystem_Actions inputActions;

   void OnEnable()
   {
      eventSystem.SetSelectedGameObject(lastSelectedButton == null ? worldButtons[0].gameObject : lastSelectedButton);
      inputActions.UI.Enable();
   }



   public void OpenLevelMenu(int world)
   {
      inputActions.UI.Disable();
      lastSelectedButton = worldButtons[world - 1].gameObject;
      gameObject.SetActive(false);
      levelSelectMenu.gameObject.SetActive(true);
      levelSelectMenu.SetWorld(WorldData.IntToWorld(world));
   }





}
