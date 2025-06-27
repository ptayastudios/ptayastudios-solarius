using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class item : MonoBehaviour , IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    public int name_;
    public Image img;
    public Transform parentAfterDrag;
    public Transform originalParent;
    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        img.raycastTarget = false;

        
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        img.raycastTarget = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (parentAfterDrag != originalParent)
        {
            transform.SetParent(originalParent);
        }
    }

    void Start()
    {
        originalParent = transform.parent;
    }






}
