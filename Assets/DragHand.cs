using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragHand : MonoBehaviour
{
    public string characterName = "Ch45";
    public string handName = "mixamorig1:LeftHand";
    private Transform handTransform;
    private bool isDragging = false;
    private Vector3 mOffset;
    private float mZCoord;

    void Start()
    {

        Console.WriteLine(handTransform);

        GameObject player = GameObject.Find(characterName);
        if (player != null)
        {
            handTransform = player.transform.Find(handName);
        }
        

    }

    void Update()
    {
        
        if (handTransform == null)
            return;

        if (isDragging && Input.GetMouseButton(0))
        {
            handTransform.position = GetMouseWorldPos() + mOffset;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
        Console.WriteLine("update");
    }


    void OnMouseDown()
    {
        Console.WriteLine(handTransform);

        isDragging = true;
        mZCoord = Camera.main.WorldToScreenPoint(handTransform.position).z;
        mOffset = handTransform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
