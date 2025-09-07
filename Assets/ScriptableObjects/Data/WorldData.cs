using UnityEngine;

[CreateAssetMenu(fileName = "WorldData", menuName = "Scriptable Objects/WorldData")]
public class WorldData : ScriptableObject
{
   public LayerMask groundLayers;
   public LayerMask lethalLayers;
   public LayerMask portalLayers;
}
