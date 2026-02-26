using UnityEngine;

[CreateAssetMenu(menuName = "Stats/PlayerData", fileName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Walk")]
    public float WalkTopSpeed;
    public float WalkAcceleration;
    public float WalkDeceleration;
    public float WalkVelPower;

    [Header("Run")]
    public float RunTopSpeed;
    public float RunAcceleration;
    public float RunDeceleration;
    public float RunVelPower;

    [Header("Jump")]
    public float JumpForce;
    public float JumpCutMultiplier;
    [Min(0.01f)]
    public float JumpBufferTime;
    public float JumpCoyoteTime;

    [Header("Fall")]
    public float VelocityThreshold;
    public float FallBoost;

    public float FrictionAmount;
}
