using UnityEngine;
using UnityEngine.EventSystems;

public class Painter : MonoBehaviour
{
    // TODO : clean variables and fields
    public Camera Camera;
    private EventSystem _eventSystem;
    public GameObject Brush;

    public LineRenderer currentLineRenderer;

    private Vector3 _mousePos;
    private Vector3 _cursorCurrentPos;
    private Vector3 _cursorLastPos;
    private float _minVelocityThreshold = 0.14f;

    public Color Color;

    private int _lineAmount = 0;

    [SerializeField] private Transform _linesParent;

    private void Awake()
    {
        _eventSystem = EventSystem.current;    
    }

    private void Update()
    {
        Draw();
    }

    private void Draw()
    {
        if (_eventSystem.IsPointerOverGameObject() || BrushColorChanger.CurrentBrush == null) return;
        if (Input.GetMouseButtonDown(0))
        {
            _mousePos = Input.mousePosition;
            _mousePos.z = 10;
            _cursorCurrentPos = Camera.ScreenToWorldPoint(_mousePos);

            CreateBrush();
        }
        if (Input.GetMouseButton(0))
        {
            _mousePos = Input.mousePosition;
            _mousePos.z = 10;
            _cursorCurrentPos = Camera.ScreenToWorldPoint(_mousePos);

            if (Vector3.Distance(_cursorCurrentPos, _cursorLastPos) > _minVelocityThreshold)
            {
                AddAPoint(_cursorCurrentPos);
                _cursorLastPos = _cursorCurrentPos;
            }
        }
        else
        {
            currentLineRenderer = null;
        }
    }

    private void CreateBrush()
    {
        // TODO : implé pool
        GameObject brushInstance = Instantiate(Brush, _cursorCurrentPos, Quaternion.identity, _linesParent);
        brushInstance.TryGetComponent(out currentLineRenderer);
        currentLineRenderer.startColor = Color;
        currentLineRenderer.endColor = Color;
        currentLineRenderer.sortingOrder = _lineAmount;
        _lineAmount++;

        Vector2 mousePos = Camera.ScreenToWorldPoint(Input.mousePosition);

        currentLineRenderer.SetPosition(0, _cursorCurrentPos);
        currentLineRenderer.SetPosition(1, _cursorCurrentPos);
    }

    private void AddAPoint(Vector2 pointPos)
    {
        currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount - 1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);
    }
}
