using UnityEngine;
using UnityEngine.EventSystems;

public class DragUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        // Trova la Canvas nel genitore
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Eventuale logica all'inizio del trascinamento
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canvas == null) return;

        // Muove l'oggetto usando il delta del puntatore
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Eventuale logica alla fine del trascinamento
    }
}