using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/LevelData")]
public class LevelData : ScriptableObject
{
   public bool canDash;
   public bool canDoubleJump;
   public bool canFreeze;
}
