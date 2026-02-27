using UnityEngine;

public class PlayerGamefeel : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField]
    private GameObject _stretchPivot;
    [SerializeField]
    private GameObject _rotationPivot;
    [SerializeField]
    private AnimationCurve _velocityRotateCurve;
    [SerializeField]
    private AnimationCurve _velocityStretchCurve;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        _stretchPivot.transform.localScale = new Vector3(1, _velocityStretchCurve.Evaluate(_rb.linearVelocity.y), 1);
        _rotationPivot.transform.rotation = new Quaternion(0, 0, _velocityRotateCurve.Evaluate(_rb.linearVelocity.x), 360);
    }
}
