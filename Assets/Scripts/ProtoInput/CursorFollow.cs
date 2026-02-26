using UnityEngine;

public class CursorFollow : MonoBehaviour
{
    private Vector3 _cursorPos = Vector3.zero;

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;

        _cursorPos = Camera.main.ScreenToWorldPoint(mousePos);
        this.transform.position = new Vector3(_cursorPos.x, _cursorPos.y, 0);
    }
}
