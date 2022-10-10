using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileSlot : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private List<TileSize> _appropriateSize = new List<TileSize>();

    private bool isInitialize = false;
    public Tile Tile { get; set; }
    public int Row { get; set; } = 0;
    public int Column { get; set; } = 0;
    public bool isFilled => Tile != null;

    public event Action<TileSlot> OnDropItemEvent;

    protected void Start()
    {
        if(!isInitialize)      
            SetSlot(GetComponentInChildren<Tile>());
    }
    public virtual void OnDrop(PointerEventData eventData)
    {
        var item = eventData.pointerDrag.transform;
        var tile = item.GetComponent<Tile>();
        if (!isFilled && _appropriateSize.Contains(tile.Size))
        {
            item.SetParent(transform);
            item.localPosition = Vector3.zero;
            tile.Slot.Clear();
            tile.Slot = this;
            this.Tile = tile;
            OnDropItemEvent?.Invoke(this);
        }

    }
    public void SetSlot(Tile tile, List<TileSize> sizes)
    {
        Tile = tile;
        if (Tile != null)
            Tile.Slot = this;
        _appropriateSize = sizes;
        isInitialize = true;
    }
    public void SetSlot(Tile tile)
    {
        Tile = tile;
        if (Tile != null)
            Tile.Slot = this;
        isInitialize = true;
    }
    public void Clear()
    {
        Tile = null;
    }

}
