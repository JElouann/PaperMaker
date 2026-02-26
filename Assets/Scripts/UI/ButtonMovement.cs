using DG.Tweening;
using UnityEngine;

public class ButtonMovement : MonoBehaviour
{
    private Vector3 _basePosition;
    private Vector3 _baseRotation;
    private Vector3 _baseScale;

    [SerializeField] private RectTransform _target;

    [Space(10)]
    [SerializeField] private Vector3 _slideOffset;
    [SerializeField] private float _slideDuration;
    [SerializeField] private float _comebackDuration;
    private bool _isSliding;

    private void Awake()
    {
        _basePosition = _target.anchoredPosition;
    }

    public void Slide()
    {
        if (!_isSliding)
        {
            _isSliding = true;
            _target.DOAnchorPos3D(_target.anchoredPosition3D + _slideOffset, _slideDuration);
        }
        else
        {
            _isSliding = false;
            _target.DOAnchorPos3D(_basePosition, _slideDuration);
        }
    }
}