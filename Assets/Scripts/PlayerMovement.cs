using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private PlayerInput _input;
    private InputAction _moveInput;
    private PlayerData _stats;
    public PlayerMain PlayerMain { get; set; }
   
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _input = GetComponent<PlayerInput>();
        _moveInput = _input.actions.FindAction("Move");
    }
    private void Start()
    {
        PlayerMain = GetComponent<PlayerMain>();
        _stats = PlayerMain.Datas;
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        #region Run
        float targetSpeed = _moveInput.ReadValue<Vector2>().x * _stats.WalkTopSpeed;

        float speedDif = targetSpeed - _rb.linearVelocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _stats.WalkAcceleration : _stats.WalkDeceleration;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, _stats.WalkVelPower) * Mathf.Sign(speedDif);

        _rb.AddForce(movement * Vector2.right);
        #endregion

        #region Friction
        // check if we're grounded and that we are trying to stop (not pressing forwards nor backwards)
        if (PlayerMain.LastGroundedTime > 0 && Mathf.Abs(_moveInput.ReadValue<Vector2>().x) < 0.01f)
        {
            float amount = Mathf.Min(Mathf.Abs(_rb.linearVelocity.x), Mathf.Abs(_stats.FrictionAmount));

            // sets to movement direction
            amount *= Mathf.Sign(_rb.linearVelocity.x);

            // applies force against movement direction
            _rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
        #endregion
    }
}

