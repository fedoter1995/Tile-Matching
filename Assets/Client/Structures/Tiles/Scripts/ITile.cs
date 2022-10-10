using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface ITile : IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler { }
