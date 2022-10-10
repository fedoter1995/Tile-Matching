using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(CanvasGroup))]
public class Tile : MonoBehaviour, ITile
{

    [SerializeField]
    private TileSize _size = TileSize.Small;

    private CanvasGroup tileCanvasGroup;
    private Canvas mainCanvas;
    private RectTransform rectTransform;

    public event Action OnBeginDragEvent;
    public TileSlot Slot { get; set; }
    public TileSize Size => _size;
    protected void Awake()
    {
        mainCanvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        tileCanvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnBeginDragEvent?.Invoke();
        rectTransform.parent.SetAsLastSibling();
        tileCanvasGroup.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / mainCanvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        tileCanvasGroup.blocksRaycasts = true;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {

    }
    public void BlockRaycasts(bool value)
    {
        if(tileCanvasGroup != null)
            tileCanvasGroup.blocksRaycasts = value;
    }

    public IEnumerator DestroyTile(float destroyTime)
    {

        float time = 0;
        while(destroyTime > time)
        {
            BlockRaycasts(false);
            yield return new WaitForEndOfFrame();
            tileCanvasGroup.alpha = (100 - (time / (destroyTime / 100))) / 100; 
            time += Time.deltaTime;
        }
    }

}
public enum TileSize
{
    Small,
    Normal,
    Large
}