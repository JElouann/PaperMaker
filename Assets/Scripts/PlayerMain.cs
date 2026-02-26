using System;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    public PlayerData Datas;

    // GroundCheck
    [Header("GroundCheck")]
    [SerializeField] private Transform _groundCheckTransform;
    [SerializeField] private LayerMask _groundLayer;
    public Vector2 GroundCheckPoint { get => _groundCheckTransform.position; }
    [field: SerializeField]
    public Vector2 GroundCheckSize { get; set; }

    [field: Header("Gamefeel GO references")]
    [field: SerializeField]
    public GameObject StretchPivot {  get; set; }
    [field: SerializeField]
    public GameObject RotationPivot {  get; set; }
    
    // Timer
    public float LastGroundedTime;

    // Events
    public event Action OnGrounded;

    public PlayerMovement PlayerMovement; 

    public bool IsJumping;
    public Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        LastGroundedTime -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (Physics2D.OverlapBox(GroundCheckPoint, GroundCheckSize, 0, _groundLayer) && _rb.linearVelocity.y <= 0)
        {
            LastGroundedTime = Datas.JumpCoyoteTime;
            OnGrounded.Invoke();
        }
    }

    Vector3 dir;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            LastGroundedTime = Datas.JumpCoyoteTime;
            OnGrounded.Invoke();
            dir = Vector3.Lerp(collision.transform.position, gameObject.transform.position, 0.5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") dir = Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(GroundCheckPoint, GroundCheckSize);
        Gizmos.color = Color.yellow;
        if (dir != Vector3.zero)
        {
            //Gizmos.DrawCube(dir, new Vector3(0.5f, 0.5f, 0.5f));
            Gizmos.DrawSphere(dir, 0.2f);
        }
    }
}
