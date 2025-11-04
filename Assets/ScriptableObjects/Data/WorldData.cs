using System.Collections.Generic;
using UnityEngine;

public enum Worlds
{
   World1,
   World2,
   World3,
   World4,
   World5
}
public class WorldsList
{
   public WorldsList(Worlds world, int levelsFrom, int levelsTo)
   {
      this.world = world;
      this.levelsFrom = levelsFrom;
      this.levelsTo = levelsTo;
   }
   public Worlds world;
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
}
