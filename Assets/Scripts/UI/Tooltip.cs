using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject _tooltipPanel;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _tooltipPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tooltipPanel.SetActive(false);
    }
}
