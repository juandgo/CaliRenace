using UnityEngine;
using UnityEngine.EventSystems;

public class TouchDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Collider2D objectCollider;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        objectCollider = GetComponent<Collider2D>();

        if (objectCollider == null)
        {
            Debug.LogError("Collider2D is missing on " + gameObject.name);
        }

        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f; // Reduce opacity for visual feedback
        canvasGroup.blocksRaycasts = false; // Allows to drop over other UI elements
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (objectCollider == null) return; // Skip drag handling if Collider2D is missing

        Vector2 newPosition = rectTransform.anchoredPosition + eventData.delta / canvas.scaleFactor;

        // Check if the new position would collide with an obstacle
        if (!IsCollidingWithObstacle(newPosition))
        {
            rectTransform.anchoredPosition = newPosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1.0f; // Reset opacity
        canvasGroup.blocksRaycasts = true; // Restore raycast blocking
    }

    private bool IsCollidingWithObstacle(Vector2 newPosition)
    {
        if (objectCollider == null) return false; // Skip collision check if Collider2D is missing

        Vector3[] worldCorners = new Vector3[4];
        rectTransform.GetWorldCorners(worldCorners);

        Vector3 shift = new Vector3(newPosition.x - rectTransform.anchoredPosition.x, newPosition.y - rectTransform.anchoredPosition.y, 0);
        for (int i = 0; i < worldCorners.Length; i++)
        {
            worldCorners[i] += shift;
        }

        Bounds newBounds = new Bounds(worldCorners[0], Vector3.zero);
        newBounds.Encapsulate(worldCorners[1]);
        newBounds.Encapsulate(worldCorners[2]);
        newBounds.Encapsulate(worldCorners[3]);

        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(newBounds.center, newBounds.size, 0);

        foreach (Collider2D collider in hitColliders)
        {
            if (collider != objectCollider && collider.CompareTag("Obstaculo"))
            {
                return true;
            }
        }
        return false;
    }
}
