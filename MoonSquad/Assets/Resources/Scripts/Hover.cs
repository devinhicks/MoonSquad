using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    public bool characterCard;
    public Canvas characterCanvas;

    private bool mouseOver;
    private bool originalPos;

    public Vector3 moveDirection;
    public float scaleValue;

    private Transform canvas;
    private Transform startParent;

    public void OnMouseEnter()
    {
        mouseOver = true;
    }

    public void OnMouseExit()
    {
        mouseOver = false;
        originalPos = true;


         transform.position -= moveDirection;
         transform.localScale /= scaleValue;
         transform.SetParent(startParent);
    }

    void Start()
    {
        originalPos = true;
        startParent = transform;
        canvas = GameObject.FindGameObjectWithTag("DraggingCanvas").transform;
    }
    
    void Update()
    {
        if (mouseOver && originalPos)
        {
            transform.SetParent(canvas);
            transform.SetAsLastSibling();

            transform.position += moveDirection;
            transform.localScale *= scaleValue;

            originalPos = false;
        }
    }
}
