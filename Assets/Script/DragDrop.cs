using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject dropLocation, stickerLocation;
    private RectTransform rectTransform;
    private GameObject duplicate;
    private Canvas canvas;

    public UnityEvent onDroppedCorrectly;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Debug.Log("Pointer Down on " + gameObject.name);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Debug.Log("Drag Started");

        duplicate = Instantiate(gameObject, transform.parent);
        duplicate.transform.SetParent(canvas.transform, true);
        duplicate.transform.SetAsLastSibling();
        duplicate.GetComponent<DragDrop>().enabled = true;
        duplicate.name = gameObject.name;
        rectTransform = duplicate.GetComponent<RectTransform>();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPoint
        );
        rectTransform.localPosition = localPoint;
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPoint
        );
        rectTransform.localPosition = localPoint;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Debug.Log("Drag Ended");

        if (IsDroppedOnTarget(eventData))
        {
            // Debug.Log("Dropped on correct location!");
            duplicate.transform.SetParent(dropLocation.transform, false);
            duplicate.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            duplicate.SetActive(false);

            onDroppedCorrectly?.Invoke();

        }
        else
        {
            Destroy(duplicate);
        }
    }

    private bool IsDroppedOnTarget(PointerEventData eventData)
    {
        RectTransform dropRect = dropLocation.GetComponent<RectTransform>();
        return RectTransformUtility.RectangleContainsScreenPoint(dropRect, eventData.position, eventData.pressEventCamera);
    }
}