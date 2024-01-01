using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDragNew : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    private Vector3 mOffset;
    private float mZCoord;
    private bool isBeingHeld = false;

    private void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
        isBeingHeld = true;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        if (isBeingHeld)
        {
            if (rb == null)
                transform.position = GetMouseWorldPos() + mOffset;
            else
                rb.position = GetMouseWorldPos() + mOffset;
        }
    }

    private void OnMouseUp()
    {
        isBeingHeld = false;
    }

    public bool IsBeingHeld()
    {
        return isBeingHeld;
    }
}
