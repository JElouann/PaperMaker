using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrushColorChanger : MonoBehaviour
{
    [SerializeField] Painter _painter;

    [SerializeField] private Image _targetImage;
    private Color _color;
    
    private static List<BrushColorChanger> _brushesColorChanger = new();
    public static BrushColorChanger CurrentBrush;

    private Vector3 _basePos;

    private void Awake()
    {
        _color = _targetImage.color;
        _brushesColorChanger.Add(this);
        _basePos = this.transform.position;
    }

    public void OnClick()
    {
        if (CurrentBrush == this)
        {
            CurrentBrush = null;
            ResetPos();
            return;
        }
        foreach (var brush in _brushesColorChanger)
        {
            if (brush != this) brush.ResetPos();
        }

        CurrentBrush = this;

        this.transform.DOMoveX(this.transform.position.x + 220, 0.5f);
        _painter.Color = _color;
    }

    public void ResetPos()
    {
        this.transform.DOMove(_basePos, 0.8f);
    }
}
