using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private bool isDragging;
    private bool hasBeenTouched = false;

    private Vector3 mousePos;
    private float startPosX;
    private float startPosY;

    private float radius = 0.5f;
    private DropSlot[] slotPivots;
    private DropSlot closestPivot;

    private Vector2 startPos;
    private Vector2 originalPos;
    private Vector2 target;

    Transform canvas;

    public void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("DraggingCanvas").transform;
        slotPivots = FindObjectsOfType<DropSlot>();
    }

    public void Update()
    {
        if (isDragging)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(new Vector2(mousePos.x - startPosX, mousePos.y - startPosY));
            transform.SetParent(canvas, true);
            transform.SetAsLastSibling();

            // check available slots to see if object is being dropped there
            closestPivot = ClosestPivot(this.transform);
            if (closestPivot == null)
                target = startPos;
            else
                target = closestPivot.transform.position;
        }
        else if (hasBeenTouched && (transform.position.x != target.x ||
            transform.position.y != target.y))
        {
            transform.position = Vector2.Lerp(transform.position, target, .1f);
        }
    }

    public void OnMouseDown()
    {
        if (!hasBeenTouched) // if this is the first touch
            originalPos = transform.position;
        hasBeenTouched = true;
        isDragging = true;
        startPos = transform.position;

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        startPosX = mousePos.x;
        startPosY = mousePos.y;
    }

    public void OnMouseUp()
    {
        isDragging = false;
        if(closestPivot != null)
            CheckIfOccupied(closestPivot);
    }

    DropSlot ClosestPivot(Transform pos)
    {
        DropSlot closest = null;
        float lastDistance = Mathf.Infinity;
        float currentDistance;

        for(int i = 0; i < slotPivots.Length; i++)
        {
            if (Mathf.Abs(pos.position.x - slotPivots[i].transform.position.x) < radius &&
                Mathf.Abs(pos.position.y - slotPivots[i].transform.position.y) < radius)
            {
                currentDistance = Vector3.Distance(pos.position,
                    slotPivots[i].transform.position);

                if (currentDistance < lastDistance)
                {
                    lastDistance = currentDistance;
                    closest = slotPivots[i];
                }
            }
        }

        return closest;
    }

    void CheckIfOccupied(DropSlot slot)
    {
        if (slot.transform.childCount > 0)
        {
            slot.ResetOccupier().ResetPosition();
        }
        slot.SetOccupied(gameObject);
        transform.SetParent(slot.transform);
    }

    void ResetPosition()
    {
        target = originalPos;
        transform.SetParent(canvas);
    }
}