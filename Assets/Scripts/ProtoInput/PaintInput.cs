using UnityEngine;
using UnityEngine.VFX;

public class PaintInput : MonoBehaviour
{
    [SerializeField] private VisualEffect _maskVFX;
    [SerializeField] private Color _VFXColor;
    [SerializeField] private float _size = 0.5f;

    private Vector3 lastPos;

    private void Start()
    {
        _maskVFX.SetVector4("Color", _VFXColor);  
    }

    private void Update()
    {
        if (_maskVFX == null) return;

        //_maskVFX.SetVector3("CurrentPos", this.transform.position);

        _maskVFX.SetFloat("MouseDelta", Vector3.Distance(lastPos, Input.mousePosition));

        if (Input.GetMouseButtonDown(0)) _maskVFX.SetFloat("Opacity", 1);

        if (Input.GetMouseButtonUp(0)) _maskVFX.SetFloat("Opacity", 0);

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            _size = Mathf.Clamp(_size += 0.1f, 0.1f, 1);
            _maskVFX.SetFloat("Size", _size);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            _size = Mathf.Clamp(_size -= 0.1f, 0.1f, 1);
            _maskVFX.SetFloat("Size", _size);
        }

        lastPos = Input.mousePosition;
        //_maskVFX.SetVector3("LastPos", this.transform.position);
    }
}
