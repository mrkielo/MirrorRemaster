using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Worlds
{
   World1,
   World2,
   World3,
   World4,
   World5
}
[System.Serializable]
public class WorldsList
{
   public WorldsList(Worlds world, int levelsFrom, int levelsTo)
   {
      this.world = world;
      this.levelsFrom = levelsFrom;
      this.levelsTo = levelsTo;
   }
   public Worlds world;
   public string title;
   public int levelsFrom;
   public int levelsTo;
}


[CreateAssetMenu(fileName = "WorldData", menuName = "Scriptable Objects/WorldData")]
public class WorldData : ScriptableObject
{
   public LayerMask groundLayers;
   public LayerMask lethalLayers;
   public LayerMask portalLayers;
   public int levelToScene;
   public List<WorldsList> worlds;

   public (int, int) GetFromTo(Worlds world)
   {
      WorldsList list = worlds.Find((x) => x.world == world);
      return (list.levelsFrom, list.levelsTo);
   }
   public List<Worlds> GetUnlockedWorlds(int savedUnlockedLevel)
   {
      List<Worlds> result = new List<Worlds>();
      foreach (var world in worlds)
      {
         if (savedUnlockedLevel >= world.levelsFrom)
         {
            result.Add(world.world);
         }
      }
      return result;
   }

   public string GetWorldName(Worlds world)
   {
      WorldsList list = worlds.Find((x) => x.world == world);
      return list.title;
   }

   public static Worlds IntToWorld(int i)
   {
      switch (i)
      {
         case 1:
            return Worlds.World1;
            break;
         case 2:
            return Worlds.World2;
            break;
         case 3:
            return Worlds.World3;
            break;
         case 4:
            return Worlds.World4;
            break;
         case 5:
            return Worlds.World5;
            break;
      }
      return Worlds.World5;
   }
}
