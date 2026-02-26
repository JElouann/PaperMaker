using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    public PlayerMain Main;
    private Rigidbody2D _rb;

    [SerializeField]
    private Transform groundCheckTransform;
    private Vector2 _groundCheckPoint;
    [SerializeField]
    private Vector2 _groundCheckSize;

    [SerializeField] private float _jumpFallFactor = 0.2f;

    private bool _isJumping;
    private bool _jumpIsCut;

    private PlayerData _datas;

    //
    private float _lastPressedJumpTime = 0;
    private float _lastGroundedTime = 0;
    private int _groundLayerIndex;

    private void Awake()
    {
        Main = GetComponent<PlayerMain>();
        _rb = GetComponent<Rigidbody2D>();
        PlayerInputManager inputManager = this.GetComponent<PlayerInputManager>();
        _groundLayerIndex = LayerMask.NameToLayer("Ground");
        _datas = Main.Datas;
        Main.OnGrounded += OnGrounded;
    }


    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _lastPressedJumpTime = _datas.JumpBufferTime;

            if (CanJump() && _lastPressedJumpTime > 0)
            {
                LogManager.Instance.Log("Jump", "Debug");
                Jump();
            }
        }
        if (context.canceled && !_jumpIsCut)
        {
            CutJump();
        }
    }

    private void Jump()
    {
        float force = _datas.JumpForce;

        // reset the vertical velocity to 0 if falling to give the impression that we always jump with the same force
        if (_rb.linearVelocity.y < 0) force -= _rb.linearVelocity.y;

        // execute jump
        _rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        Main.LastGroundedTime = _datas.JumpBufferTime;

        //_isJumping = true;
        Main.IsJumping = true;

        StartCoroutine(JumpFall());
    }

    private IEnumerator JumpFall()
    {
        yield return new WaitForSeconds(0.15f);

        while (_rb.linearVelocity.y > Main.Datas.VelocityThreshold)
        {
            yield return new WaitForEndOfFrame();
        }

        _rb.AddForce(Vector2.up * -_rb.linearVelocityY * Main.Datas.FallBoost, ForceMode2D.Impulse);
    }

    private bool CanJump()
    {
        return (Main.LastGroundedTime > 0 && /*!_isJumping*/ !Main.IsJumping);
    }

    private void CutJump()
    {
        _rb.AddForce(Vector2.down * _rb.linearVelocity.y * (1 - _datas.JumpCutMultiplier), ForceMode2D.Impulse);
        _jumpIsCut = true;
    }

    private void OnGrounded()
    {
        _jumpIsCut = false;
        Main.IsJumping = false;
    }

    private void Update()
    {
        _lastPressedJumpTime -= Time.deltaTime;
    }

    private bool CoyoteTime()
    {
        return _lastGroundedTime > 0 | _lastPressedJumpTime > 0;
    }
}