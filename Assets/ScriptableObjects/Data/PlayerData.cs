using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
   public float mSpeed;
   public float jumpForce;
   public float mSmoothing;
   public float coyoteTime;
   public float dashPower;
   public float dashTime;
}
