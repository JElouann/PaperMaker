using DG.Tweening;
using DG.Tweening.Core;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelBlock : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private RawImage _preview;

    [field: SerializeField] public OptionsUIFade OptionsUIFade { get; private set; }

    private static LevelBlock _currentSelected = null;

    public void InitBlock(string levelName, Texture levelPreview)
    {
        _title.text = levelName;
        _preview.texture = levelPreview;
    }

    public void InitEmptyBlock()
    {
        _title.text = "";
        _preview.texture = null;
        _preview.color = Color.gray5;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Sequence sequence = DOTween.Sequence().Pause();

        sequence
            .Append(this.transform.DOScale(1.2f, 0.2f))
            .Insert(0, this.transform.DORotate(Vector3.back * 3 * Input.GetAxis("Mouse X"), 0.1f))
            .Append(this.transform.DORotate(Vector3.one * 0f, 0.30f));

        sequence.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Unscale();
        if (_currentSelected == this)
        {
            OptionsUIFade.Fade();
            _currentSelected = null;    
        }
    }

    private void Unscale()
    {
        this.transform.DOScale(1f, 0.35f);
    }

    public void Select()
    {
        _currentSelected = this;
        OptionsUIFade.Unfade();
        LevelHandler.Instance.SelectLevelData(_title.text);
    }
}
