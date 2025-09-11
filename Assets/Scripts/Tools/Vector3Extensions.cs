using UnityEngine;

public static class Vector3Extensions
{
   ///<summary>
   /// Sets given vector z axis to 0
   ///</summary>
   public static Vector3 FlatZ(this Vector3 pos)
   {
      return new Vector3(pos.x, pos.y, 0);
   }
}