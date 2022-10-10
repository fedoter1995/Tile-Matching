using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class Table : MonoBehaviour, IWinLoaseHandler
{

    [SerializeField]
        private int _rowsCount = 3;
    [SerializeField]
        private int _columnCount = 3;
    [SerializeField]
        private TileSlot _slotPrefab;
    [SerializeField]
        private GridLayoutGroup _grid;
    [SerializeField]
        private bool _canDragItemsInTable = false;
    [SerializeField]
        private List<TileSize> _appropriateSize = new List<TileSize>();
    [SerializeField]
        private float _animationTime = 2f;

    // Список ,изначально заполненных, слотов(с плитками);
    [HideInInspector]
    public List<int> FilledSlots = new List<int>();
    [HideInInspector]
    public List<Tile> Tiles = new List<Tile>();

    public event Action<bool> WinLoseEvent;
    public float GridZize => _rowsCount * _columnCount;

    private TileSlot[,] slots;
    private bool inEditMode = true;

    protected void Awake()
    {
        inEditMode = false;
        _grid = GetComponent<GridLayoutGroup>();
        SetupGrid();
    }

    private void OnGUI()
    {
        //Отключаем GridLayout для избежания графических неприятностей.
        if (_grid.IsActive())
            _grid.enabled = false;
    }
    private void SetupGrid()
    {
        Clear();
        slots = new TileSlot[_rowsCount, _columnCount];
        for (int i = 0; i < _rowsCount; i++)
        {
            for(int j = 0; j < _columnCount; j++)
            {
                var newSlot = Instantiate(_slotPrefab, transform);
                newSlot.Row = i;
                newSlot.Column = j;
                newSlot.OnDropItemEvent += CheckTiles;
                slots[i, j] = newSlot;

                var itterator = _columnCount * i + j;
                if (FilledSlots.Contains(itterator))
                {
                    var num = FilledSlots.IndexOf(itterator);
                    if(Tiles[num] != null)
                    {
                        var tile = Instantiate(Tiles[num], newSlot.transform);
                        tile.BlockRaycasts(_canDragItemsInTable);
                        newSlot.SetSlot(tile, _appropriateSize);
                    }
                }
                else
                {
                    newSlot.SetSlot(null, _appropriateSize);
                }
            }
        }
    }
    public void ResetGrid()
    {
        _grid.enabled = true;
        _grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _grid.constraintCount = _columnCount;
        SetupGrid();
    }
    private void Clear()
    {
        var childrens = GetComponentsInChildren<TileSlot>();
        if (childrens == null)
            return;
        if(!inEditMode)
        {
            foreach (TileSlot slot in childrens)
            {
                Destroy(slot.gameObject);
            }
        }
        else
        {
            foreach(TileSlot slot in childrens)
            {
                DestroyImmediate(slot.gameObject);
            }
        }

    }
    private void CheckTiles(TileSlot slot)
    {
        if (!HasColumnСoincidences(slot) && !HasRowСoincidences(slot))
            WinLoseEvent?.Invoke(false);
    }
    private bool HasRowСoincidences(TileSlot slot)
    {
        List<TileSlot> matchesSlots = new List<TileSlot>();
        for (int i = 0; i < _rowsCount; i++)
        {
            if (slots[i, slot.Column].isFilled)
            {
                matchesSlots.Add(slots[i, slot.Column]);
            }
            else
            {
                if (matchesSlots.Count >= 3 && matchesSlots.Contains(slot))
                {
                    StartCoroutine(DestroyTilesRoutine(matchesSlots, _animationTime));
                    return true;
                }
                else
                    matchesSlots = new List<TileSlot>();
            }

        }
        if (matchesSlots.Count >= 3 && matchesSlots.Contains(slot))
        {
            StartCoroutine(DestroyTilesRoutine(matchesSlots, _animationTime));
            return true;
        }
        else
            return false;
    }
    private bool HasColumnСoincidences(TileSlot slot)
    {
        List<TileSlot> matchesSlots = new List<TileSlot>();
        for (int i = 0; i < _columnCount; i++)
        {
            if (slots[slot.Row, i].isFilled)
            {
                matchesSlots.Add(slots[slot.Row, i]);
            }
            else
            {
                if (matchesSlots.Count >= 3 && matchesSlots.Contains(slot))
                {
                    StartCoroutine(DestroyTilesRoutine(matchesSlots, _animationTime));
                    return true;
                }
                else
                    matchesSlots = new List<TileSlot>();
            }
        }
        if (matchesSlots.Count >= 3 && matchesSlots.Contains(slot))
        {
            StartCoroutine(DestroyTilesRoutine(matchesSlots, _animationTime));
            return true;
        }
        else
            return false;
    }
    private IEnumerator DestroyTilesRoutine(List<TileSlot> tileSlots,float time)
    {
        foreach (TileSlot matchSlot in tileSlots)
        {
            StartCoroutine(matchSlot.Tile.DestroyTile(time));
            matchSlot.Clear();
        }
        yield return new WaitForSeconds(time);
        WinLoseEvent?.Invoke(true);
    }
}
