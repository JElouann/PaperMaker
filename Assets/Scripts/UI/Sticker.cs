using UnityEngine;
using UnityEngine.EventSystems;

public class Sticker : MonoBehaviour, IDragHandler
{
    public StickerType StickerType;
    private bool _isPosed;

    public void OnDrag(PointerEventData eventData)
    {
        if (!_isPosed)
        {
            _isPosed = true;
            this.transform.parent = this.transform.parent.parent.parent;
            StickerHandler.PlacedStickers.Add(this);
        }

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;
        Vector3 cursorCurrentPos = Camera.main.ScreenToWorldPoint(mousePos);

        this.transform.position = mousePos;
    }
}

public enum StickerType
{
    Player,
    LevelEnd
}
