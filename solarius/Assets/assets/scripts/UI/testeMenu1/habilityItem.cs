using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class habilityItem : MonoBehaviour, IPointerClickHandler
{
//IBeginDragHandler
    public string name_;
    public Transform originalP;
    public Canvas canvas;
    public CanvasGroup group;


    public void Update()
    {/*
        if (canvas == null)
            Debug.Log("Canvas não encontrado!");
        if (group == null)
            Debug.Log("CanvasGroup não encontrado!");*/
    }


    private void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        group = GetComponent<CanvasGroup>();
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        originalP = transform.parent;
        transform.SetParent(canvas.transform);
        //group.blocksRaycasts = false;

        Debug.Log("clicado");


    }
    
    
}
