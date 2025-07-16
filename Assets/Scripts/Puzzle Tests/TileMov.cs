using UnityEngine;
using UnityEngine.EventSystems;

public class TileMov : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private bool isSelected = false;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Update()
    {
        if(isSelected && Input.GetKeyDown(KeyCode.R))
        {
            rectTransform.Rotate(0, 0, -90);
            Debug.Log($"{gameObject.name} ruotato di 90 gradi");
        }

        if(isSelected && Input.GetMouseButtonDown(1))
        {
            isSelected = false;
            Debug.Log($"Deselezionato {gameObject.name}");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isSelected = true;
        Debug.Log($"Cliccato su: {gameObject.name}");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log($"Inizio trascinamento di: {gameObject.name}");
        if (canvasGroup != null) canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canvas == null) return;

        Vector2 position;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
            out position))
        {
            rectTransform.anchoredPosition = position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log($"Fine trascinamento di: {gameObject.name}");
        if (canvasGroup != null) canvasGroup.blocksRaycasts = true;
    }
}
