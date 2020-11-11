using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : EventTrigger
{
    // Start is called before the first frame update
    private bool _dragging;
    GameObject _cursor;

    private void Awake()
    {
        _cursor = GameObject.Find("GazeIcon");
    }

    public void Update()
    {
        if (_dragging)
        {
            this.transform.position = _cursor.transform.position;
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        _dragging = true;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        _dragging = false;
    }
}
