using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUIFade : MonoBehaviour
{
    [field: SerializeField] public Image OptionsPanel { get; private set; }
    [field: SerializeField] public List<Image> OptionButtons { get; private set; } = new();
    [field: SerializeField] public List<TextMeshProUGUI> OptionTexts { get; private set; } = new();

    public void Fade()
    {
        float panelFade = 0.0f;
        float uiFade = 0.0f;

        OptionsPanel.DOFade(panelFade, 0.25f);
        foreach (Image image in OptionButtons)
        {
            image.DOFade(uiFade, 0.3f).OnComplete(() => image.gameObject.SetActive(false));
            image.transform.DOMoveY(this.transform.position.y - 25, 0.15f);
        }
        foreach (TextMeshProUGUI text in OptionTexts)
        {
            text.DOFade(uiFade, 0.3f).OnComplete(() => text.gameObject.SetActive(false));
        }
    }

    public void Unfade()
    {
        float panelFade = 0.35f;
        float uiFade = 1f;

        OptionsPanel.DOFade(panelFade, 0.1f);
        foreach (Image image in OptionButtons)
        {
            image.gameObject.SetActive(true);
            image.DOFade(uiFade, 0.2f);
            image.transform.DOMoveY(this.transform.position.y + 25, 0.12f);
        }
        foreach (TextMeshProUGUI text in OptionTexts)
        {
            text.gameObject.SetActive(true);
            text.DOFade(uiFade, 0.3f);
        }
    }
}
